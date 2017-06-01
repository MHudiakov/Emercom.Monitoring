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
    using System.ServiceModel;

    using ServiceReference;
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
        public WcfDataContext Context { get; }

        public List<Division> DivisionList { get; private set; }

        public List<Equipment> EquipmentList { get; private set; }

        public List<EquipmentGroup> EquipmentGroupList { get; private set; }

        public List<KEquipment> KEquipmentList { get; private set; }

        public List<Unit> UnitList { get; private set; }

        public List<User> UserList { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context"> 
        /// Контекст подключения 
        /// </param>
        public WcfDataManager(WcfDataContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            Context = context;
            Thread.Sleep(5000); 
            RefreshCashLists();

            var thread = new Thread(LoadUnitTask) { IsBackground = true };
            thread.Start();
        }

        public void RefreshCashLists()
        {
            DivisionList = ServiceOperationClient.GetDivisionList();
            EquipmentList = ServiceOperationClient.GetEquipmentList();
            EquipmentGroupList = ServiceOperationClient.GetGroupList();
            KEquipmentList = ServiceOperationClient.GetKEquipmentList();
            UnitList = ServiceOperationClient.GetUnitList();
            UserList = ServiceOperationClient.GetUserList();
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
