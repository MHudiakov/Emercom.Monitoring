// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WrongFieldTypeOnExternalAttributeObject.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Объект с атрибутом обрезки у поля нестрокового типа
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Test.DataObjects
{
    using Init.DbCore.Processing.Attributes;

    /// <summary>
    /// Объект с атрибутом обрезки у поля нестрокового типа (проверяется генерация исключения)
    /// </summary>
    [ExtDbTrimTo("WrongTypeField", 10)]
    public class WrongFieldTypeOnExternalAttributeObject
    {
        /// <summary>
        /// Нестроковое поле
        /// </summary>
        public int WrongTypeField { get; set; }
    }
}
