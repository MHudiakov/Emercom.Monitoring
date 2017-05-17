// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceOperation.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Сервис управления оборудованием
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Xml;

    using DAL;

    using Init.DAL.Sync;
    using Init.DAL.Sync.GeneralDataPoint;
    using Init.DAL.Sync.Transfer;
    using Init.Tools.GPS;
    using Init.Web;
    using Server.Dal.SQL.DataObjects;
    using PointLocalisation;

    using Server.Dal;
    using Server.Dal.Sql.DataObjects;

    /// <summary>
    /// WCF сервис
    /// </summary>
    public class ServiceOperation : BaseWebService, IServiceOperation, IGeneralDataPoint
    {
        #region Конструктор и инициализация

        /// <summary>
        /// Стандартная стратегия универсальной точки обмена данными
        /// </summary>
        private readonly StandartDataPointStrategy _standartDataPointStrategy;

        /// <summary>
        /// Сервис управления сервером 
        /// </summary>
        public ServiceOperation()
            : base(typeof(ServiceOperation))
        {
            _standartDataPointStrategy = new StandartDataPointStrategy();
            _standartDataPointStrategy.RegisterDataAccess(DalContainer.DataManager.kEquipmentRepository);
            _standartDataPointStrategy.RegisterDataAccess(DalContainer.DataManager.EquipmentRepository);
            _standartDataPointStrategy.RegisterDataAccess(DalContainer.DataManager.EquipmentGroupRepository);
            _standartDataPointStrategy.RegisterDataAccess(DalContainer.DataManager.MovementRepository);
        }

        #endregion

        /// <summary>
        /// Метод проверки соединения с сервером
        /// </summary>
        public void TestConnection()
        {
        }

        #region kEquipment

        /// <summary>
        /// Добавить классификатора оборудования
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора оборудования
        /// </param>
        public void AddkEquipment(KEquipment item)
        {
            DalContainer.DataManager.kEquipmentRepository.Add(item);
        }

        /// <summary>
        /// Редактировать классификатора оборудования
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора оборудования
        /// </param>
        public void EditkEquipment(KEquipment item)
        {
            DalContainer.DataManager.kEquipmentRepository.Edit(item);
        }

        /// <summary>
        /// Удалить классификатора оборудования
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора оборудования
        /// </param>
        public void DeletekEquipment(KEquipment item)
        {
            DalContainer.DataManager.kEquipmentRepository.Delete(item);
        }

        /// <summary>
        /// Получить список классификаторов оборудования
        /// </summary>
        /// <returns>
        /// Список классификаторов оборудования
        /// </returns>
        public List<KEquipment> GetAllkEquipment()
        {
            return DalContainer.DataManager.kEquipmentRepository.GetAll();
        }

        #endregion

        #region GeoPoints

        /// <summary>
        /// Добавить гео метку
        /// </summary>
        /// <param name="item">
        /// Элемент гео метка
        /// </param>
        public void AddGeoPoint(GeoPoints item)
        {
            var lastTrip = DalContainer.DataManager.TripRepository.GetLastTrip(item.UnitId);

            // Добавляем GeoPoint, если расстояние от центра базы больше её радиуса
            var store = DalContainer.DataManager.StoreRepository.GetStore();

            var isGeoPointInStore = true;
            var cordinatesForStorePolygon = StoreBoundaryCoordinateParser.ParseStringCoordinates(store.StoreBoundaries);

            foreach (var cordinatesForStore in cordinatesForStorePolygon)
            {
                if (Localisation.IsPointLoc(cordinatesForStore.ToArray(), new Point(item.Longitude, item.Latitude)))
                    continue;
                isGeoPointInStore = false;
                break;
            }

            if (lastTrip != null)
            {

                // Стоим на базе
                if (isGeoPointInStore && lastTrip.EndTime > (DateTime)SqlDateTime.MinValue)
                    return;

                item.TripId = lastTrip.Id;
                // Завершение поездки
                if (isGeoPointInStore && lastTrip.EndTime <= (DateTime)SqlDateTime.MinValue)
                {
                    DalContainer.DataManager.GeoPointsRepository.Add(item);
                    lastTrip.EndTime = DateTime.Now;
                    DalContainer.DataManager.TripRepository.Edit(lastTrip);

                    //Добавления комплектации
                    AddComplectation(item, lastTrip, false);
                    return;
                }
            }

            // Добавление новой поездки, если поездка завершена
            if (!isGeoPointInStore && (lastTrip == null || lastTrip.EndTime > (DateTime)SqlDateTime.MinValue))
            {
                var newTrip = new Trip();
                newTrip.UnitId = item.UnitId;
                newTrip.StartTime = DateTime.Now;
                newTrip.EndTime = (DateTime)SqlDateTime.MinValue;
                DalContainer.DataManager.TripRepository.Add(newTrip);
                item.TripId = newTrip.Id;

                //Добавления комплектации
                AddComplectation(item, newTrip, true);
            }

            DalContainer.DataManager.GeoPointsRepository.Add(item);
        }



        /// <summary>
        /// Получить список гео меток
        /// </summary>
        /// <returns>
        /// Список гео меток
        /// </returns>
        public List<GeoPoints> GetAllGeoPoints()
        {
            return DalContainer.DataManager.GeoPointsRepository.GetAll();
        }

        /// <summary>
        /// Получить список координат по промежутку времени для определенного объекта
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="minTime"></param>
        /// <param name="maxTime"></param>
        /// <returns></returns>
        public List<GeoPoints> GetGeoPointListByTime(int itemId, DateTime minTime, DateTime maxTime)
        {
            return DalContainer.DataManager.GeoPointsRepository.GetGeoPointListByTime(itemId, minTime, maxTime);
        }

        /// <summary>
        /// Получить список координат по поездке
        /// </summary>
        /// <param name="tripId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<GeoPoints> GetGeoPointListByTripId(int tripId, int idFrom, int count)
        {
            return DalContainer.DataManager.GeoPointsRepository.GetGeoPointsByTripId(tripId, idFrom, count);
        }

        /// <summary>
        /// Добавлениe комплектаций
        /// </summary>
        /// <param name="item"></param>
        /// <param name="newTrip"></param>
        /// <param name="isStart"></param>
        private static void AddComplectation(GeoPoints item, Trip newTrip, bool isStart)
        {
            // добавление комплектации
            var listEquipment = DalContainer.DataManager.EquipmentRepository.GetСomplectationListByUnitId(item.UnitId);
            foreach (var equipment in listEquipment)
            {
                var tc = new TripComplectation();
                tc.IsStart = isStart;
                tc.EquipmentId = equipment.Id;
                tc.TripId = newTrip.Id;
                DalContainer.DataManager.TripComplectationRepository.Add(tc);
            }
        }

        #endregion

        #region EquipmentGroup

        /// <summary>
        /// Добавить группу оборудования
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        public void AddGroup(EquipmentGroup item)
        {
            DalContainer.DataManager.EquipmentGroupRepository.Add(item);
        }

        /// <summary>
        /// Редактировать группу оборудование
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        public void EditGroup(EquipmentGroup item)
        {
            DalContainer.DataManager.EquipmentGroupRepository.Edit(item);
        }

        /// <summary>
        /// Удалить группу оборудование
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        public void DeleteGroup(EquipmentGroup item)
        {
            DalContainer.DataManager.EquipmentGroupRepository.Delete(item);
        }

        /// <summary>
        /// Получить весь список групп оборудования
        /// </summary>
        /// <returns>
        /// Группа оборудования
        /// </returns>
        public List<EquipmentGroup> GetAllGroup()
        {
            return DalContainer.DataManager.EquipmentGroupRepository.GetAll();
        }

        #endregion

        #region Store

        /// <summary>
        /// Добавить базу
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        public void AddStore(Store item)
        {
            DalContainer.DataManager.StoreRepository.Add(item);
        }

        /// <summary>
        /// Редактировать базу
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        public void EditStore(Store item)
        {
            DalContainer.DataManager.StoreRepository.Edit(item);
        }

        /// <summary>
        /// Удалить базу
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        public void DeleteStore(Store item)
        {
            DalContainer.DataManager.StoreRepository.Delete(item);
        }

        /// <summary>
        /// Получить весь список баз
        /// </summary>
        /// <returns>
        /// Группа оборудования
        /// </returns>
        public List<Store> GetAllStore()
        {
            return DalContainer.DataManager.StoreRepository.GetAll();
        }

        #endregion

        #region Trip

        /// <summary>
        /// Получить весь список порездок
        /// </summary>
        /// <returns>
        /// Группа оборудования
        /// </returns>
        public List<Trip> GetAllTrips()
        {
            return DalContainer.DataManager.TripRepository.GetAll();
        }

        /// <summary>
        /// Получение последней поездки
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public Trip GetLastTrip(int unitId)
        {
            return DalContainer.DataManager.TripRepository.GetLastTrip(unitId);
        }

        #endregion

        #region TripComplectation

        /// <summary>
        /// Получить весь список коплектаций
        /// </summary>
        /// <returns>
        /// Группа оборудования
        /// </returns>
        public List<TripComplectation> GetAllTripComplectations()
        {
            return DalContainer.DataManager.TripComplectationRepository.GetAll();
        }

        #endregion

        #region Equipment

        /// <summary>
        /// Добавить оборудование
        /// </summary>
        /// <param name="item">
        /// Оборудование
        /// </param>
        public void AddEquipment(Equipment item)
        {
            DalContainer.DataManager.EquipmentRepository.Add(item);
        }

        /// <summary>
        /// Редактировать оборудование
        /// </summary>
        /// <param name="item">
        /// Оборудование
        /// </param>
        public void EditEquipment(Equipment item)
        {
            DalContainer.DataManager.EquipmentRepository.Edit(item);
        }

        /// <summary>
        /// Удалить оборудование
        /// </summary>
        /// <param name="item">
        /// Оборудование
        /// </param>
        public void DeleteEquipment(Equipment item)
        {
            DalContainer.DataManager.EquipmentRepository.Delete(item);
        }

        /// <summary>
        /// Получить список оборудования
        /// </summary>
        /// <returns>
        /// Список оборудования
        /// </returns>
        public List<Equipment> GetAllEquipment()
        {
            return DalContainer.DataManager.EquipmentRepository.GetAll();
        }

        /// <summary>
        /// Получить комплектацию машины во время конкретной поездки
        /// </summary>
        /// <param name="tripId">
        /// Ид поездки
        /// </param>
        /// <param name="isStart">
        /// Флаг, указывающий на начало или конец поездки (за какой из моментов берётся комплектация)
        /// </param>
        /// <returns>
        /// Список оборудования
        /// </returns>
        public List<Equipment> GetComplectationByTripId(int tripId, bool isStart)
        {
            return DalContainer.DataManager.EquipmentRepository.GetComplectationByTripId(tripId, isStart);
        }

        /// <summary>
        /// Получение списка оборудования в объекте
        /// </summary>
        /// <returns>
        /// Список оборудования 
        /// </returns>
        public List<Equipment> GetСomplectationListByUnitId(int unitId)
        {
            return DalContainer.DataManager.EquipmentRepository.GetСomplectationListByUnitId(unitId);
        }

        #endregion

        #region Movement

        /// <summary>
        /// Добавить движение оборудования
        /// </summary>
        /// <param name="item">
        /// Движение оборудования
        /// </param>
        public void AddMovement(Movement item)
        {
            DalContainer.DataManager.MovementRepository.Add(item);
        }

        /// <summary>
        /// Редактировать движение оборудования
        /// </summary>
        /// <param name="item">
        /// Движение оборудования
        /// </param>
        public void EditMovement(Movement item)
        {
            DalContainer.DataManager.MovementRepository.Edit(item);
        }

        /// <summary>
        /// Удалить движение оборудования
        /// </summary>
        /// <param name="item">
        /// Движение оборудования
        /// </param>
        public void DeleteMovement(Movement item)
        {
            DalContainer.DataManager.MovementRepository.Delete(item);
        }

        /// <summary>
        /// Получить список перемещений оборудования
        /// </summary>
        /// <returns>
        /// Список движений
        /// </returns>
        public List<Movement> GetAllMovement()
        {
            return DalContainer.DataManager.MovementRepository.GetAll();
        }

        /// <summary>
        /// Получить список последних передвижений
        /// </summary>
        /// <returns>
        /// Список движений
        /// </returns>
        public List<Movement> GetLastMovements()
        {
            return DalContainer.DataManager.MovementRepository.GetLastMovements();
        }

        /// <summary>
        /// Метод получения списка передвижений по ид поездки
        /// </summary>
        /// <param name="tripId"></param>
        /// <returns></returns>
        public List<Movement> GetMovementListByTripId(int tripId)
        {
            var trip = DalContainer.DataManager.TripRepository.Get(tripId);
            return DalContainer.DataManager.MovementRepository.GetByTimeAndUnitId(trip.UnitId, trip.StartTime, trip.EndTime); ;
        }

        /// <summary>
        /// Получение передвижения по ид
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Movement GetMovement(int? id)
        {
            return id != null ? DalContainer.DataManager.MovementRepository.Get(id) : null;
        }

        /// <summary>
        /// Получение списка передвижений оборудования по объекту и времени
        /// </summary>
        /// <param name="minTime"></param>
        /// <param name="maxTime"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<Movement> GetMovementListByTimeAndUnitId(DateTime minTime, DateTime maxTime, int unitId)
        {
            return DalContainer.DataManager.MovementRepository.GetByTimeAndUnitId(unitId, minTime, maxTime);
        }

        /// <summary>
        /// Полчения списка передвижений оборудования
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public List<Movement> GetMovementListByEquipmentId(int? equipmentId)
        {
            return DalContainer.DataManager.MovementRepository.GetByEquipmentId(equipmentId);
        }

        #endregion

        #region Tag

        /// <summary>
        /// Добавить тег
        /// </summary>
        /// <param name="item">
        /// тег
        /// </param>
        public void AddTag(Tag item)
        {
            DalContainer.DataManager.TagRepository.Add(item);
        }

        /// <summary>
        /// Редактировать тег
        /// </summary>
        /// <param name="item">
        /// тег
        /// </param>
        public void EditTag(Tag item)
        {
            DalContainer.DataManager.TagRepository.Edit(item);
        }

        /// <summary>
        /// Удалить тег
        /// </summary>
        /// <param name="item">
        /// тег
        /// </param>
        public void DeleteTag(Tag item)
        {
            DalContainer.DataManager.TagRepository.Delete(item);
        }

        /// <summary>
        /// Получить список тегов
        /// </summary>
        /// <returns>
        /// Список тегов
        /// </returns>
        public List<Tag> GetAllTags()
        {
            return DalContainer.DataManager.TagRepository.GetAll();
        }

        #endregion

        #region kObject

        /// <summary>
        /// Добавление классификатора типа объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора типа объекта
        /// </param>
        public void AddkObject(kObject item)
        {
            DalContainer.DataManager.kObjectRepository.Add(item);
        }

        /// <summary>
        /// Редактирование классификатора типа объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора типа объекта
        /// </param>
        public void EditkObject(kObject item)
        {
            DalContainer.DataManager.kObjectRepository.Edit(item);
        }

        /// <summary>
        /// Удаление классификатора типа объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора типа объекта
        /// </param>
        public void DeletekObject(kObject item)
        {
            DalContainer.DataManager.kObjectRepository.Delete(item);
        }

        /// <summary>
        /// Получение списка классификаторов типов объектов
        /// </summary>
        /// <returns>
        /// Список классификаторов типов объектов
        /// </returns>
        public List<kObject> GetAllkObject()
        {
            return DalContainer.DataManager.kObjectRepository.GetAll();
        }

        #endregion

        #region Unit

        /// <summary>
        /// Добавление классификатора объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора объекта
        /// </param>
        public void AddUnit(Unit item)
        {
            DalContainer.DataManager.UnitRepository.Add(item);
        }

        /// <summary>
        /// Редактирование классификатора объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора объекта
        /// </param>
        public void EditUnit(Unit item)
        {
            DalContainer.DataManager.UnitRepository.Edit(item);
        }

        /// <summary>
        /// Удаление классификатора объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора объекта
        /// </param>
        public void DeleteUnit(Unit item)
        {
            DalContainer.DataManager.UnitRepository.Delete(item);
        }

        /// <summary>
        /// Получение списка классификаторов объектов
        /// </summary>
        /// <returns>
        /// Список классификаторов объектов
        /// </returns>
        public List<Unit> GetAllUnit()
        {
            return DalContainer.DataManager.UnitRepository.GetAll();
        }

        #endregion

        #region UniqEquipmentObject

        /// <summary>
        /// Добавление классификатора уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора уникального оборудования для машины
        /// </param>
        public void AddUniqEquipmentObject(UniqEquipmentObject item)
        {
            DalContainer.DataManager.UniqEquipmentObjectRepository.Add(item);
        }

        /// <summary>
        /// Редактирование классификатора уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора уникального оборудования для машины
        /// </param>
        public void EditUniqEquipmentObject(UniqEquipmentObject item)
        {
            DalContainer.DataManager.UniqEquipmentObjectRepository.Edit(item);
        }

        /// <summary>
        /// Удаление классификатора уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора уникального оборудования для машины
        /// </param>
        public void DeleteUniqEquipmentObject(UniqEquipmentObject item)
        {
            DalContainer.DataManager.UniqEquipmentObjectRepository.Delete(item);
        }

        /// <summary>
        /// Получение списка классификаторов уникального оборудования для машины
        /// </summary>
        /// <returns>
        /// Список классификаторов уникального оборудования для машины
        /// </returns>
        public List<UniqEquipmentObject> GetAllUniqEquipmentObject()
        {
            return DalContainer.DataManager.UniqEquipmentObjectRepository.GetAll();
        }

        /// <summary>
        /// Получение списка классификаторов уникального оборудования для машины 
        /// </summary>
        /// <returns>
        /// Список классификаторов уникального оборудования для машины
        /// </returns>
        public List<UniqEquipmentObject> GetUniqEquipmentObjectListByUnitId(int unitId)
        {
            return DalContainer.DataManager.UniqEquipmentObjectRepository.GetUniqEquipmentObjectListByUnitId(unitId);
        }

        #endregion

        #region NonUniqEquipmentObject

        /// <summary>
        /// Добавление классификатора не уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора не уникального оборудования для машины
        /// </param>
        public void AddNonUniqEquipmentObject(NonUniqEquipmentObject item)
        {
            DalContainer.DataManager.NonUniqEquipmentObjectRepository.Add(item);
        }

        /// <summary>
        /// Редактирование классификатора не уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора не уникального оборудования для машины
        /// </param>
        public void EditNonUniqEquipmentObject(NonUniqEquipmentObject item)
        {
            DalContainer.DataManager.NonUniqEquipmentObjectRepository.Edit(item);
        }

        /// <summary>
        /// Удаление классификатора не уникального оборудования для машины
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора не уникального оборудования для машины
        /// </param>
        public void DeleteNonUniqEquipmentObject(NonUniqEquipmentObject item)
        {
            DalContainer.DataManager.NonUniqEquipmentObjectRepository.Delete(item);
        }

        /// <summary>
        /// Получение списка классификаторов не уникального оборудования для машины
        /// </summary>
        /// <returns>
        /// Список классификаторов не уникального оборудования для машины
        /// </returns>
        public List<NonUniqEquipmentObject> GetAllNonUniqEquipmentObject()
        {
            return DalContainer.DataManager.NonUniqEquipmentObjectRepository.GetAll();
        }

        /// <summary>
        /// Получение списка классификаторов неуникального оборудования для машины 
        /// </summary>
        /// <returns>
        /// Список классификаторов уникального оборудования для машины
        /// </returns>
        public List<NonUniqEquipmentObject> GetNonUniqEquipmentListObjectByUnitId(int unitId)
        {
            return DalContainer.DataManager.NonUniqEquipmentObjectRepository.GetAll().
                Where(e => e.UnitId == unitId).ToList();
        }

        #endregion

        #region Implementation of IGeneralDataPoint

        /// <summary>
        /// Получить количество элементов
        /// </summary>
        /// <param name="typeName">
        /// Имя шлюза записи
        /// </param>
        /// <returns>
        /// Количество элементов, лоступное через шлюз записи
        /// </returns>
        public long Count(string typeName)
        {
            return _standartDataPointStrategy.Count(typeName);
        }

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="item">
        /// Информация о добавляемой записи
        /// </param>
        /// <returns>
        /// Измененный объект, если есть автогенерируемые поля
        /// </returns>
        public Change Add(Change item)
        {
            return _standartDataPointStrategy.Add(item);
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="item">
        /// Новое состояние записи
        /// </param>
        /// <returns>
        ///  Измененный объект (Возврат изменений сервера)
        /// </returns>
        public Change Edit(Change item)
        {
            return _standartDataPointStrategy.Edit(item);
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="item">
        /// Новое состояние записи
        /// </param>
        public void DeleteWhere(Change item)
        {
            _standartDataPointStrategy.DeleteWhere(item);
        }

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
        public List<Change> GetItemsWhere(string typeName, Dictionary<string, object> keys)
        {
            return _standartDataPointStrategy.GetItemsWhere(typeName, keys);
        }

        /// <summary>
        /// Получение данных посредством TransferManager
        /// </summary>
        /// <param name="typeName">Имя шлюза записи</param>
        /// <param name="ident">Идентификатор части</param>
        /// <returns>Часть данных со списка</returns>
        public TransferPart TransferItems(string typeName, TransferPartIdent ident)
        {
            return _standartDataPointStrategy.TransferItems(typeName, ident);
        }

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
        public List<Change> GetPage(string typeName, int pageIndex, int count)
        {
            return _standartDataPointStrategy.GetPage(typeName, pageIndex, count);
        }

        #endregion
    }
}