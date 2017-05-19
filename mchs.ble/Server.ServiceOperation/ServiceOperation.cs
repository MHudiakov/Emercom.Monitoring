using System;
using System.Collections.Generic;
using Init.Web;
using Server.Dal;
using Server.Dal.Sql.DataObjects;

namespace Server.WCF
{
    /// <summary>
    /// WCF сервис
    /// </summary>
    public class ServiceOperation : BaseWebService, IServiceOperation
    {
        #region Конструктор и инициализация

        /// <summary>
        /// Сервис управления сервером 
        /// </summary>
        public ServiceOperation()
           : base(typeof(ServiceOperation))
        {}

        #endregion
        
        public void TestConnection()
        {}

        #region EquipmentGroup

        /// <summary>
        /// Добавить группу оборудования
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        public void AddGroup(EquipmentGroup item)
        {
            DalContainer.GetDataManager.EquipmentGroupRepository.Add(item);
        }

        /// <summary>
        /// Редактировать группу оборудование
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        public void EditGroup(EquipmentGroup item)
        {
            DalContainer.GetDataManager.EquipmentGroupRepository.Edit(item);
        }

        /// <summary>
        /// Удалить группу оборудование
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        public void DeleteGroup(EquipmentGroup item)
        {
            DalContainer.GetDataManager.EquipmentGroupRepository.Delete(item);
        }

        /// <summary>
        /// Получить список групп оборудования
        /// </summary>
        /// <returns>
        /// Список групп оборудования
        /// </returns>
        public List<EquipmentGroup> GetGroupList()
        {
            return DalContainer.GetDataManager.EquipmentGroupRepository.GetAll();
        }

        #endregion



























        #region kEquipment

        /// <summary>
        /// Добавить классификатора оборудования
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора оборудования
        /// </param>
        public void AddKEquipment(KEquipment item)
        {
            DalContainer.GetDataManager.KEquipmentRepository.Add(item);
        }

        /// <summary>
        /// Редактировать классификатора оборудования
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора оборудования
        /// </param>
        public void EditKEquipment(KEquipment item)
        {
            DalContainer.GetDataManager.KEquipmentRepository.Edit(item);
        }

        /// <summary>
        /// Удалить классификатора оборудования
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора оборудования
        /// </param>
        public void DeleteKEquipment(KEquipment item)
        {
            DalContainer.GetDataManager.KEquipmentRepository.Delete(item);
        }

        /// <summary>
        /// Получить список классификаторов оборудования
        /// </summary>
        /// <returns>
        /// Список классификаторов оборудования
        /// </returns>
        public List<KEquipment> GetKEquipmentList()
        {
            return DalContainer.GetDataManager.KEquipmentRepository.GetAll();
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
            DalContainer.GetDataManager.EquipmentRepository.Add(item);
        }

        /// <summary>
        /// Редактировать оборудование
        /// </summary>
        /// <param name="item">
        /// Оборудование
        /// </param>
        public void EditEquipment(Equipment item)
        {
            DalContainer.GetDataManager.EquipmentRepository.Edit(item);
        }

        /// <summary>
        /// Удалить оборудование
        /// </summary>
        /// <param name="item">
        /// Оборудование
        /// </param>
        public void DeleteEquipment(Equipment item)
        {
            DalContainer.GetDataManager.EquipmentRepository.Delete(item);
        }

        /// <summary>
        /// Получить список оборудования
        /// </summary>
        /// <returns>
        /// Список оборудования
        /// </returns>
        public List<Equipment> GetAllEquipment()
        {
            return DalContainer.GetDataManager.EquipmentRepository.GetAll();
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
            DalContainer.GetDataManager.MovementRepository.Add(item);
        }

        /// <summary>
        /// Редактировать движение оборудования
        /// </summary>
        /// <param name="item">
        /// Движение оборудования
        /// </param>
        public void EditMovement(Movement item)
        {
            DalContainer.GetDataManager.MovementRepository.Edit(item);
        }

        /// <summary>
        /// Удалить движение оборудования
        /// </summary>
        /// <param name="item">
        /// Движение оборудования
        /// </param>
        public void DeleteMovement(Movement item)
        {
            DalContainer.GetDataManager.MovementRepository.Delete(item);
        }

        /// <summary>
        /// Получить список перемещений оборудования
        /// </summary>
        /// <returns>
        /// Список движений
        /// </returns>
        public List<Movement> GetAllMovement()
        {
            return DalContainer.GetDataManager.MovementRepository.GetAll();
        }

        /// <summary>
        /// Получение передвижения по ид
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Movement GetMovement(int? id)
        {
            return id != null ? DalContainer.GetDataManager.MovementRepository.Get(id) : null;
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
            throw new NotImplementedException();
            // return DalContainer.GetDataManager.MovementRepository.GetByTimeAndUnitId(unitId, minTime, maxTime);
        }

        /// <summary>
        /// Полчения списка передвижений оборудования
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public List<Movement> GetMovementListByEquipmentId(int? equipmentId)
        {
            throw new NotImplementedException();
            //  return DalContainer.GetDataManager.MovementRepository.GetByEquipmentId(equipmentId);
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
            DalContainer.GetDataManager.UnitRepository.Add(item);
        }

        /// <summary>
        /// Редактирование классификатора объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора объекта
        /// </param>
        public void EditUnit(Unit item)
        {
            DalContainer.GetDataManager.UnitRepository.Edit(item);
        }

        /// <summary>
        /// Удаление классификатора объекта
        /// </summary>
        /// <param name="item">
        /// Элемент классификатора объекта
        /// </param>
        public void DeleteUnit(Unit item)
        {
            DalContainer.GetDataManager.UnitRepository.Delete(item);
        }

        /// <summary>
        /// Получение списка классификаторов объектов
        /// </summary>
        /// <returns>
        /// Список классификаторов объектов
        /// </returns>
        public List<Unit> GetAllUnit()
        {
            return DalContainer.GetDataManager.UnitRepository.GetAll();
        }

        #endregion
    }
}