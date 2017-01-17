// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TagReceiver.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Команды работы с ридером
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace ReaderUtility.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Init.Tools;

    using Org.LLRP.LTK.LLRPV1;

    using ReaderUtility.Dal;
    using ReaderUtility.Dal.ServerService;

    /// <summary>
    /// Команды работы с ридером
    /// </summary>
    public sealed class TagReceiver
    {
        /// <summary>
        /// Таймаут приема тегов
        /// </summary>
        private const int READ_TIMEOUT_SECONDS = 4;

        /// <summary>
        /// Период переподключения к считывателю
        /// </summary>
        private const int RECONNECT_PERIOD_SECONDS = 120;

        /// <summary>
        /// Время последнего переподключения к считывателю
        /// </summary>
        private DateTime mLastReconnectTime;

        private static object s_lockObject = new object();

        /// <summary>
        /// ИД объекта, на котором установлен ридер
        /// </summary>
        private readonly int _unitId;

        /// <summary>
        /// Reader
        /// </summary>
        private readonly LLRPClient _reader = new LLRPClient();

        /// <summary>
        /// Время последнего приема данных со считывателя
        /// </summary>
        private DateTime LastReceiveTagsTime { get; set; }

        /// <summary>
        /// Предыдущий список пришедших тегов
        /// </summary>
        private List<string> _lastTagList = new List<string>();


        private List<CashMovement> _cashListMovements = new List<CashMovement>();

        private RoSpec roSpec = new RoSpec();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="TagReceiver"/>.
        /// </summary>
        /// <param name="unitId">
        /// ИД объекта, на котором установлен ридер
        /// </param>
        public TagReceiver(int unitId)
        {
            this._unitId = unitId;
        }

        /// <summary>
        /// Начать прием тегов
        /// </summary>
        /// <param name="host">
        /// Хост
        /// </param>
        public void RunReceiving(string host)
        {
            // Подключаемся к ридеру
            this.Connect(host);

            // Подписываемся на событие получения тегов
            roSpec.reader.OnRoAccessReportReceived += this.OnReportEvent;

            // Запускаем в отдельном потоке проверку подключения утилиты к считывателю
            ThreadPool.QueueUserWorkItem(this.ReaderConnectionChecker, host);

            // Send the messages
            roSpec.DeleteRoSpec();
            roSpec.AddRoSpec();
            roSpec.EnableRoSpec();
        }

        /// <summary>
        /// Осуществлят подключение к считывателю тегов
        /// </summary>
        /// <param name="host"></param>
        private void Connect(string host)
        {
            var status = ENUM_ConnectionAttemptStatusType.Another_Connection_Attempted;
            do
            {
                try
                {
                    roSpec.reader.Open(host, 2000, out status);
                    if (status != ENUM_ConnectionAttemptStatusType.Success)
                    {
                        Console.WriteLine("Не успешная попытка подключения. Статус: " + status);
                        Thread.Sleep(5000);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Не удалось подключиться к оборудованию: " + e.Message);
                    Log.AddException(e);
                    Thread.Sleep(5000);
                }
            }
            while (status != ENUM_ConnectionAttemptStatusType.Success);

            mLastReconnectTime = DateTime.Now;
        }

        /// <summary>
        /// Осуществляет проверку подключения и переподключение к считывателю
        /// </summary>
        /// <param name="host">
        /// Хост
        /// </param>
        private void ReaderConnectionChecker(object host)
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(11000);

                    lock (this)
                    {
                        if ((DateTime.Now - LastReceiveTagsTime).TotalSeconds > READ_TIMEOUT_SECONDS * 2)
                        {
                            Log.Add("Переподключение... ");
                            Console.WriteLine("Переподключение... ");
                            roSpec.reader.Close();
                            roSpec = new RoSpec();
                            this.RunReceiving(host as string);
                            LastReceiveTagsTime = DateTime.Now;
                            Console.WriteLine("OK");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка запуска потока чтения оборудования и движения: " + ex.Message);
                    Log.AddException(new Exception("Ошибка запуска потока чтения оборудования и движения", ex));
                }
            }
        }

        /// <summary>
        /// Обработчик события приема тегов с ридера
        /// </summary>
        /// <param name="msg">
        /// Сообщение с тегами
        /// </param>
        public void OnReportEvent(MSG_RO_ACCESS_REPORT msg)
        {
            lock (s_lockObject)
            {
                LastReceiveTagsTime = DateTime.Now;
                var tagList = new List<string>();

                // В поле нет тегов
                if (msg.TagReportData != null)
                {
                    for (var i = 0; i < msg.TagReportData.Length; i++)
                    {
                        if (msg.TagReportData[i].EPCParameter.Count > 0)
                        {
                            string epc;

                            // Two possible types of EPC: 96-bit and 128-bit
                            if (msg.TagReportData[i].EPCParameter[0].GetType() == typeof(PARAM_EPC_96))
                                epc = ((PARAM_EPC_96)msg.TagReportData[i].EPCParameter[0]).EPC.ToHexString();
                            else
                                epc = ((PARAM_EPCData)msg.TagReportData[i].EPCParameter[0]).EPC.ToHexString();

                            tagList.Add(epc);
                        }
                    }
                }

                this.SendDataOnServer(tagList);
                try
                {
                    DalContainer.WcfDataManager.ServiceOperationClient.Close();
                }
                catch (Exception ex)
                {
                    var exMsg = "Ошибка при закрытии канала";
                    Console.WriteLine(exMsg);
                    ex.AddData("msg", exMsg);
                    Log.AddException(ex);
                }

                _lastTagList = tagList;
            }
        }

        /// <summary>
        /// Отправка данных о тегах на сервер
        /// </summary>
        /// <param name="tagList">
        /// Список тегов
        /// </param>
        private void SendDataOnServer(List<string> tagList)
        {
            try
            {
                // если кэш не пуст отправка данных
                if (this._cashListMovements.Any())
                {
                    var deleteTegList = new List<CashMovement>();
                    foreach (var cashMovement in this._cashListMovements)
                    {
                        AddCashMovement(cashMovement);
                        deleteTegList.Add(cashMovement);
                    }

                    deleteTegList.ForEach(item => this._cashListMovements.Remove(item));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при передачи данных на сервер");
                ex.AddData("msg", "Произошла ошибка при передачи данных на сервер");
                Log.AddException(ex);
            }

            // Находим новые теги
            foreach (var tag in tagList.Where(tag => !this._lastTagList.Contains(tag)))
            {
                this.AddMovement(tag, true, DateTime.Now);
            }

            // Находим теги, которые вышли из поля
            foreach (var tag in this._lastTagList.Where(tag => !tagList.Contains(tag)))
            {
                this.AddMovement(tag, false, DateTime.Now);
            }
        }

        /// <summary>
        /// Добавление движения на сервер
        /// </summary>
        /// <param name="tag">
        /// Тег </param>
        /// <param name="isArrived">
        /// Флаг, показывает, появилось оборудование в поле устройства, или вышло из него
        /// </param>
        private void AddMovement(string tag, bool isArrived, DateTime time)
        {
            try
            {
                this.AddMovementToServer(tag, isArrived, time);
            }
            catch (Exception ex)
            {
                var cashMovement = new CashMovement { Tag = tag, IsArrived = isArrived, Time = time };
                if (!_cashListMovements.Exists(x => x == cashMovement))
                    _cashListMovements.Add(cashMovement);
                Console.WriteLine("Произошла ошибка при передачи данных на сервер");
                ex.AddData("msg", "Произошла ошибка при передачи данных на сервер");
                Log.AddException(ex);
            }
        }

        /// <summary>
        /// Добавление движения из кэша на сервер
        /// </summary>
        private void AddCashMovement(CashMovement cashMovement)
        {
            this.AddMovementToServer(cashMovement.Tag, cashMovement.IsArrived, cashMovement.Time);
        }

        /// <summary>
        /// Отправка движения на сервер
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="isArrived"></param>
        /// <param name="time"></param>
        private void AddMovementToServer(string tag, bool isArrived, DateTime time)
        {
            var tagsList = DalContainer.WcfDataManager.ServiceOperationClient.GetAllTags().ToList();
            var equipmentTag = tagsList.FirstOrDefault(t => t.Rfid.Equals(tag));

            // Выгружаем оборудование, которому соответствует данней тег, чтобы установить у движения ид оборудования
            var equipmentList = DalContainer.WcfDataManager.ServiceOperationClient.GetAllEquipment().ToList();
            var equipment = equipmentTag != null ? equipmentList.FirstOrDefault(e => e.Id == equipmentTag.EquipmentId) : null;

            // Создаем движение
            var movement = new Movement
                               {
                                   IsArrived = isArrived,
                                   UnitId = this._unitId,
                                   DateOfMovement = time,
                                   EquipmentId = equipment == null ? (int?)null : equipment.Id
                               };

            var arrived = isArrived ? "Прибыло" : "Убыло";
            Console.WriteLine("{2}: {0} {1}", arrived, tag, DateTime.Now.ToString("T"));

            // Добавление передвижения
            DalContainer.WcfDataManager.ServiceOperationClient.AddMovement(movement);

            // Добавление нового тега в базу
            if (equipment == null && equipmentTag == null)
            {
                var ntag = new Tag { EquipmentId = null, Rfid = tag };
                DalContainer.WcfDataManager.ServiceOperationClient.AddTag(ntag);
            }
        }

        /// <summary>
        /// Класс для кэширования неотправленных данных
        /// </summary>
        private class CashMovement
        {
            /// <summary>
            /// Номер метки
            /// </summary>
            public string Tag { get; set; }

            /// <summary>
            /// Прибыло\убыло
            /// </summary>
            public bool IsArrived { get; set; }

            /// <summary>
            /// Время события
            /// </summary>
            public DateTime Time { get; set; }
        }
    }
}