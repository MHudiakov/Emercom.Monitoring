using System;
using Init.Tools;
using Server.Dal;
using Server.WCF;
using Settings = Server.Host.Properties.Settings;

namespace Server.Host
{
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
                    $"Необработанная ошибка в домене приложения. {(args.IsTerminating ? "Приложение будет закрыто" : "Приложение не будет закрыто")}",
                    args.ExceptionObject as Exception);

            sLogger.LogException(ex);
        }

        /// <summary>
        /// Обертка системы логирования
        /// </summary>
        private static Loger sLogger;

        /// <summary>
        /// Точка входа сервера
        /// </summary>
        public static void Main()
        {
            // настраиваем систему логирования
            sLogger = new Loger();

            sLogger.OnLogMsg += msg =>
            {
                Log.Add("main", msg);
                Console.WriteLine(@"{0:dd.MM.yyyy HH:mm:ss.ffff}>> {1}", DateTime.Now, msg);
            };
            sLogger.OnLogException += ex =>
            {
                Log.AddException("errors", ex);
                Log.Add("main", $"Произошла ошибка. Подробности: {ex.Message}");
            };

            sLogger.LogMsg("Запуск сервера");

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
                    Log.Add("main", msg, "GetDataManager");
                    Console.WriteLine(@"{0:dd.MM.yyyy HH:mm:ss.ffff}>> GetDataManager: {1}", DateTime.Now, msg);
                };
                dataManger.Loger.OnLogException += ex =>
                {
                    Log.AddException("errors", ex, "GetDataManager");
                    Log.Add("main", $"Произошла ошибка. Подробности: {ex.Message}", "GetDataManager");
                    sLogger.LogException(ex);
                };
            }
            catch (Exception ex)
            {
                sLogger.LogException(new Exception("Ошибка создания подключения к DB. Приложение будет закрыто.", ex));
                return;
            }

            sLogger.LogMsg("Создание сервиса");                    
            try
            {
                var serviceOperation = new ServiceOperation();

                serviceOperation.Logger.OnLogMsg += msg => Log.Add("main", msg, "ServiceOperation");
                serviceOperation.Logger.OnLogException += ex =>
                {
                    Log.AddException("errors", ex, "ServiceOperation");
                    Log.Add("main", $"Произошла ошибка. Подробности: {ex.Message}", "ServiceOperation");
                    sLogger.LogException(new Exception("Ошибка на сервисе", ex));
                };

                serviceOperation.Faulted += (sender, eventArgs) =>
                {
                    sLogger.LogMsg("Service перешел в состоянии Faulted. Перезапускаем сервис.");
                    serviceOperation.Stop();
                    serviceOperation.Start();
                };

                sLogger.LogMsg("Запуск сервиса");
                serviceOperation.Start();
            }
            catch (Exception exception)
            {
                sLogger.LogException(new Exception("Ошибка запуска сервиса", exception));
                throw;
            }


            //EquipmentGroup equipmentGroup = new EquipmentGroup();
            //equipmentGroup.Name = "Test";
            //equipmentGroup.ParentId = null;
            //DalContainer.GetDataManager.EquipmentGroupRepository.Add(equipmentGroup);

            //KEquipment kEquipment = new KEquipment();
            //kEquipment.EquipmentGroupId = equipmentGroup.Id;
            //kEquipment.Name = "dfsa";
            //DalContainer.GetDataManager.KEquipmentRepository.Add(kEquipment);



            var t = DalContainer.GetDataManager.MovementRepository.GetByTimeAndUnitId(1, DateTime.MinValue, DateTime.MinValue);


            do
            {
                sLogger.LogMsg("для остановки сервера введите exit");
                Console.Write(@"> ");
            }

            while (Console.ReadLine() != "exit");
        }    
    }
}