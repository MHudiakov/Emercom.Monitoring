// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelTrimStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Стратегия Trim для строк
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing.Attributes
{
    using System.Collections.Generic;

    using Init.Tools;

    /// <summary>
    /// Стратегия Trim для строк
    /// </summary>
    /// <typeparam name="T">Тип модели</typeparam>
    internal class ModelTrimStrategy<T> : CustomModelProcessStrategy<T, string>
        where T : class
    {
        /// <summary>
        /// Произвести обработку над полем объекта в соответствии со значение ProcessAttribute
        /// </summary>
        /// <param name="item">Объект над полем которого производится обработка</param>
        /// <param name="property">Данные по свойству над которым производится обработка</param>
        /// <param name="args">Данные по атрибуту</param>
        public override void ProcessOverride(T item, PropertyHelper<T> property, Dictionary<string, object> args)
        {
            var fieldValue = (string)property.Getter(item);

            var tetimLen = args["Lenght"].ToInt();
            if (fieldValue.Length <= tetimLen)
                return;

            fieldValue = fieldValue.Substring(0, tetimLen);
            property.Setter(item, fieldValue);
        }
    }
}