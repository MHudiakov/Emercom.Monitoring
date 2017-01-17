// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleFileManager.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Простой файловый менеджер
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Простой файловый менеджер
    /// </summary>
    public class SimpleFileManager
    {
        /// <summary>
        /// Путь к папке данных
        /// </summary>
        public string DataPath { get; private set; }

        /// <summary>
        /// Простой файловый менеджер
        /// </summary>
        /// <param name="dataPath">Путь к папке данных</param>
        public SimpleFileManager(string dataPath)
        {
            if (string.IsNullOrWhiteSpace(dataPath))
                throw new ArgumentNullException("dataPath");

            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);

            if (Path.IsPathRooted(dataPath))
                DataPath = dataPath;
            else
                // ReSharper disable once AssignNullToNotNullAttribute
                DataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), dataPath);
        }

        /// <summary>
        /// Записать файл в хранилище
        /// </summary>
        /// <param name="stream">Поток данных для записи в файл</param>
        /// <param name="fileName">Имя файла</param>
        public void WriteFile(Stream stream, string fileName)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");
            if (this.FileExists(fileName))
                throw new ArgumentException(@"Файл с таким именем уже существует.", "fileName").AddData("имя файла", fileName);

            var destFileName = Path.Combine(DataPath, fileName);
            var dir = Path.GetDirectoryName(destFileName);
            if (dir == null)
                throw new InvalidOperationException("Неверная директория файла.").AddData("fileName", fileName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (var fs = File.Create(destFileName))
                StreamUtils.Copy(stream, fs, new byte[2048]);
        }

        /// <summary>
        /// Прочитать файл из хранилища
        /// </summary>
        /// <param name="fileName">Относительное имя файла в хранилище</param>
        /// <returns>Поток данных файла</returns>
        public Stream ReadFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");
            if (!this.FileExists(fileName))
                throw new IOException(@"Файл с таким именем не существует.").AddData("имя файла", fileName);

            var destFileName = Path.Combine(DataPath, fileName);
            return File.OpenRead(destFileName);
        }

        /// <summary>
        /// Проверяет сущестование файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>true, если файл уже существует</returns>
        public bool FileExists(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");
            var destFileName = Path.Combine(DataPath, fileName);
            return File.Exists(destFileName);
        }

        /// <summary>
        /// Удаляет файлиз хранилища
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        public void FileDelete(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");
            var destFileName = Path.Combine(DataPath, fileName);
            File.Delete(destFileName);
        }
    }
}