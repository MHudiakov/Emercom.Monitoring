// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbTrimStrategyTest.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Тест cтаретигии и атрибутов DbTrim
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Init.DbCore.Test
{
    using Init.DbCore.Processing;
    using Init.DbCore.Processing.Attributes;
    using Init.DbCore.Test.DataObjects;

    /// <summary>
    /// Тест cтаретигии и атрибутов DbTrim
    /// </summary>
    [TestClass]
    public class DbTrimStrategyTest
    {
        /// <summary>
        /// Тест обработки параметров атрибута DbTrimToAttribute
        /// </summary>
        [TestMethod]
        public void AttributeArgsTest()
        {
            var attr = new DbTrimToAttribute(10);
            Assert.AreEqual(1, attr.Args.Count, "Неверное количество аргументов");
            Assert.IsTrue(attr.Args.ContainsKey("Lenght"), "Неверное название параметра");
            Assert.AreEqual(10, attr.Args["Lenght"], "Неверное значение параметра");
        }

        /// <summary>
        /// Тест обработки параметров атрибута ExtDbTrimTo
        /// </summary>
        [TestMethod]
        public void ExtAttributeArgsTest()
        {
            var attr = new ExtDbTrimTo("SomeField", 10);
            Assert.AreEqual("SomeField", attr.PropertyName, "Неверное имя поля");
            Assert.AreEqual(1, attr.Args.Count, "Неверное количество аргументов");
            Assert.IsTrue(attr.Args.ContainsKey("Lenght"), "Неверное название параметра");
            Assert.AreEqual(10, attr.Args["Lenght"], "Неверное значение параметра");
        }

        /// <summary>
        /// Проверка случая : при котором аттрибут выставлен у поля тип которого не является string'ом
        /// </summary>
        [TestMethod]
        public void CheckAttributeInIntField()
        {
            try
            {
                var strategy = new ModelTrimStrategy<WrongFieldTypeOnAttributeObject>();
                ModelProcessor<WrongFieldTypeOnAttributeObject>.Active.RegisterStrategy<DbTrimToAttribute>(strategy);
                Assert.Fail("Не сгенерировано исключение : Атрибут выставлен у поля с типом отличающимся от sting");
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Проверка случая : при котором аттрибут выставлен у поля тип которого не является string'ом у внешнего объекта
        /// </summary>
        [TestMethod]
        public void CheckExtAttibuteInIntField()
        {
            try
            {
                var strategy = new ModelTrimStrategy<WrongFieldTypeOnExternalAttributeObject>();
                ModelProcessor<WrongFieldTypeOnExternalAttributeObject>.Active.RegisterStrategy<ExtDbTrimTo>(strategy);
                Assert.Fail("Не сгенерировано исключение : Атрибут выставлен у поля с типом отличающимся от sting у внешнего объекта");
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Проверка обработки свойства при случае количество символов меньше заданного 
        /// </summary>
        [TestMethod]
        public void CheckTrimInShortField()
        {
            var dbObject = new ValidTrimObject();
            const string VALUE = "12345678";
            dbObject.ExtSimpleProperty = VALUE;
            dbObject.SimpleProperty = VALUE;

            ModelProcessor<ValidTrimObject>.Active.Process(dbObject);

            Assert.AreEqual(dbObject.ExtSimpleProperty, VALUE, "Лишние символы не обрезаны т.к. количество символов у поля меньше чем указано в атрибуте.");
            Assert.AreEqual(dbObject.SimpleProperty, VALUE, "Лишние символы не обрезаны т.к. количество символов у поля меньше чем указано в атрибуте");
        }

        /// <summary>
        /// Проверка обработки свойства при случае количество символов больше заданного 
        /// </summary>
        [TestMethod]
        public void CheckTrimField()
        {
            var processor = ModelProcessor<ValidTrimObject>.Active;

            var dbObject = new ValidTrimObject();
            const string VALUE = "1234567890";
            dbObject.ExtSimpleProperty = VALUE;
            dbObject.SimpleProperty = VALUE;
            processor.Process(dbObject);

            Assert.AreEqual(dbObject.ExtSimpleProperty, VALUE, "Лишние символы не обрезаны т.к. количество символов у поля равно указаному значению в атрибуте");
            Assert.AreEqual(dbObject.SimpleProperty, VALUE, "Лишние символы не обрезаны т.к. количество символов у поля равно указаному значению в атрибуте");
        }

        /// <summary>
        /// Проверка обработки свойства при случае количество символов больше заданного 
        /// </summary>
        [TestMethod]
        public void CheckTrimLongField()
        {
            var processor = ModelProcessor<ValidTrimObject>.Active;

            var dbObject = new ValidTrimObject();
            const string VALUE = "Initialize to an appropriate value";
            dbObject.ExtSimpleProperty = VALUE;
            dbObject.SimpleProperty = VALUE;

            processor.Process(dbObject);

            Assert.AreEqual(dbObject.ExtSimpleProperty.Length, 10, "Обрезаны лишние символы обрезаны");
            Assert.AreEqual(dbObject.SimpleProperty.Length, 10, "Обрезаны лишние символы обрезаны");
        }
    }
}
