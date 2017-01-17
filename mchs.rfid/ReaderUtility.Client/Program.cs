// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Утилита считывателя RFID
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ReaderUtility.Client
{
    using System;
    using System.Linq;
    using System.Threading;

    using Init.Tools;

    using ReaderUtility.Core;
    using ReaderUtility.Dal;
    using ReaderUtility.Dal.ServerService;

    /// <summary>
    /// Утилита считывателя RFID
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Запуск утилиты считывателя RFID
        /// </summary>
        public static void Main()
        {
            Log.Add("Init");
            AppDomain.CurrentDomain.UnhandledException += (sender1, args) =>
            {
                var error = args.ExceptionObject as Exception ?? new Exception("Неизвестная ошибка.");
                Log.AddException(new Exception("Необработанная ошибка в домене приложения", error));
            };

            Unit unit = null;

            // Инициализируем WCF службу
            try
            {
                Console.WriteLine("Создание контекста доступа к серверу приложений");
                var context = new WcfServerContext(Settings.Default.ServiceOperationAddress);
                var dataManager = new WcfDataManager(context);
                DalContainer.RegisterWcfDataMager(dataManager);
                
                
                var serialNum = Settings.Default.SerialNumber;
                Unit[] list = dataManager.ServiceOperationClient.GetAllUnit();
                unit = list.FirstOrDefault(u => u.SerialNum.Equals(serialNum));

                dataManager.ServiceOperationClient.Close();

                if (unit == null)
                {
                    Console.WriteLine("Внимание! Не найден объект с ИД: " + Settings.Default.SerialNumber);
                    Log.Add("Внимание! Не найден объект с ИД: " + Settings.Default.SerialNumber);
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Log.Add("Объект: " + unit.Name + " (" + unit.Id + ")");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при подключении к серверу" + ex.Message);
                Log.AddException(ex);
            }

            Log.Add("Подключение к оборудованию к [" + Settings.Default.ReaderHost + "] ...");

            // Запускаем приемник тегов
            if (unit == null)
            {
                Console.WriteLine("Не найден объект, соответствующий указанному в конфигурации серийному номеру. " + 
                                  "Пожалуйста, проверьте конфигурацию и перезапустите приложение");
                return;
            }

            var tagReceiver = new TagReceiver(unit.Id);
            tagReceiver.RunReceiving(Settings.Default.ReaderHost);

            Console.ReadLine();

            Log.Add("Close");
        }
    }
}
