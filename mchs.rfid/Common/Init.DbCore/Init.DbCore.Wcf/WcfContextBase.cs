// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfContextBase.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Типизированый WCF контекст
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Wcf
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.ServiceModel;

    /// <summary>
    /// Типизированый WCF контекст
    /// </summary>
    /// <typeparam name="TCommunicationObject">
    /// Интерфейс коммуникационного объекта
    /// </typeparam>
    public class WcfContextBase<TCommunicationObject> : IDisposable
        where TCommunicationObject : class, ICommunicationObject, new()
    {
        /// <summary>
        /// Типизированый WCF контекст
        /// </summary>
        public WcfContextBase()
        {
            AppDomain.CurrentDomain.DomainUnload += this.CurrentDomainOnDomainUnload;
        }

        /// <summary>
        /// Отключаемся от сервера при выгрузке домена
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void CurrentDomainOnDomainUnload(object sender, EventArgs eventArgs)
        {
            AppDomain.CurrentDomain.DomainUnload -= this.CurrentDomainOnDomainUnload;
            this.Dispose();
        }

        /// <summary>
        /// Коммуникационный объект, используемый контекстом для связи
        /// </summary>
        private TCommunicationObject _communicationObject;

        /// <summary>
        /// Коммуникационный объект, используемый контекстом для связи
        /// </summary>
        public virtual TCommunicationObject CommunicationObject
        {
            get
            {
                // если канал в ненадлежащем состоянии, то открываем новый
                if (this._communicationObject == null
                    || this._communicationObject.State == CommunicationState.Closed
                    || this._communicationObject.State == CommunicationState.Faulted
                    || this._communicationObject.State == CommunicationState.Closing)
                    this._communicationObject = this.CreateClientInstance();

                return this._communicationObject;
            }
        }

        /// <summary>
        /// Метод создания объекта подключения
        /// </summary>
        /// <returns>Прокси для подулючения к сервису</returns>
        protected virtual TCommunicationObject CreateClientInstance()
        {
            return new TCommunicationObject();
        }

        /// <summary>
        /// Отключаемся от сервера
        /// </summary>
        public void Dispose()
        {
            if (this._communicationObject != null)
                this._communicationObject.Close();
            this._communicationObject = null;
        }
    }
}
