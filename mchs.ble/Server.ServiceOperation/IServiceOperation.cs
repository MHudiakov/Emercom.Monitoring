// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceOperation.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Интерфейс WCF сервиса
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    using DAL.SQL.DataObjects;

    using Init.DAL.Sync;
    using Init.DAL.Sync.Transfer;
    using Init.DbCore;
    using Server.Dal.SQL.DataObjects;

    /// <summary>
    /// Интерфейс WCF сервиса
    /// </summary>
    [ServiceContract]
    [ServiceKnownType(typeof(Group))]
    [ServiceKnownType(typeof(kEquipment))]
    [ServiceKnownType(typeof(Equipment))]
    [ServiceKnownType(typeof(Movement))]
    [ServiceKnownType(typeof(DbObject))]
    [ServiceKnownType(typeof(Tag))]
    [ServiceKnownType(typeof(GeoPoints))]
    [ServiceKnownType(typeof(Store))]
    [ServiceKnownType(typeof(Trip))]
    [ServiceKnownType(typeof(TripComplectation))]
    public interface IServiceOperation
    {
        /// <summary>
        /// Метод проверки соединения с сервером
        /// </summary>
        [OperationContract]
        void TestConnection();

        #region kEquipment

        /// <summary>
        /// Добавить классификатора оборудования
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора оборудования
        /// </param>
        [OperationContract]
        void AddkEquipment(kEquipment item);

        /// <summary>
        /// Редактировать классификатора оборудования
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора оборудования
        /// </param>
        [OperationContract]
        void EditkEquipment(kEquipment item);

        /// <summary>
        /// Удалить классификатора оборудования
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора оборудования
        /// </param>
        [OperationContract]
        void DeletekEquipment(kEquipment item);

        /// <summary>
        /// Получить список классификаторов оборудования
        /// </summary>
        /// <returns>
        /// Список классификаторов оборудования
        /// </returns>
        [OperationContract]
        List<kEquipment> GetAllkEquipment();

        #endregion

        #region Group

        /// <summary>
        /// Добавить группу оборудования
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        [OperationContract]
        void AddGroup(Group item);

        /// <summary>
        /// Редактировать группу оборудование
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        [OperationContract]
        void EditGroup(Group item);

        /// <summary>
        /// Удалить группу оборудование
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        [OperationContract]
        void DeleteGroup(Group item);

        /// <summary>
        /// Получить весь список групп оборудования
        /// </summary>
        /// <returns>
        /// Группа оборудования
        /// </returns>
        [OperationContract]
        List<Group> GetAllGroup();

        #endregion

        #region Store

        /// <summary>
        /// Добавить базу
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        [OperationContract]
        void AddStore(Store item);

        /// <summary>
        /// Редактировать базу
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        [OperationContract]
        void EditStore(Store item);

        /// <summary>
        /// Удалить базу
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        [OperationContract]
        void DeleteStore(Store item);

        /// <summary>
        /// Получить весь список баз
        /// </summary>
        /// <returns>
        /// Группа оборудования
        /// </returns>
        [OperationContract]
        List<Store> GetAllStore();

        #endregion

        #region Trip


        /// <summary>
        /// Получить весь список поездок
        /// </summary>
        /// <returns>
        /// Группа оборудования
        /// </returns>
        [OperationContract]
        List<Trip> GetAllTrips();

        #endregion

        #region TripComplectation

        /// <summary>
        /// Получить весь список коплектации в поездке
        /// </summary>
        /// <returns>
        /// Группа оборудования
        /// </returns>
        [OperationContract]
        List<TripComplectation> GetAllTripComplectations();

        #endregion

        #region Equipment

        /// <summary>
        /// Добавить оборудование
        /// </summary>
        /// <param name="item">
        /// Оборудование
        /// </param>
        [OperationContract]
        void AddEquipment(Equipment item);

        /// <summary>
        /// Редактировать оборудование
        /// </summary>
        /// <param name="item">
        /// Оборудование
        /// </param>
        [OperationContract]
        void EditEquipment(Equipment item);

        /// <summary>
        /// Удалить оборудование
        /// </summary>
        /// <param name="item">
        /// Оборудование
        /// </param>
        [OperationContract]
        void DeleteEquipment(Equipment item);

        /// <summary>
        /// Получить список оборудования
        /// </summary>
        /// <returns>
        /// Список оборудования
        /// </returns>
        [OperationContract]
        List<Equipment> GetAllEquipment();

        /// <summary>
        /// Получить комплектацию машины во время конкретной поездке
        /// </summary>
        /// <param name="tripId">
        /// Ид поездки
        /// </param>
        /// <param name="isStart">
        /// Флаг, показывает, в начале или конце поездки получаем комплектацию
        /// </param>
        /// <returns>
        /// Список оборудования
        /// </returns>
        [OperationContract]
        List<Equipment> GetComplectationByTripId(int tripId, bool isStart);

        #endregion

        #region Movement

        /// <summary>
        /// Добавить движение оборудования
        /// </summary>
        /// <param name="item">
        /// Движение оборудования
        /// </param>
        [OperationContract]
        void AddMovement(Movement item);

        /// <summary>
        /// Получить список последних передвижений
        /// </summary>
        /// <param name="item">
        /// Движение оборудования
        /// </param>
        [OperationContract]
        List<Movement> GetLastMovements();

        /// <summary>
        /// Редактировать движение оборудования
        /// </summary>
        /// <param name="item">
        /// Движение оборудования
        /// </param>
        [OperationContract]
        void EditMovement(Movement item);

        /// <summary>
        /// Удалить движение оборудования
        /// </summary>
        /// <param name="item">
        /// Движение оборудования
        /// </param>
        [OperationContract]
        void DeleteMovement(Movement item);

        /// <summary>
        /// Получить список перемещений оборудования
        /// </summary>
        /// <returns>
        /// Список движений
        /// </returns>
        [OperationContract]
        List<Movement> GetAllMovement();

        /// <summary>
        /// Метод получения списка передвижений по ид поездки
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Movement> GetMovementListByTripId(int tripId);

        /// <summary>
        /// Полчения списка передвижений оборудования
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Movement> GetMovementListByEquipmentId(int? equipmentId);

        /// <summary>
        /// Получение списка передвижений оборудования по объекту и времени
        /// </summary>
        /// <param name="minTime"></param>
        /// <param name="maxTime"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Movement> GetMovementListByTimeAndUnitId(DateTime minTime, DateTime maxTime, int unitId);

        /// <summary>
        /// Получение передвижения по ид
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Movement GetMovement(int? id);

        #endregion

        #region GeoPoints

        /// <summary>
        /// Добавить гео метку
        /// </summary>
        /// <param name="item">
        /// Движение оборудования
        /// </param>
        [OperationContract]
        void AddGeoPoint(GeoPoints item);

        /// <summary>
        /// Получить список гео меток
        /// </summary>
        /// <returns>
        /// Список движений
        /// </returns>
        [OperationContract]
        List<GeoPoints> GetAllGeoPoints();


        /// <summary>
        /// Получить список координат по промежутку времени и номеру объекта
        /// </summary>
        /// <returns>
        /// Список движений
        /// </returns>
        [OperationContract]
        List<GeoPoints> GetGeoPointListByTime(int itemId, DateTime minTime, DateTime maxTime);

        /// <summary>
        /// Получить список координат по поездке
        /// </summary>
        /// <returns>
        /// Список движений
        /// </returns>
        [OperationContract]
        List<GeoPoints> GetGeoPointListByTripId(int tripId, int idFrom, int count);

        #endregion

        #region Tag

        /// <summary>
        /// Добавить тег
        /// </summary>
        /// <param name="item">
        /// Тег
        /// </param>
        [OperationContract]
        void AddTag(Tag item);

        /// <summary>
        /// Редактировать тег
        /// </summary>
        /// <param name="item">
        /// Тег
        /// </param>
        [OperationContract]
        void EditTag(Tag item);

        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="item">
        /// Тег
        /// </param>
        [OperationContract]
        void DeleteTag(Tag item);

        /// <summary>
        /// Получить список тегов
        /// </summary>
        /// <returns>
        /// Список тегов
        /// </returns>
        [OperationContract]
        List<Tag> GetAllTags();

        #endregion

        #region kObject

        /// <summary>
        /// Добавление классификатора типа объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора типа объекта
        /// </param>
        [OperationContract]
        void AddkObject(kObject item);

        /// <summary>
        /// Редактирование классификатора типа объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора типа объекта
        /// </param>
        [OperationContract]
        void EditkObject(kObject item);

        /// <summary>
        /// Удаление классификатора типа объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора типа объекта
        /// </param>
        [OperationContract]
        void DeletekObject(kObject item);

        /// <summary>
        /// Получение списка классификаторов типов объектов
        /// </summary>
        /// <returns>
        /// Список классификаторов типов объектов
        /// </returns>
        [OperationContract]
        List<kObject> GetAllkObject();

        #endregion

        #region Unit

        /// <summary>
        /// Добавление классификатора объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора объекта
        /// </param>
        [OperationContract]
        void AddUnit(Unit item);

        /// <summary>
        /// Редактирование классификатора объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора объекта
        /// </param>
        [OperationContract]
        void EditUnit(Unit item);

        /// <summary>
        /// Удаление классификатора объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора объекта
        /// </param>
        [OperationContract]
        void DeleteUnit(Unit item);

        /// <summary>
        /// Получение списка классификаторов объектов
        /// </summary>
        /// <returns>
        /// Список классификаторов объектов
        /// </returns>
        [OperationContract]
        List<Unit> GetAllUnit();

        /// <summary>
        /// Получение списка оборудования в объекте
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        [OperationContract]
        List<Equipment> GetСomplectationListByUnitId(int unitId);

        /// <summary>
        /// Получение последней поездки
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        [OperationContract]
        Trip GetLastTrip(int unitId);

        #endregion

        #region UniqEquipmentObject

        /// <summary>
        /// Добавление классификатора уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора уникального оборудования для машины
        /// </param>
        [OperationContract]
        void AddUniqEquipmentObject(UniqEquipmentObject item);

        /// <summary>
        /// Редактирование классификатора уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора уникального оборудования для машины
        /// </param>
        [OperationContract]
        void EditUniqEquipmentObject(UniqEquipmentObject item);

        /// <summary>
        /// Удаление классификатора уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора уникального оборудования для машины
        /// </param>
        [OperationContract]
        void DeleteUniqEquipmentObject(UniqEquipmentObject item);

        /// <summary>
        /// Получение списка классификаторов уникального оборудования для машины
        /// </summary>
        /// <returns>
        /// Список классификаторов уникального оборудования для машины
        /// </returns>
        [OperationContract]
        List<UniqEquipmentObject> GetAllUniqEquipmentObject();

        /// <summary>
        /// Получение списка классификаторов уникального оборудования для машины 
        /// </summary>
        /// <returns>
        /// Список классификаторов уникального оборудования для машины
        /// </returns>
        [OperationContract]
        List<UniqEquipmentObject> GetUniqEquipmentObjectListByUnitId(int unitId);

        #endregion

        #region NonUniqEquipmentObject

        /// <summary>
        /// Добавление классификатора не уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора не уникального оборудования для машины
        /// </param>
        [OperationContract]
        void AddNonUniqEquipmentObject(NonUniqEquipmentObject item);

        /// <summary>
        /// Редактирование классификатора не уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора не уникального оборудования для машины
        /// </param>
        [OperationContract]
        void EditNonUniqEquipmentObject(NonUniqEquipmentObject item);

        /// <summary>
        /// Удаление классификатора не уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора не уникального оборудования для машины
        /// </param>
        [OperationContract]
        void DeleteNonUniqEquipmentObject(NonUniqEquipmentObject item);

        /// <summary>
        /// Получение списка классификаторов не уникального оборудования для машины
        /// </summary>
        /// <returns>
        /// Список классификаторов не уникального оборудования для машины
        /// </returns>
        [OperationContract]
        List<NonUniqEquipmentObject> GetAllNonUniqEquipmentObject();

        /// <summary>
        /// Получение списка классификаторов неуникального оборудования для машины 
        /// </summary>
        /// <returns>
        /// Список классификаторов уникального оборудования для машины
        /// </returns>
        [OperationContract]
        List<NonUniqEquipmentObject> GetNonUniqEquipmentListObjectByUnitId(int unitId);
        #endregion

        #region GeneralDataPoint

        /// <summary>
        /// Получить количество элементов
        /// </summary>
        /// <param name="typeName">
        /// Имя шлюза записи
        /// </param>
        /// <returns>
        /// Количество элементов, доступное через шлюз записи
        /// </returns>
        [OperationContract]
        long Count(string typeName);

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="item">
        /// Информация о добавляемой записи
        /// </param>
        /// <returns>
        /// Измененный объект, если есть автогенерируемые поля
        /// </returns>
        [OperationContract]
        Change Add(Change item);

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="item">
        /// Новое состояние записи
        /// </param>
        /// <returns>
        ///  Измененный объект (Возврат изменений сервера)
        /// </returns>
        [OperationContract]
        Change Edit(Change item);

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="item">
        /// Новое состояние записи
        /// </param>
        [OperationContract]
        void DeleteWhere(Change item);

        /// <summary>
        /// Получение части записей по фильтру
        /// </summary>
        /// <param name="typeName">
        /// Имя шлюза записи
        /// </param>
        /// <param name="keys">
        /// Улюч-фильтр
        /// </param>
        /// <returns>
        /// Часть объектов шлюза таблицы, проходищих фильтр keys
        /// </returns>
        [OperationContract]
        List<Change> GetItemsWhere(string typeName, Dictionary<string, object> keys);

        /// <summary>
        /// Получение данных посредством TransferManager
        /// </summary>
        /// <param name="typeName">Имя шлюза записи</param>
        /// <param name="ident">Идентификатор части</param>
        /// <returns>Часть данных со списка</returns>
        [OperationContract]
        TransferPart TransferItems(string typeName, TransferPartIdent ident);

        /// <summary>
        /// Получение части объектов
        /// </summary>
        /// <param name="typeName">
        /// Имя шлюза записи
        /// </param>
        /// <param name="pageIndex">
        /// Номер страницы
        /// </param>
        /// <param name="count">
        /// Количество объектов
        /// </param>
        /// <returns>
        /// Список объектов начиная с idFrom длинной не более count
        /// </returns>
        [OperationContract]
        List<Change> GetPage(string typeName, int pageIndex, int count);

        #endregion
    }
}