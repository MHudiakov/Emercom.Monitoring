// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WrongFieldTypeOnAttributeObject.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Объект с атрибутом обрезки у поля с нестроковым типом
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Test.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore.Processing.Attributes;

    /// <summary>
    /// Объект с атрибутом обрезки у поля с нестроковым типом
    /// </summary>
    [DataContract]
    public class WrongFieldTypeOnAttributeObject
    {
        /// <summary>
        /// Поле нестрокового типа с атрибутом обрезки
        /// </summary>
        [DbTrimTo(10)]
        public int WrongTypeField { get; set; }
    }
}
