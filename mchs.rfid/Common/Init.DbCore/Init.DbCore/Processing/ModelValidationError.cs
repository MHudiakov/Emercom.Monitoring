// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelValidationError.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Ошибка валидации свойства модели
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing
{
    using System;

    /// <summary>
    /// Ошибка валидации свойства модели
    /// </summary>
    public class ModelValidationError
    {
        /// <summary>
        /// Ошибка валидации свойства модели
        /// </summary>
        /// <param name="propertyNane">Название свойства</param>
        /// <param name="errorMessage">Сообщение об ошибке</param>
        public ModelValidationError(string propertyNane, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(propertyNane))
                throw new ArgumentNullException("propertyNane");
            this.ErrorMessage = errorMessage;
            this.PropertyNane = propertyNane;
        }

        /// <summary>
        /// Название свойства
        /// </summary>
        public string PropertyNane { get; private set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; private set; }
    }
}