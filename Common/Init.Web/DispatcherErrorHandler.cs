// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DispatcherErrorHandler.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Перехватчик ошибок канала
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Web
{
    using System;
    using System.Net.Sockets;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;

    /// <summary>
    /// Перехватчик ошибок канала
    /// </summary>
    public class DispatcherErrorHandler : IErrorHandler
    {
        #region IErrorHandler Members

        /// <summary>
        /// Enables the creation of a custom <see cref="T:System.ServiceModel.FaultException`1"/> that is returned from an exception in the course of a service method.
        /// </summary>
        /// <param name="error">The <see cref="T:System.Exception"/> object thrown in the course of the service operation.</param><param name="version">The SOAP version of the message.</param><param name="fault">The <see cref="T:System.ServiceModel.Channels.Message"/> object that is returned to the client, or service, in the duplex case.</param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (!(error is FaultException))
            {
                var ex = new FaultException(error.Message);
                ex.Data.Add("ServerStack", this.RecFormatMessage(error));
                var msgFault = ex.CreateMessageFault();
                fault = Message.CreateMessage(version, msgFault, null);
            }
        }

        /// <summary>
        /// Enables error-related processing and returns a value that indicates whether the dispatcher aborts the session and the instance context in certain cases. 
        /// </summary>
        /// <returns>
        /// True if  should not abort the session (if there is one) and instance context if the instance context is not <see cref="F:System.ServiceModel.InstanceContextMode.Single"/>; otherwise, false. The default is false.
        /// </returns>
        /// <param name="error">
        /// The exception thrown during processing.
        /// </param>
        public bool HandleError(Exception error)
        {
            if (this.OnHandleError != null)
                this.OnHandleError(error);

            return true;
        }

        #endregion

        /// <summary>
        /// Рекурсивное форматирование сообщения об ошибке для отправки клиенту
        /// </summary>
        /// <param name="ex">
        /// Сообщение об ошибке
        /// </param>
        /// <returns>
        /// Строка, описывающая сообщение об ошибке
        /// </returns>
        private string RecFormatMessage(Exception ex)
        {
            return ex == null ? string.Empty : string.Format("{0}. {1}", ex.Message, this.RecFormatMessage(ex.InnerException));
        }

        /// <summary>
        /// Событие генерируется при прохождении ошибки по каналу
        /// </summary>
        public event Action<Exception> OnHandleError;
    }
}
