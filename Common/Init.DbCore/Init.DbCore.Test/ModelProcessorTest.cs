// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModelProcessorTest.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Проверка базовй функциональности валидатора модели
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Init.DbCore.Test
{
    using Init.DbCore.Processing;
    using Init.DbCore.Test.DataObjects;

    /// <summary>
    /// Проверка базовй функциональности валидатора модели
    /// </summary>
    [TestClass]
    public class ModelProcessorTest
    {
        // todo: Model mulitple error generation test

        /// <summary>
        /// Проверяем генерацию исключения при отсутствии свойства, указанного во внешнем атрибуте
        /// </summary>
        [TestMethod]
        public void ExternalAttributesForNonMemberFieldsTest()
        {
            try
            {
                var processor = ModelProcessor<NotExistanceFieldExternalAttributeObject>.Active;
                Assert.Fail("Не сгенерировано исключение отсутствия свойства, указанного в внешнем атрибуте объекта.");
            }
            catch (InvalidOperationException)
            {
            }
        }

        /// <summary>
        /// Проверяем ганерерацию исключения для атрибута не сопоставленного со стратегией
        /// </summary>
        [TestMethod]
        public void NotRegisteredStrategyTest()
        {
            try
            {
                var processor = ModelProcessor<NotExistanceStrategyObject>.Active;
                processor.Process(new NotExistanceStrategyObject());
                Assert.Fail("Не сгенерировано исключение о незарегистрированной стратегии для атрибута MockNotRegisteredAttribute");
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
