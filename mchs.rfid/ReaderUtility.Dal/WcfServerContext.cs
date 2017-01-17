// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfServerContext.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Контекст подключения к WCF службе
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ReaderUtility.Dal
{
    using System.ServiceModel;

    using ReaderUtility.Dal.ServerService;

    /// <summary>
    /// Контекст подключения к WCF службе
    /// </summary>
    public class WcfServerContext
    {
        /// <summary>
        /// Адрес сервиса
        /// </summary>
        private readonly string _serviceOperationAddress;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WcfServerContext"/>.
        /// </summary>
        /// <param name="serviceOperationAddress">
        /// Адрес сервиса
        /// </param>
        public WcfServerContext(string serviceOperationAddress)
        {
            this._serviceOperationAddress = serviceOperationAddress;
        }

        /// <summary>
        /// Создать экземпляр клиента подключения к сервису
        /// </summary>
        /// <returns>
        /// Экземпляр клиента подключения к сервису
        /// </returns>
        public ServiceOperationClient CreateClientInstanceForOperatorService()
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