// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelPropertyProcessException.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Ошибка обработки свойства модели
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Ошибка обработки свойства модели
    /// </summary>
    [Serializable]
    public class ModelPropertyValidationException : Exception
    {
        /// <summary>
        /// Имя свойства модели
        /// </summary>
        public string PropertyName { get; private set; }

        /// <summary>
        /// Ошибка обработки модели
        /// </summary>
        /// <param name="message">Собщение об ошибке</param>
        /// <param name="propertyName">Имя свойства модели</param>
        public ModelPropertyValidationException(string message, string propertyName)
            : base(message)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Ошибка обработки модели
        /// </summary>
        /// <param name="message">
        /// Сообщенике об ошибке
        /// </param>
        /// <param name="inner">
        /// Вложенное исключение
        /// </param>
        /// <param name="propertyName">
        /// Имя свойства модели
        /// </param>
        public ModelPropertyValidationException(string message, Exception inner, string propertyName)
            : base(message, inner)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Ошибка обработки модели
        /// </summary>
        /// <param name="info">Сериализованное исключение</param>
        /// <param name="context">Контекст сериализации</param>
        protected ModelPropertyValidationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
            this.PropertyName = info.GetString("PropertyName");
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
            info.AddValue("PropertyName", this.PropertyName);
        }
    }
}