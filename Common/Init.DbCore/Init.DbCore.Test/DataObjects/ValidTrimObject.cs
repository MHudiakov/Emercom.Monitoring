// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidTrimObject.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Правильно оформленная модель для валидации обрезки строк
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Test.DataObjects
{
    using Init.DbCore.Processing.Attributes;

    /// <summary>
    /// Правильно оформленная модель для валидации обрезки строк
    /// </summary>
    [ExtDbTrimTo("ExtSimpleProperty", 10)]
    public class ValidTrimObject
    {
        /// <summary>
        /// Свойство для обрезки прямым указанием
        /// </summary>
        [DbTrimTo(10)]
        public string SimpleProperty { get; set; }

        /// <summary>
        /// Сворйство для обрезки указанием внешнего атрибута
        /// </summary>
        public string ExtSimpleProperty { get; set; }
    }
}
