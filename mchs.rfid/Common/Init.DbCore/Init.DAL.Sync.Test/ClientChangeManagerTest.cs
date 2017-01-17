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
    public class ClientChangeManagerTest
    {
        #region Environment
        /// <summary>
        /// Менеджер регистрации изменений
        /// </summary>
        private ChangeRegistrationManager _changeRegistrationManager;

        /// <summary>
        /// Менеджер применения изменений к кешу
        /// </summary>
        private ClientChangeManager _clientChangeManager;

        /// <summary>
        /// Опорная дат агенерации изменений
        /// </summary>
        private DateTime _startDate = new DateTime(2012, 11, 20, 12, 20, 0);

        /// <summary>
        /// Иниыиализация тестов
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            this._changeRegistrationManager = new ChangeRegistrationManager();
            _clientChangeManager = new ClientChangeManager();

            Container.RegisterDatamanager(new MockDataManager());

            this._changeRegistrationManager.RegisterObservable(typeof(ObjectA).Name, Container.DataManager.GetRepository<ObjectA>() as ObjectARepository);
            this._changeRegistrationManager.RegisterObservable(typeof(ObjectB).Name, Container.DataManager.GetRepository<ObjectB>() as ObjectBRepository);

            _clientChangeManager.RegisterSyncStrategy(Container.DataManager.ObjectsA.Strategy, "ObjectATypeName");
            _clientChangeManager.RegisterSyncStrategy(Container.DataManager.ObjectsB.Strategy, "ObjectBTypeName");
        }
        #endregion

        /// <summary>
        /// Тест добавления объекта A1
        /// </summary>
        [TestMethod]
        public void AddA1()
        {
            var objectA1 = new ObjectA { Id = 1 };
            _clientChangeManager.Sync(new List<Change> { new Change(ChangeTypeEnum.Add, "ObjectATypeName", this._startDate.AddSeconds(1), objectA1.GetKeys(), objectA1) });

            Assert.AreEqual(1, Container.DataManager.ObjectsA.Count, "Неверное количество объектов а в списке A после добавления объекта А");
            Assert.AreEqual(0, Container.DataManager.ObjectsB.Count, "Неверное количество объектов b в списке B после добавления объекта А");
            ObjectA actualA = Container.DataManager.ObjectsA.List.Single();
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
            _clientChangeManager.Sync(new List<Change> { new Change(ChangeTypeEnum.Edit, "ObjectATypeName", this._startDate.AddSeconds(2), objectA1.GetKeys(), objectA1) });
            Assert.AreEqual(1, Container.DataManager.ObjectsA.Count, "Неверное количество объектов А в списке A после редактирования объекта А");
        }

        /// <summary>
        /// Тест комплексной синхронизации 
        /// при редактировании структуры A-->>B
        /// </summary>
        [TestMethod]
        public void SyncTest()
        {
            Assert.Inconclusive();
            //#region Операции с объектами ObjectA и ObjectB

            ////Добавление объекта В1
            //var objectB1 = new ObjectB() { ObjectA = objectA1, ObjectAId = objectA1.Id, Id = 1 };
            //objectA1.ObjectBList.Add(objectB1);
            //_changes.Add(new Change(ChangeTypeEnum.Add, "ObjectBTypeName", StartDate.AddSeconds(3), objectB1.Id, objectB1));

            ////Добавление объекта А2
            //var objectA2 = new ObjectA() { Id = 2 };
            //_changes.Add(new Change(ChangeTypeEnum.Add, "ObjectATypeName", StartDate.AddSeconds(4), objectA2.Id, objectA2));

            ////Добавление объекта В2
            //var objectB2 = new ObjectB() { ObjectA = objectA2, ObjectAId = objectA2.Id, Id = 2 };
            //objectA2.ObjectBList.Add(objectB2);
            //_changes.Add(new Change(ChangeTypeEnum.Add, "ObjectBTypeName", StartDate.AddSeconds(5), objectB1.Id, objectB2));

            ////Редактирование объекта В1
            //objectB2.ObjectAId = objectA1.Id;
            //_changes.Add(new Change(ChangeTypeEnum.Edit, "ObjectBTypeName", StartDate.AddSeconds(6), objectB1.Id, objectB2));

            ////Удаление объекта А2
            //_changes.Add(new Change(ChangeTypeEnum.Delete, "ObjectATypeName", StartDate.AddSeconds(7), objectA2.Id, null));

            ////Удаление обекта В1
            //_changes.Add(new Change(ChangeTypeEnum.Delete, "ObjectBTypeName", StartDate.AddSeconds(8), objectB1.Id, null));

            ////Удаление обекта В2
            //_changes.Add(new Change(ChangeTypeEnum.Delete, "ObjectBTypeName", StartDate.AddSeconds(9), objectB2.Id, null));

            ////Удаление обекта A2
            //_changes.Add(new Change(ChangeTypeEnum.Delete, "ObjectATypeName", StartDate.AddSeconds(10), objectA1.Id, null));


            //_clientChangeManager.Sync(_changes.GetRange(2, 1));
            //Assert.AreEqual(1, Container.DataManager.ObjectsA.Count, "Неверное количество объектов а в списке A после добавления объекта А");
            //Assert.AreEqual(1, Container.DataManager.ObjectsB.Count, "Неверное количество объектов b в списке B после добавления объекта А");

            //{
            //    ObjectA actualA = Container.DataManager.ObjectsA.List.Single();
            //    Assert.AreEqual(objectA1.Id, actualA.Id, "В списке объектов А оказался другой объект");

            //    ObjectB actualB = Container.DataManager.ObjectsB.List.Single();
            //    Assert.AreEqual(objectB1.Id, actualB.Id,
            //                    "В списке объектов B оказался другой объект");

            //    Assert.IsNotNull(actualB.ObjectA, "Не заполнена ссылка на объект А");
            //    Assert.AreSame(actualB.ObjectA, actualA, "В объекте В другой объект А");
            //    Assert.AreEqual(1, actualA.ObjectBList.Count, "Добавленный объект В не попал в кеш объекта А");
            //    Assert.AreSame(actualB, actualA.ObjectBList.Single(), "В кеш объекта А попал дургой объект В");
            //}


            //{
            //    _clientChangeManager.Sync(_changes.GetRange(3, 1));
            //    Assert.AreEqual(2, Container.DataManager.ObjectsA.Count, "Не верное количество объектов А после добавления обекта А2");
            //    Assert.AreEqual(1, Container.DataManager.ObjectsB.Count, "Неверное количество объектов B в списке B после добавления объекта А2");
            //    ObjectA actualA = Container.DataManager.ObjectsA.List.Last();
            //    Assert.AreEqual(objectA2.Id, actualA.Id, "В списке объектов А оказался другой объект");
            //    Assert.AreEqual(0, actualA.ObjectBList.Count, "Неверное количество объектов В в навигационном свойстве добавленного А2");
            //}

            //{
            //    _clientChangeManager.Sync(_changes.GetRange(4, 1));
            //    Assert.AreEqual(2, Container.DataManager.ObjectsA.Count, "Неверное количество объектов А после добавления обекта B2");
            //    Assert.AreEqual(2, Container.DataManager.ObjectsB.Count, "Неверное количество объектов b в списке B после добавления объекта B2");
            //    var actualA = Container.DataManager.ObjectsA.List.Last();
            //    var actualB = Container.DataManager.ObjectsB.List.Last();

            //    Assert.AreEqual(objectA2.Id, actualA.Id, "В списке объектов А оказался другой объект");
            //    Assert.AreEqual(objectB2.Id, actualB.Id, "В списке объектов B оказался другой объект");
            //    Assert.IsNotNull(actualB.ObjectA, "Не заполнена ссылка на объект А");
            //    Assert.AreSame(actualB.ObjectA, actualA, "В объекте В другой объект А");
            //    Assert.AreEqual(1, actualA.ObjectBList.Count, "Добавленный объект В не попал в кеш объекта А");
            //    Assert.AreSame(actualB, actualA.ObjectBList.Single(), "В кеш объекта А попал дургой объект В");
            //}

            //{
            //    _clientChangeManager.Sync(_changes.GetRange(5, 1));
            //    var actualA1 = Container.DataManager.ObjectsA.List.First();
            //    var actualA2 = Container.DataManager.ObjectsA.List.Last();
            //    Assert.AreEqual(2, actualA1.ObjectBList.Count, "Неверное количество объектов В в списке объекта А1");
            //    Assert.AreEqual(0, actualA2.ObjectBList.Count, "Неверное количество объектов В в списке объекта А2");
            //    Assert.AreSame(actualA1.ObjectBList.Last(), Container.DataManager.ObjectsB.List.Last(), "В списке объектов B объектаА1 оказался другой объект");
            //    Assert.AreSame(Container.DataManager.ObjectsB.List.Last().ObjectA, actualA1, "В объекте В другой объект А");
            //}

            //{

            //    _clientChangeManager.Sync(_changes.GetRange(6, 1));

            //    Assert.AreEqual(1, Container.DataManager.ObjectsA.Count, "Неверное количество объектов А в списке объектов А");
            //    Assert.AreEqual(2, Container.DataManager.ObjectsB.Count, "Неверное количество объектов B в списке объектов B");
            //    var actualB = Container.DataManager.ObjectsB.List.Last();
            //    Assert.IsNotNull(actualB.ObjectA, "Не заполнена ссылка на объект А");
            //    var actualA = Container.DataManager.ObjectsA.List.Single();
            //    Assert.AreSame(actualA, actualB.ObjectA, "В объекте В другой объект А");
            //}

            //{
            //    _clientChangeManager.Sync(_changes.GetRange(7, 1));
            //    Assert.AreEqual(1, Container.DataManager.ObjectsB.Count, "Неверное количество объектов B в списке объектов B");
            //    var actualA = Container.DataManager.ObjectsA.List.Single();
            //    Assert.AreEqual(1, actualA.ObjectBList.Count, "Неверное количество объектов B в списке объектов B объекта А1");
            //}

            //{
            //    _clientChangeManager.Sync(_changes.GetRange(8, 1));
            //    Assert.AreEqual(0, Container.DataManager.ObjectsB.Count, "Неверное количество объектов B в списке объектов B");
            //    var actualA = Container.DataManager.ObjectsA.List.Single();
            //    Assert.AreEqual(0, actualA.ObjectBList.Count, "Неверное количество объектов B в списке объектов B объекта А1");
            //}

            //_clientChangeManager.Sync(_changes.GetRange(9, 1));
            //Assert.AreEqual(0, Container.DataManager.ObjectsA.Count, "Неверное количество объектов A в списке объектов A");
            //#endregion
        }
    }
}
