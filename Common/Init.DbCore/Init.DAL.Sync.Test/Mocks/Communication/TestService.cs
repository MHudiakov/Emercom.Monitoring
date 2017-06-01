// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestService.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Тестовый сервис с поддержкой универсальной точки обмена данными
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.Test.Mocks.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.ServiceModel;

    using Init.DAL.Sync.GeneralDataPoint;
    using Init.DAL.Sync.Transfer;

    /// <summary>
    /// Тестовый сервис с поддержкой универсальной точки обмена данными
    /// </summary>
    public class TestService : IGeneralDataPoint, ICommunicationObject
    {
        /// <summary>
        /// Стандартная стратегия универсальной точки обмена данными
        /// </summary>
        private readonly StandartDataPointStrategy _dataPointStrategy;

        /// <summary>
        /// Тестовый сервис с поддержкой универсальной точки обмена данными
        /// </summary>
        public TestService()
        {
            this._dataPointStrategy = new StandartDataPointStrategy();
        }

        /// <summary>
        /// Получить количество элементов
        /// </summary>
        /// <param name="typeName">
        /// Имя шлюза записи
        /// </param>
        /// <returns>
        /// Количество элементов, лоступное через шлюз записи
        /// </returns>
        public long Count(string typeName)
        {
            return this.DataPointStrategy.Count(typeName);
        }

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="item">
        /// Информация о добавляемой записи
        /// </param>
        /// <returns>
        /// Идентификатор добавленной записи
        /// </returns>
        public Change Add(Change item)
        {
            return this.DataPointStrategy.Add(item);
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="item">
        /// Новое состояние записи
        /// </param>
        /// <returns>
        ///  Измененный объект (Возврат изменений сервера)
        /// </returns>
        public Change Edit(Change item)
        {
            return this.DataPointStrategy.Edit(item);
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="item">
        /// Новое состояние записи
        /// </param>
        public void DeleteWhere(Change item)
        {
            this.DataPointStrategy.DeleteWhere(item);
        }

        /// <summary>
        /// Получение части записей по фильтру
        /// </summary>
        /// <param name="typeName">
        /// Имя шлюза записи
        /// </param>
        /// <param name="keys">
        /// Улюч-фильтр
        /// </param>
        /// <returns>
        /// Часть объектов шлюза таблицы, проходищих фильтр keys
        /// </returns>
        public List<Change> GetItemsWhere(string typeName, Dictionary<string, object> keys)
        {
            return this.DataPointStrategy.GetItemsWhere(typeName, keys);
        }

        /// <summary>
        /// Получение данных посредством TransferManager
        /// </summary>
        /// <param name="typeName">Имя шлюза записи</param>
        /// <param name="ident">Идентификатор части</param>
        /// <returns>Часть данных со списка</returns>
        public TransferPart TransferItems(string typeName, TransferPartIdent ident)
        {
            return this.DataPointStrategy.TransferItems(typeName, ident);
        }

        /// <summary>
        /// Получение части объектов
        /// </summary>
        /// <param name="typeName">
        /// Имя шлюза записи
        /// </param>
        /// <param name="pageIndex">
        /// Номер страницы
        /// </param>
        /// <param name="count">
        /// Количество объектов
        /// </param>
        /// <returns>
        /// Список объектов начиная с idFrom длинной не более count
        /// </returns>
        public List<Change> GetPage(string typeName, int pageIndex, int count)
        {
            return this.DataPointStrategy.GetPage(typeName, pageIndex, count);
        }

        #region Implementation of ICommunicationObject

        /// <summary>
        /// Causes a communication object to transition immediately from its current state into the closed state.  
        /// </summary>
        public void Abort()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Causes a communication object to transition from its current state into the closed state.  
        /// </summary>
        /// <exception cref="T:System.ServiceModel.CommunicationObjectFaultedException"><see cref="M:System.ServiceModel.ICommunicationObject.Close"/> was called on an object in the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The default close timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to close gracefully.</exception>
        public void Close()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Causes a communication object to transition from its current state into the closed state.  
        /// </summary>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the send operation has to complete before timing out.</param><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException"><see cref="M:System.ServiceModel.ICommunicationObject.Close"/> was called on an object in the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to close gracefully.</exception>
        public void Close(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins an asynchronous operation to close a communication object.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous close operation. 
        /// </returns>
        /// <param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives notification of the completion of the asynchronous close operation.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous close operation.</param><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException"><see cref="M:System.ServiceModel.ICommunicationObject.BeginClose"/> was called on an object in the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The default timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to close gracefully.</exception>
        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins an asynchronous operation to close a communication object with a specified timeout.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous close operation.
        /// </returns>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the send operation has to complete before timing out.</param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives notification of the completion of the asynchronous close operation.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous close operation.</param><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException"><see cref="M:System.ServiceModel.ICommunicationObject.BeginClose"/> was called on an object in the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The specified timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to close gracefully.</exception>
        public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Completes an asynchronous operation to close a communication object.
        /// </summary>
        /// <param name="result">The <see cref="T:System.IAsyncResult"/> that is returned by a call to the <see cref="M:System.ServiceModel.ICommunicationObject.BeginClose"/> method.</param><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException"><see cref="M:System.ServiceModel.ICommunicationObject.BeginClose"/> was called on an object in the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to close gracefully.</exception>
        public void EndClose(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Causes a communication object to transition from the created state into the opened state.  
        /// </summary>
        /// <exception cref="T:System.ServiceModel.CommunicationException">The <see cref="T:System.ServiceModel.ICommunicationObject"/> was unable to be opened and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The default open timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to enter the <see cref="F:System.ServiceModel.CommunicationState.Opened"/> state and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception>
        public void Open()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Causes a communication object to transition from the created state into the opened state within a specified interval of time.
        /// </summary>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the send operation has to complete before timing out.</param><exception cref="T:System.ServiceModel.CommunicationException">The <see cref="T:System.ServiceModel.ICommunicationObject"/> was unable to be opened and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The specified timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to enter the <see cref="F:System.ServiceModel.CommunicationState.Opened"/> state and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception>
        public void Open(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins an asynchronous operation to open a communication object.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous open operation. 
        /// </returns>
        /// <param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives notification of the completion of the asynchronous open operation.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous open operation.</param><exception cref="T:System.ServiceModel.CommunicationException">The <see cref="T:System.ServiceModel.ICommunicationObject"/> was unable to be opened and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The default open timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to enter the <see cref="F:System.ServiceModel.CommunicationState.Opened"/> state and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception>
        public IAsyncResult BeginOpen(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins an asynchronous operation to open a communication object within a specified interval of time.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous open operation. 
        /// </returns>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the send operation has to complete before timing out.</param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives notification of the completion of the asynchronous open operation.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous open operation.</param><exception cref="T:System.ServiceModel.CommunicationException">The <see cref="T:System.ServiceModel.ICommunicationObject"/> was unable to be opened and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The specified timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to enter the <see cref="F:System.ServiceModel.CommunicationState.Opened"/> state and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception>
        public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Completes an asynchronous operation to open a communication object.
        /// </summary>
        /// <param name="result">The <see cref="T:System.IAsyncResult"/> that is returned by a call to the <see cref="M:System.ServiceModel.ICommunicationObject.BeginOpen"/> method.</param><exception cref="T:System.ServiceModel.CommunicationException">The <see cref="T:System.ServiceModel.ICommunicationObject"/> was unable to be opened and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to enter the <see cref="F:System.ServiceModel.CommunicationState.Opened"/> state and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception>
        public void EndOpen(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the current state of the communication-oriented object.
        /// </summary>
        /// <returns>
        /// The value of the <see cref="T:System.ServiceModel.CommunicationState"/> of the object.
        /// </returns>
        public CommunicationState State { get; private set; }

        /// <summary>
        /// Стандартная стратегия универсальной точки обмена данными
        /// </summary>
        public StandartDataPointStrategy DataPointStrategy
        {
            get
            {
                return this._dataPointStrategy;
            }
        }

        /// <summary>
        /// Occurs when the communication object completes its transition from the closing state into the closed state.
        /// </summary>
        public event EventHandler Closed;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected virtual void OnClosed()
        {
            var handler = this.Closed;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when the communication object first enters the closing state.
        /// </summary>
        public event EventHandler Closing;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected virtual void OnClosing()
        {
            var handler = this.Closing;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when the communication object first enters the faulted state.
        /// </summary>
        public event EventHandler Faulted;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected virtual void OnFaulted()
        {
            var handler = this.Faulted;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when the communication object completes its transition from the opening state into the opened state.
        /// </summary>
        public event EventHandler Opened;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected virtual void OnOpened()
        {
            var handler = this.Opened;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when the communication object first enters the opening state.
        /// </summary>
        public event EventHandler Opening;

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected virtual void OnOpening()
        {
            var handler = this.Opening;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion
    }
}