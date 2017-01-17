// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfDataContext.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Контекст подключения к сервису WCF
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.WCF
{
    using System;
    using System.ServiceModel;

    using DAL.WCF.ServiceReference;

    /// <summary>
    /// Контекст доступа к серверу приложения по технологии WCF
    /// </summary>
    public class WcfDataContext
    {
        /// <summary>
        /// Адрес сервиса
        /// </summary>
        private readonly string _serviceOperationAddress;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WcfDataContext"/>.
        /// </summary>
        /// <param name="serviceOperationAddress">
        /// Адрес сервиса
        /// </param>
        public WcfDataContext(string serviceOperationAddress)
        {
            this._serviceOperationAddress = serviceOperationAddress;
        }

        /// <summary>
        /// Создать экземпляр клиента подключения к сервису
        /// </summary>
        /// <returns>
        /// Экземпляр клиента подключения к сервису
        /// </returns>
        public ServiceOperationClient CreateClientInstance()
        {
            var binding = new NetTcpBinding();
            binding.MaxBufferPoolSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.Security.Mode=SecurityMode.None;

            var serviceOperatorClient = new ServiceOperationClient(binding, new EndpointAddress(_serviceOperationAddress));

            return serviceOperatorClient;
        }
    }
}