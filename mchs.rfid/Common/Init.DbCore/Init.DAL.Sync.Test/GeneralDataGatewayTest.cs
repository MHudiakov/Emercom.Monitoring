// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneralDataGatewayTest.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Init.DAL.Sync.Test
{
    using System.Collections.Generic;
    using System.Linq;

    using Init.DAL.Sync.GeneralDataPoint;
    using Init.DAL.Sync.Test.DataObjects;
    using Init.DAL.Sync.Test.Mocks;
    using Init.DAL.Sync.Test.Mocks.Communication;
    using Init.DbCore.Wcf;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Тест универсальной точки обмена данными
    /// </summary>
    [TestClass]
    public class GeneralDataGatewayTest
    {
        #region Properties

        /// <summary>
        /// Шлюз доступа к данным
        /// </summary>
        private GeneralDataGateway<ObjectA, TestService> _dataGateway;

        /// <summary>
        /// Менеджер данных
        /// </summary>
        private MockDataManager _dataManager;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Проверка метода добавления
        /// </summary>
        [TestMethod]
        public void AddTest()
        {
            var excepted = new ObjectA { Name = "object a1" };
            this._dataGateway.Add(excepted);
            Assert.AreEqual(1, excepted.Id, "Не сгенерировался идентификатор объекта");
            Assert.AreEqual(1, this._dataManager.ObjectsA.GetCount(), "Объект не добавился в репозиторий");
            ObjectA actual = this._dataManager.ObjectsA.Get(excepted.Id);
            Assert.IsNotNull(actual, "Объект неправильно добавился в репозиторий");
        }

        /// <summary>
        /// Тест получения количества объектов
        /// </summary>
        [TestMethod]
        public void CountTest()
        {
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name1" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name2" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name3" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name4" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name5" });

            long actual = this._dataGateway.GetCount();
            Assert.AreEqual(5, actual, "Неверное колчество элементов в репозитории");
        }

        /// <summary>
        /// Тест метода удаления
        /// </summary>
        [TestMethod]
        public void DeleteTest()
        {
            var excepted = new ObjectA { Name = "object a1" };
            this._dataManager.ObjectsA.Add(excepted);
            this._dataGateway.Delete(excepted.Id);
            Assert.AreEqual(0, this._dataManager.ObjectsA.GetCount(), "Объект не удалился из репозитория");
        }

        /// <summary>
        /// Тест редактирования
        /// </summary>
        [TestMethod]
        public void EditTest()
        {
            var excepted = new ObjectA { Name = "object a1" };
            this._dataManager.ObjectsA.Add(excepted);
            ObjectA edited = this._dataGateway.Get(excepted.Id);
            edited.Name = "newName";
            this._dataGateway.Edit(edited);
            Assert.AreEqual(excepted.Name, edited.Name, "Не отредактировалось поле");
        }

        /// <summary>
        /// Тест получения объектов по ключу
        /// </summary>
        [TestMethod]
        public void GetItemsByTest()
        {
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name1" });

            var excepted = new ObjectA { Name = "name1", Id = 1 };
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name2" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name2" });

            List<ObjectA> items = this._dataGateway.GetItemsWhere(a => a.Name, "name2");
            Assert.AreEqual(2, items.Count, "Неверное количество объектов в результируюущем наборе");
            foreach (ObjectA item in items)
                Assert.AreEqual("name2", item.Name, "Неверный элемент в результируюущем наборе");

            ObjectA actual = this._dataGateway.GetItemsWhere(new Dictionary<string, object> { { "Id", excepted.Id } }).SingleOrDefault();
            Assert.IsNotNull(actual, "Не получено значение");
            Assert.IsTrue(excepted.PropEqualas(actual.GetProperties()), "Неверное значение в результируюущем наборе");
        }

        /// <summary>
        /// Тест получения списка объектов по частям
        /// </summary>
        [TestMethod]
        public void GetPageTest()
        {
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name1" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name2" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name3" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name4" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name5" });

            List<ObjectA> items = this._dataGateway.GetPage(0, 2);
            Assert.AreEqual(2, items.Count, "Неверное количество элементов в результируюущем наборе");
            items = this._dataGateway.GetPage(0, 4);
            Assert.AreEqual(4, items.Count, "Неверное количество элементов в результируюущем наборе");
            items = this._dataGateway.GetPage(1, 4);
            Assert.AreEqual(1, items.Count, "Неверное количество элементов в результируюущем наборе");
        }

        /// <summary>
        /// Тест получения списка всех объектов
        /// </summary>
        [TestMethod]
        public void ListTest()
        {
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name1" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name2" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name3" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name4" });
            this._dataManager.ObjectsA.Add(new ObjectA { Name = "name5" });

            List<ObjectA> actual = this._dataGateway.GetAll();
            Assert.AreEqual(5, actual.Count, "Неверное колчество элементов в результируюущем наборе.");
            foreach (ObjectA objectA in actual)
                Assert.IsTrue(actual.Exists(item => item.Equals(objectA)), "Неверный состав результируюущего набора");
        }

        /// <summary>
        /// Инициализация теста
        /// </summary>
        [TestInitialize]
        public void MyTestInitialize()
        {
            this._dataManager = new MockDataManager();

            var context = new WcfContextBase<TestService>();
            context.CommunicationObject.DataPointStrategy.RegisterDataAccess(this._dataManager.ObjectsA);
            this._dataGateway = new GeneralDataGateway<ObjectA, TestService>(() => context);
        }

        #endregion
    }
}