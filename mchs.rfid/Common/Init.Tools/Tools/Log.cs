// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Log.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Логер
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    /// <summary>
    /// Логер
    /// </summary>
    public class Log
    {
        #region Singletone

        /// <summary>
        /// Семафор доступа к ссылке на активный логер
        /// </summary>
        private static readonly object s_activeLock = new object();

        /// <summary>
        /// Ссылка на активный логер
        /// </summary>
        private static volatile Log s_active;

        /// <summary>
        /// Ссылка на активный логер
        /// </summary>
        public static Log Active
        {
            get
            {
                if (s_active == null)
                {
                    lock (s_activeLock)
                    {
                        if (s_active == null)
                            s_active = new Log();
                    }
                }

                return s_active;
            }
        }

        /// <summary>
        /// Предотвращает вызов конструктора по умолчанию для класса <see cref="Log"/>.
        /// </summary>
        private Log()
        {
            AppDomain.CurrentDomain.DomainUnload += (sender, e) =>
                                                        {
                                                            this._logWriter.Abort();
                                                            this.WriteMessagesToDisk();
                                                        };

            this.LogPaths = new Dictionary<string, bool>();
            this._messages = new List<Entry>();

            this._logWriter = new Thread(this.WriteLogs) { IsBackground = true };

            var directoryName = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);

            if (directoryName == null)
                throw new ArgumentException(string.Format("Не удалось получить путь к директории {0}", Assembly.GetCallingAssembly().Location));
            this.RootDir = Path.Combine(directoryName, "Log");

            this._logWriter.Start();
        }
        #endregion

        #region Логирование сообщений

        /// <summary>
        /// Добавление сообщения в активный лог
        /// </summary>
        /// <param name="msg">
        /// Сообщение
        /// </param>
        public static void Add(string msg)
        {
            Active.LogMsg(msg, string.Empty, string.Empty);
        }

        /// <summary>
        /// Добавление сообщения в активный лог
        /// </summary>
        /// <param name="prefix">
        /// Префикс
        /// </param>
        /// <param name="msg">
        /// Сообщение
        /// </param>
        /// <param name="subDir">
        /// Директория
        /// </param>
        public static void Add(string prefix, string msg, string subDir = "")
        {
            Active.LogMsg(msg, prefix, subDir);
        }
        #endregion

        #region Логирование ошибок

        /// <summary>
        /// Добавление исключения в лог 
        /// </summary>
        /// <param name="ex">
        /// Логируемое исключение
        /// </param>
        /// <param name="subDir">
        /// Подкаталог
        /// </param>
        public static void AddException(Exception ex, string subDir = "")
        {
            AddException(string.Empty, ex, subDir);
        }

        /// <summary>
        /// Добавление исключения в лог
        /// </summary>
        /// <param name="prefix">
        /// Префикс 
        /// </param>
        /// <param name="ex">
        /// Логируемое исключение
        /// </param>
        /// <param name="subDir">
        /// Подкаталог
        /// </param>
        public static void AddException(string prefix, Exception ex, string subDir = "")
        {
            string msg = RecursiveExceptionFormat(ex);
            Add(prefix, msg, subDir);
        }

        /// <summary>
        /// Рекурсивное формирование строки сообщения об исключении
        /// </summary>
        /// <param name="ex">
        /// Исключение 
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string RecursiveExceptionFormat(Exception ex)
        {
            if (ex == null)
                return string.Empty;

            // формируем строку доп. данных об ошибке
            var dataStr = string.Empty;
            if (ex.Data.Keys.Count > 0)
            {
                foreach (DictionaryEntry data in ex.Data)
                    dataStr += string.Format("\t{0}:{1}\r\n", data.Key, data.Value);
            }

            dataStr = string.IsNullOrEmpty(dataStr) ? "empty" : "\r\n" + dataStr;

            var stackTrace = ex.StackTrace;
            stackTrace = string.IsNullOrEmpty(stackTrace) ? "empty" : "\r\n" + stackTrace;

            var inner = RecursiveExceptionFormat(ex.InnerException);
            inner = string.IsNullOrEmpty(inner) ? "\r\n" : inner;
            return string.Format("[{3}] Message: {0}\r\n[Data]: {4}\r\nStack: {1}\r\n{2}", ex.Message, stackTrace, inner, ex.GetType().Name, dataStr);
        }
        #endregion

        #region Entry
        /// <summary>
        /// Представляет сведения о логируемом сообщении
        /// </summary>
        private class Entry
        {
            /// <summary>
            /// Время логирования
            /// </summary>
            public DateTime Time { get; private set; }

            /// <summary>
            /// Сообщение, отправляющееся в лог
            /// </summary>
            public string Msg { get; private set; }

            /// <summary>
            /// Префикс формата строки пути к файлу 
            /// </summary>
            public string Preffix { get; private set; }

            /// <summary>
            /// Директория
            /// </summary>
            public string Dir { get; private set; }

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="Entry"/>.
            /// </summary>
            /// <param name="msg">
            /// Сообщение, отправляющееся в лог
            /// </param>
            /// <param name="preffix">
            /// Префикс формата строки пути к файлу
            /// </param>
            /// <param name="dir">
            /// Директория
            /// </param>
            public Entry(string msg, string preffix, string dir)
            {
                this.Time = DateTime.Now;
                this.Msg = msg;
                this.Preffix = preffix;
                this.Dir = dir;
            }
        }

        /// <summary>
        /// Семафор доступа к логируемым сообщениям
        /// </summary>
        private readonly object _messagesLock = new object();

        /// <summary>
        /// Список логируемых сообщений
        /// </summary>
        private readonly List<Entry> _messages;

        /// <summary>
        /// Запись логов на диск
        /// </summary>
        private void WriteLogs()
        {
            try
            {
                while (true)
                {
                    this.WriteMessagesToDisk();
                    if (Thread.CurrentThread.IsAlive)
                        Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                this.LogMsgToFile(new Entry(new Exception("Ошибка остановки потока записи логов", ex).ToString(), string.Empty, string.Empty));
            }
        }

        /// <summary>
        /// Запись сообщений на диск
        /// </summary>
        private void WriteMessagesToDisk()
        {
            var messages = new List<Entry>();

            // Блокировка доступа к сообщениям во время записи
            lock (this._messagesLock)
            {
                messages.AddRange(this._messages);
                this._messages.Clear();
            }

            if (messages.Count > 0)
                foreach (Entry entry in messages.OrderBy(e => e.Time))
                    this.LogMsgToFile(entry);
        }
        #endregion

        /// <summary>
        /// Разделы логирования, которые будут запысываться в файл
        /// формат строк: {dir}\{prefix}, true/false
        /// </summary>
        public Dictionary<string, bool> LogPaths { get; private set; }
        
        /// <summary>
        /// Директория логирования
        /// </summary>
        public string RootDir { get; set; }

        #region Запись в файл

        /// <summary>
        /// Семафор доступа к коду записи сообщения в файл
        /// </summary>
        private readonly object _thisLock = new object();

        /// <summary>
        /// Поток логирования
        /// </summary>
        private readonly Thread _logWriter;

        /// <summary>
        /// Логирование сообщения
        /// </summary>
        /// <param name="msg">
        /// Сообщение
        /// </param>
        /// <param name="filePrefix">
        /// Префикс файла
        /// </param>
        /// <param name="subDir">
        /// Поддиректория
        /// </param>
        private void LogMsg(string msg, string filePrefix, string subDir)
        {
            lock (this._messagesLock)
            {
                var entry = new Entry(msg, filePrefix, subDir);

                // _messages.Add(entry);
                this.LogMsgToFile(entry);
            }
        }

        /// <summary>
        /// Логирование сообщения в файл
        /// </summary>
        /// <param name="entry">
        /// Сведения о логируемом сообщении
        /// </param>
        private void LogMsgToFile(Entry entry)
        {
            lock (this._thisLock)
            {
                var subDir = entry.Dir;
                var msg = entry.Msg;
                var filePrefix = entry.Preffix ?? string.Empty;
                filePrefix = filePrefix.Trim('_', ' ');

                // фильтруем логи
                var fullDir = subDir + "\\" + filePrefix;
                fullDir = fullDir.Trim('\\');
                fullDir = fullDir.ToLower();

                if (this.LogPaths.Count > 0 && this.LogPaths.ContainsKey(fullDir) && !this.LogPaths[fullDir.ToLower()])
                    return;

                var dt = entry.Time;
                try
                {
                    var txt = string.Format("{0:00}:{1:00}:{2:00}.{3:000}> {4}", dt.TimeOfDay.Hours, dt.Minute, dt.Second, dt.Millisecond, msg);

                    var dirname = this.GetLogDirectory(subDir);

                    var filename = Path.Combine(dirname, string.Format("{0}_{1}.log", entry.Time.Date.ToString("yyyy-MM-dd"), filePrefix));

                    // удаляем двойные последовательности
                    var internalTrim = new[] { "_", " " };

                    foreach (var s in internalTrim)
                    {
                        var ds = s + s;
                        while (filename.Contains(ds))
                            filename = filename.Replace(ds, s);
                    }

                    filename = filename.Trim(' ', '_');

                    File.AppendAllText(filename, txt + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    AddException("Не удалось залогировать сообщение в файл", ex);
                }
            }
        }

        /// <summary>
        /// Получение дирректории логирования
        /// </summary>
        /// <param name="subDir">
        /// Поддиректория
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetLogDirectory(string subDir = "")
        {
            if (!Directory.Exists(this.RootDir))
                Directory.CreateDirectory(this.RootDir);

            var dirName = Path.Combine(this.RootDir, DateTime.Now.Date.Year.ToString(CultureInfo.InvariantCulture) + "-" + DateTime.Now.Date.Month.ToString(CultureInfo.InvariantCulture));

            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var newDir = dirName;
            if (subDir != string.Empty)
            {
                newDir = Path.Combine(newDir, subDir);
                if (!Directory.Exists(newDir))
                    Directory.CreateDirectory(newDir);
            }

            return newDir;
        }
        #endregion
    }
}
