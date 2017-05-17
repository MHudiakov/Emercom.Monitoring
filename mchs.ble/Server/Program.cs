// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Сервера
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Server
{
    using System;

    using DAL;

    using Init.Tools;

    using Server.Dal;
    using Server.Host.Properties;

    using Service;

    /// <summary>
    /// Сервера
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Обработчик ошибки домена
        /// </summary>
        /// <param name="sender">
        /// Отправитель события
        /// </param>
        /// <param name="args">
        /// Описание события
        /// </param>
        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var ex =
                new Exception(
                    string.Format(
                        "Необработанная ошибка в домене приложения. {0}",
                        args.IsTerminating ? "Приложение будет закрыто" : "Приложение не будет закрыто"),
                    args.ExceptionObject as Exception);

            s_logger.LogException(ex);
        }

        /// <summary>
        /// Обертка системы логирования
        /// </summary>
        private static Loger s_logger;

        /// <summary>
        /// Точка входа сервера
        /// </summary>
        public static void Main()
        {
            // настраиваем систему логирования
            s_logger = new Loger();

            s_logger.OnLogMsg += msg =>
            {
                Log.Add("main", msg);
                Console.WriteLine(@"{0}>> {1}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.ffff"), msg);
            };
            s_logger.OnLogException += ex =>
            {
                Log.AddException("errors", ex);
                Log.Add("main", string.Format("Произошла ошибка. Подробности: {0}", ex.Message));
            };

            s_logger.LogMsg("Запуск сервера");

            // подписываемся на перехват ошибок в домене приложения
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;

            // Создание контекста подключения к базе
            try
            {
                var connectionString = Settings.Default.ConnectionString;
                var context = new DataContext(connectionString);               

                var dataManger = new DataManager(context);
                DalContainer.RegisterDataManger(dataManger);

                dataManger.Loger.OnLogMsg += msg =>
                {
                    Log.Add("main", msg, "DataManager");
                    Console.WriteLine(@"{0}>> DataManager: {1}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.ffff"), msg);
                };
                dataManger.Loger.OnLogException += ex =>
                {
                    Log.AddException("errors", ex, "DataManager");
                    Log.Add("main", string.Format("Произошла ошибка. Подробности: {0}", ex.Message), "DataManager");
                    s_logger.LogException(ex);
                };
            }
            catch (Exception ex)
            {
                s_logger.LogException(new Exception("Ошибка создания подключения к DB. Приложение будет закрыто.", ex));
                return;
            }

            s_logger.LogMsg("Создание сервиса");                    
            try
            {
                var serviceOperation = new ServiceOperation();

                serviceOperation.Logger.OnLogMsg += msg => Log.Add("main", msg, "ServiceOperation");
                serviceOperation.Logger.OnLogException += ex =>
                {
                    Log.AddException("errors", ex, "ServiceOperation");
                    Log.Add("main", string.Format("Произошла ошибка. Подробности: {0}", ex.Message), "ServiceOperation");
                    s_logger.LogException(new Exception("Ошибка на сервисе", ex));
                };

                serviceOperation.Faulted += (sender, eventArgs) =>
                {
                    s_logger.LogMsg("Service перешел в состоянии Faulted. Перезапускаем сервис.");
                    serviceOperation.Stop();
                    serviceOperation.Start();
                };

                s_logger.LogMsg("Запуск сервиса");
                serviceOperation.Start();
            }
            catch (Exception exception)
            {
                s_logger.LogException(new Exception("Ошибка запуска сервиса", exception));
                throw;
            }

            do
            {
                s_logger.LogMsg("для остановки сервера введите exit");
                Console.Write(@"> ");
            }

            while (Console.ReadLine() != "exit");
        }    
    }
}