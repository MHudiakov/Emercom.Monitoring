using System;
using System.Collections.Generic;
using System.Linq;
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

        #region KEquipment

        /// <summary>
        /// Добавить классификатор оборудования
        /// </summary>
        /// <param name="item">
        /// Классификатор оборудования
        /// </param>
        public void AddKEquipment(KEquipment item)
        {
            DalContainer.GetDataManager.KEquipmentRepository.Add(item);
        }

        /// <summary>
        /// Редактировать классификатор оборудования
        /// </summary>
        /// <param name="item">
        /// Классификатор оборудования
        /// </param>
        public void EditKEquipment(KEquipment item)
        {
            DalContainer.GetDataManager.KEquipmentRepository.Edit(item);
        }

        /// <summary>
        /// Удалить классификатор оборудования
        /// </summary>
        /// <param name="item">
        /// Классификатор оборудования
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
        public List<Equipment> GetEquipmentList()
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
        public List<Movement> GetMovementList()
        {
            return DalContainer.GetDataManager.MovementRepository.GetAll();
        }

        /// <summary>
        /// Получение передвижения по ид
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Movement GetMovement(int id)
        {
            return DalContainer.GetDataManager.MovementRepository.Get(id);
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
            return DalContainer.GetDataManager.MovementRepository.GetByTimeAndUnitId(unitId, minTime, maxTime);
        }

        /// <summary>
        /// Полчения списка передвижений конкретного оборудования 
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public List<Movement> GetMovementListByEquipmentId(int equipmentId)
        {
            return DalContainer.GetDataManager.MovementRepository.GetByEquipmentId(equipmentId);
        }

        #endregion

        #region Unit

        /// <summary>
        /// Добавление юнита
        /// </summary>
        /// <param name="item">
        /// Юнит
        /// </param>
        public void AddUnit(Unit item)
        {
            DalContainer.GetDataManager.UnitRepository.Add(item);
        }

        /// <summary>
        /// Редактирование юнита
        /// </summary>
        /// <param name="item">
        /// Юнит
        /// </param>
        public void EditUnit(Unit item)
        {
            DalContainer.GetDataManager.UnitRepository.Edit(item);
        }

        /// <summary>
        /// Удаление юнита
        /// </summary>
        /// <param name="item">
        /// Юнит
        /// </param>
        public void DeleteUnit(Unit item)
        {
            DalContainer.GetDataManager.UnitRepository.Delete(item);
        }

        /// <summary>
        /// Получение списка юнитов
        /// </summary>
        /// <returns>
        /// Список юнитов
        /// </returns>
        public List<Unit> GetUnitList()
        {
            return DalContainer.GetDataManager.UnitRepository.GetAll();
        }

        /// <summary>
        /// Получить список юнитов, которые может просматривать данный пользователь
        /// </summary>
        /// <param name="userId">
        /// Id пользователя
        /// </param>
        /// <returns>
        /// Список юнитов
        /// </returns>
        public List<Unit> GetUnitListForUser(int userId)
        {
            return DalContainer.GetDataManager.UnitRepository.GetUnitListForUser(userId);
        }

        #endregion

        #region Settings

        /// <summary>
        /// Добавление настроек сервера
        /// </summary>
        /// <param name="item">
        /// Настрйки сервера
        /// </param>
        public void AddSettings(Settings item)
        {
            DalContainer.GetDataManager.SettingsRepository.Add(item);
        }

        /// <summary>
        /// Редактирование настроек сервера
        /// </summary>
        /// <param name="item">
        /// Настрйки сервера
        /// </param>
        public void EditSettings(Settings item)
        {
            DalContainer.GetDataManager.SettingsRepository.Edit(item);
        }

        /// <summary>
        /// Удаление настроек сервера
        /// </summary>
        /// <param name="item">
        /// Настрйки сервера
        /// </param>
        public void DeleteSettings(Settings item)
        {
            DalContainer.GetDataManager.SettingsRepository.Delete(item);
        }

        /// <summary>
        /// Получение настроек сервера
        /// </summary>
        /// <returns>
        /// Настрйки сервера
        /// </returns>
        public Settings GetSettings()
        {
            return DalContainer.GetDataManager.SettingsRepository.GetAll().FirstOrDefault();
        }

        #endregion

        #region Division

        /// <summary>
        /// Добавление подразделения
        /// </summary>
        /// <param name="item">
        /// Подразделение
        /// </param>
        public void AddDivision(Division item)
        {
            DalContainer.GetDataManager.DivisionRepository.Add(item);
        }

        /// <summary>
        /// Редактирование подразделения
        /// </summary>
        /// <param name="item">
        /// Подразделение
        /// </param>
        public void EditDivision(Division item)
        {
            DalContainer.GetDataManager.DivisionRepository.Edit(item);
        }

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="item">
        /// Подразделение
        /// </param>
        public void DeleteDivision(Division item)
        {
            DalContainer.GetDataManager.DivisionRepository.Delete(item);
        }

        /// <summary>
        /// Получение списка подразделений
        /// </summary>
        /// <returns>
        /// Список подразделений
        /// </returns>
        public List<Division> GetDivisionList()
        {
            return DalContainer.GetDataManager.DivisionRepository.GetAll();
        }

        /// <summary>
        /// Получить список подразделений в древовидной структуре
        /// </summary>
        /// <returns>
        /// Список подразделений
        /// </returns>
        public List<Division> GetTreeSortedDivisionList()
        {
            return DalContainer.GetDataManager.DivisionRepository.GetTreeSortedList();
        }

        #endregion

        #region User

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="item">
        /// Пользователь
        /// </param>
        public void AddUser(User item)
        {
            DalContainer.GetDataManager.UserRepository.Add(item);
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="item">
        /// Пользователь
        /// </param>
        public void EditUser(User item)
        {
            DalContainer.GetDataManager.UserRepository.Edit(item);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="item">
        /// Пользователь
        /// </param>
        public void DeleteUser(User item)
        {
            DalContainer.GetDataManager.UserRepository.Delete(item);
        }

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns>
        /// Список пользователей
        /// </returns>
        public List<User> GetUserList()
        {
           return DalContainer.GetDataManager.UserRepository.GetAll();
        }

        #endregion
    }
}