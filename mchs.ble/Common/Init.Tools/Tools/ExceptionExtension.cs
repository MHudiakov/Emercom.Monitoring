// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionExtension.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Расширение для класса Exception
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;

    /// <summary>
    /// Расширение для класса Exception
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// Добавляет доп. данные в коллекцию данных
        /// </summary>
        /// <param name="ex">Сообщение об ошибке</param>
        /// <param name="key">Ключ данных</param>
        /// <param name="value">Значение</param>
        /// <returns>Исходное сообщение с обновленной коллекцией данных</returns>
        public static Exception AddData(this Exception ex, object key, object value)
        {
            ex.Data.Add(key, value);
            return ex;
        }
    }
}