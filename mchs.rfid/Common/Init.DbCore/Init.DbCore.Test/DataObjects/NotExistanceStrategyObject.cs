// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotExistanceStrategyObject.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Объект с атрибутом, не зарегистрированном в обработчике модели
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Test.DataObjects
{
    using Init.DbCore.Test.Mocks.Attributes;

    /// <summary>
    /// Объект с атрибутом, не зарегистрированном в обработчике модели
    /// </summary>
    public class NotExistanceStrategyObject
    {
        /// <summary>
        /// Некоторое существуюущее свойство
        /// </summary>
        [MockNotRegistered]
        public string Foo { get; set; }
    }
}