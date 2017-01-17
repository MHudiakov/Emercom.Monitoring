// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfDataManager.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Обект управляющий данными, полученными через WCF
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace ReaderUtility.Dal
{
    using System.ServiceModel;

    using ReaderUtility.Dal.ServerService;

    /// <summary>
    /// Обект управляющий данными, полученными через WCF
    /// </summary>
    public class WcfDataManager
    {
        /// <summary>
        /// Контекст подключения к WCF
        /// </summary>
        public WcfServerContext Context { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context"> 
        /// Контекст подключения 
        /// </param>
        public WcfDataManager(WcfServerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Context = context;
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
                    _serviceOperationClient = Context.CreateClientInstanceForOperatorService();

                return _serviceOperationClient;
            }
        }
    }
}
