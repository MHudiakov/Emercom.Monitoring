// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelProcessException.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Ошибка обработки модели
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Ошибка обработки модели
    /// </summary>
    [Serializable]
    public class ModelProcessException : Exception
    {
        /// <summary>
        /// Ошибки валидации модели
        /// </summary>
        public List<ModelValidationError> ModelErrors { get; private set; }

        /// <summary>
        /// Ошибка обработки модели
        /// </summary>
        /// <param name="modelErrors">
        /// Ошибки валидации модели
        /// </param>
        public ModelProcessException(List<ModelValidationError> modelErrors = null)
        {
            if (modelErrors == null)
                throw new ArgumentNullException("modelErrors");
            this.ModelErrors = modelErrors;
        }

        /// <summary>
        /// Ошибка обработки модели
        /// </summary>
        /// <param name="message">
        /// Сообщение об ошибке
        /// </param>
        /// <param name="modelErrors">
        /// Ошибки валидации модели
        /// </param>
        public ModelProcessException(string message, List<ModelValidationError> modelErrors = null)
            : base(message)
        {
            if (modelErrors == null)
                throw new ArgumentNullException("modelErrors");
            this.ModelErrors = modelErrors;
        }

        /// <summary>
        /// Ошибка обработки модели
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="inner">Вложенное исключение</param>
        public ModelProcessException(string message, Exception inner)
            : base(message, inner)
        {
            this.ModelErrors = new List<ModelValidationError>();
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown. </param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination. </param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic). </exception>
        protected ModelProcessException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
            this.ModelErrors = (List<ModelValidationError>)info.GetValue("ModelErrors", typeof(List<ModelValidationError>));
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown. </param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination. </param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic). </exception>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ModelErrors", this.ModelErrors);
        }

        /// <summary>
        /// Возвращает текстовое описание ошибок валидации
        /// </summary>
        /// <returns>Ошибки валидации по свойствам модели</returns>
        public override string ToString()
        {
            return ModelErrors.Aggregate("Ошибки валидации:\r\n", (c, n) => string.Format("{0}.\r\n{1}: {2}", c, n.PropertyNane, n.ErrorMessage.Trim('.')));
        }
    }
}