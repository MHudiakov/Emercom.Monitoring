// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfDataManager.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Обект управляющий данными, полученными через WCF
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.WCF
{
    using System;
    using System.Linq;
    using System.ServiceModel;

    using DAL.WCF.ServiceReference;
    using System.Collections.Generic;
    using Init.Tools;
    using System.Threading;


    /// <summary>
    /// Обект управляющий данными, полученными через WCF
    /// </summary>
    public class WcfDataManager
    {
        /// <summary>
        /// Контекст подключения к WCF
        /// </summary>
        public WcfDataContext Context { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context"> 
        /// Контекст подключения 
        /// </param>
        public WcfDataManager(WcfDataContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Context = context;
            Thread.Sleep(5000); 
            RefreshCashLists();

            var thread = new Thread(new ThreadStart(this.LoadUnitTask)) { IsBackground = true };
            thread.Start();
        }

        public void RefreshCashLists()
        {
            UnitList = this.ServiceOperationClient.GetAllUnit().ToList();
            MovementList = this.ServiceOperationClient.GetLastMovements().ToList();
            EquipmentList = this.ServiceOperationClient.GetAllEquipment().ToList();
            UniqEquipmentList = this.ServiceOperationClient.GetAllUniqEquipmentObject().ToList();
            NonUniqEquipmentList = this.ServiceOperationClient.GetAllNonUniqEquipmentObject().ToList();
            TagList = this.ServiceOperationClient.GetAllTags().ToList();
            kEquipmentList = this.ServiceOperationClient.GetAllkEquipment().ToList();
            kObjectList = this.ServiceOperationClient.GetAllkObject().ToList();
            GroupList = this.ServiceOperationClient.GetAllGroup().ToList();
            TripList = this.ServiceOperationClient.GetAllTrips().ToList();
        }

        /// <summary>
        /// Посредник для работы с сервисом оператора службы WCF
        /// </summary>
        private ServiceOperationClient _serviceOperationClient;

        /// <summary>
        /// Прямой доступ к сервису оператора
        /// </summary>
        public ServiceOperationClient ServiceOperationClient
        {
            get
            {
                if (_serviceOperationClient == null
                    || _serviceOperationClient.State == CommunicationState.Closed
                    || _serviceOperationClient.State == CommunicationState.Faulted
                    || _serviceOperationClient.State == CommunicationState.Closing)
                    _serviceOperationClient = Context.CreateClientInstance();

                return _serviceOperationClient;
            }
        }

        public List<Trip> TripList { get; private set; }

        public List<Unit> UnitList { get; private set; }

        public List<Movement> MovementList { get; private set; }

        public List<Equipment> EquipmentList { get; private set; }

        public List<UniqEquipmentObject> UniqEquipmentList { get; private set; }

        public List<NonUniqEquipmentObject> NonUniqEquipmentList { get; private set; }

        public List<Tag> TagList { get; private set; }

        public List<kEquipment> kEquipmentList { get; private set; }

        public List<kObject> kObjectList { get; private set; }

        public List<Group> GroupList { get; private set; }

        /// <summary>
        /// Поток обновления списков
        /// </summary>
        private void LoadUnitTask()
        {
            while (true)
            {
                try
                {
                    // 1 раз в 10 сек обновляем все списки
                    Thread.Sleep(10000);

                    RefreshCashLists();
                }
                catch (Exception ex)
                {
                    Log.AddException(new Exception("Ошибка запуска потока чтения оборудования и движения", ex));
                }
            }
        }


    }
}
