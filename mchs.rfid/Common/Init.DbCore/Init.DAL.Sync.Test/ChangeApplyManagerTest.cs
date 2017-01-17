// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientChangeManagerTest.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Тест механизма применения изменений к кешу
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Init.DAL.Sync.Common;
using Init.DAL.Sync.Test.DataObjects;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Init.DAL.Sync.Test
{
    using Init.DAL.Sync.Test.Mocks;

    /// <summary>
    /// Тест механизма применения изменений к кешу
    /// </summary>
    [TestClass]
    public class ChangeApplyManagerTest
    {
        #region Environment
        /// <summary>
        /// Менеджер регистрации изменений
        /// </summary>
        private ChangeRegistrationManager _changeRegistrationManager;

        /// <summary>
        /// Менеджер применения изменений к кешу
        /// </summary>
        private ChangeApplyManager _changeApplyManager;

        /// <summary>
        /// Опорная дата генерации изменений
        /// </summary>
        private DateTime _startDate = new DateTime(2012, 11, 20, 12, 20, 0);

        /// <summary>
        /// Иниыиализация тестов
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this._changeRegistrationManager = new ChangeRegistrationManager();
            this._changeApplyManager = new ChangeApplyManager();

            Container.RegisterDatamanager(new MockDataManager());

            this._changeRegistrationManager.RegisterObservable(typeof(ObjectA).Name, Container.DataManager.GetRepository<ObjectA>() as ObjectARepository);
            this._changeRegistrationManager.RegisterObservable(typeof(ObjectB).Name, Container.DataManager.GetRepository<ObjectB>() as ObjectBRepository);

            this._changeApplyManager.RegisterSyncStrategy(Container.DataManager.ObjectsA.Strategy, "ObjectATypeName");
            this._changeApplyManager.RegisterSyncStrategy(Container.DataManager.ObjectsB.Strategy, "ObjectBTypeName");
        }
        #endregion

        /// <summary>
        /// Тест добавления объекта A1
        /// </summary>
        [TestMethod]
        public void AddA1()
        {
            var objectA1 = new ObjectA { Id = 1 };
            this._changeApplyManager.Sync(new List<Change> { new Change(ChangeTypeEnum.Add, "ObjectATypeName", this._startDate.AddSeconds(1), new Dictionary<string, object> { { "Id", objectA1.Id } }, objectA1) });

            Assert.AreEqual(1, Container.DataManager.ObjectsA.GetCount(), "Неверное количество объектов а в списке A после добавления объекта А");
            Assert.AreEqual(0, Container.DataManager.ObjectsB.GetCount(), "Неверное количество объектов b в списке B после добавления объекта А");
            ObjectA actualA = Container.DataManager.ObjectsA.GetAll().Single();
            Assert.AreEqual(objectA1.Id, actualA.Id, "В списке объектов А оказался другой объект");
            Assert.AreEqual(0, actualA.ObjectBList.Count, "Неверное количество объектов В в навигационном свойстве добавленного А");
        }

        /// <summary>
        /// Тест редактирования объекта А1
        /// </summary>
        [TestMethod]
        public void EditA1()
        {
            AddA1();
            var objectA1 = new ObjectA { Id = 1, Name = "Object A1" };
            this._changeApplyManager.Sync(new List<Change> { new Change(ChangeTypeEnum.Edit, "ObjectATypeName", this._startDate.AddSeconds(2), new Dictionary<string, object> { { "Id", objectA1.Id } }, objectA1) });
            Assert.AreEqual(1, Container.DataManager.ObjectsA.GetCount(), "Неверное количество объектов А в списке A после редактирования объекта А");
        }

        /// <summary>
        /// Тест удаления объекта А1
        /// </summary>
        [TestMethod]
        public void SyncTest()
        {
            this.EditA1();
            var objectA1 = new ObjectA() { Id = 1, Name = "Object A1" };
            this._changeApplyManager.Sync(new List<Change>() { new Change(ChangeTypeEnum.Delete, "ObjectATypeName", this._startDate.AddSeconds(3), new Dictionary<string, object> { { "Id", objectA1.Id } }, null) });
            Assert.AreEqual(0, Container.DataManager.ObjectsA.GetCount(), "Неверное количество объектов А в списке A после удаления объекта А");
        }
    }
}
