// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotExistanceFieldExternalAttributeObject.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Объект с атрибутом, указывающим на несуществующее свойство
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Test.DataObjects
{
    using Init.DbCore.Test.Mocks.Attributes;

    /// <summary>
    /// Объект с атрибутом, указывающим на несуществующее свойство
    /// </summary>
    [MockExt("SomeNotExistanceField")]
    public class NotExistanceFieldExternalAttributeObject
    {
        /// <summary>
        /// Некоторое существуюущее свойство
        /// </summary>
        public string Foo { get; set; }
    }
}
