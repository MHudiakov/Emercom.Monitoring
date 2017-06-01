// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceOperation.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Интерфейс WCF сервиса
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ServiceModel;
using Server.Dal.Sql.DataObjects;

namespace Server.WCF
{
    /// <summary>
    /// Интерфейс WCF сервиса
    /// </summary>
    [ServiceContract]
    public interface IServiceOperation
    {
        /// <summary>
        /// Метод проверки соединения с сервером
        /// </summary>
        [OperationContract]
        void TestConnection();

        #region EquipmentGroup

        /// <summary>
        /// Добавить группу оборудования
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        [OperationContract]
        void AddGroup(EquipmentGroup item);

        /// <summary>
        /// Редактировать группу оборудование
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        [OperationContract]
        void EditGroup(EquipmentGroup item);

        /// <summary>
        /// Удалить группу оборудование
        /// </summary>
        /// <param name="item">
        /// Группа оборудования
        /// </param>
        [OperationContract]
        void DeleteGroup(EquipmentGroup item);

        /// <summary>
        /// Получить весь список групп оборудования
        /// </summary>
        /// <returns>
        /// Группа оборудования
        /// </returns>
        [OperationContract]
        List<EquipmentGroup> GetGroupList();

        #endregion

        #region KEquipment

        /// <summary>
        /// Добавить классификатор оборудования
        /// </summary>
        /// <param name="item">
        /// Классификатор оборудования
        /// </param>
        [OperationContract]
        void AddKEquipment(KEquipment item);

        /// <summary>
        /// Редактировать классификатор оборудования
        /// </summary>
        /// <param name="item">
        /// Классификатор оборудования
        /// </param>
        [OperationContract]
        void EditKEquipment(KEquipment item);

        /// <summary>
        /// Удалить классификатор оборудования
        /// </summary>
        /// <param name="item">
        /// Классификатор оборудования
        /// </param>
        [OperationContract]
        void DeleteKEquipment(KEquipment item);

        /// <summary>
        /// Получить список классификаторов оборудования
        /// </summary>
        /// <returns>
        /// Список классификаторов оборудования
        /// </returns>
        [OperationContract]
        List<KEquipment> GetKEquipmentList();

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
        List<Equipment> GetEquipmentList();

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
        /// Получить список весь перемещений оборудования
        /// </summary>
        /// <returns>
        /// Список движений
        /// </returns>
        [OperationContract]
        List<Movement> GetMovementList();

        /// <summary>
        /// Полчения списка передвижений конкретного оборудования
        /// </summary>
        /// <param name="equipmentId">
        /// Ид оборудования
        /// </param>
        /// <returns></returns>
        [OperationContract]
        List<Movement> GetMovementListByEquipmentId(int equipmentId);

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
        Movement GetMovement(int id);

        #endregion

        #region Unit

        /// <summary>
        /// Добавление юнита
        /// </summary>
        /// <param name="item">
        /// Юнит
        /// </param>
        [OperationContract]
        void AddUnit(Unit item);

        /// <summary>
        /// Редактирование юнита
        /// </summary>
        /// <param name="item">
        /// Юнит
        /// </param>
        [OperationContract]
        void EditUnit(Unit item);

        /// <summary>
        /// Удаление юнита
        /// </summary>
        /// <param name="item">
        /// Юнит
        /// </param>
        [OperationContract]
        void DeleteUnit(Unit item);

        /// <summary>
        /// Получение списка юнитов
        /// </summary>
        /// <returns>
        /// Список юнитов
        /// </returns>
        [OperationContract]
        List<Unit> GetUnitList();

        /// <summary>
        /// Получить список юнитов, которые может просматривать данный пользователь
        /// </summary>
        /// <param name="userId">
        /// Id пользователя
        /// </param>
        /// <returns>
        /// Список юнитов
        /// </returns>
        [OperationContract]
        List<Unit> GetUnitListForUser(int userId);

        #endregion

        #region Settings

        /// <summary>
        /// Добавление настроек сервера
        /// </summary>
        /// <param name="item">
        /// Настрйки сервера
        /// </param>
        [OperationContract]
        void AddSettings(Settings item);

        /// <summary>
        /// Редактирование настроек сервера
        /// </summary>
        /// <param name="item">
        /// Настрйки сервера
        /// </param>
        [OperationContract]
        void EditSettings(Settings item);

        /// <summary>
        /// Удаление настроек сервера
        /// </summary>
        /// <param name="item">
        /// Настрйки сервера
        /// </param>
        [OperationContract]
        void DeleteSettings(Settings item);

        /// <summary>
        /// Получение настроек сервера
        /// </summary>
        /// <returns>
        /// Настрйки сервера
        /// </returns>
        [OperationContract]
        Settings GetSettings();

        #endregion

        #region Division

        /// <summary>
        /// Добавление подразделения
        /// </summary>
        /// <param name="item">
        /// Подразделение
        /// </param>
        [OperationContract]
        void AddDivision(Division item);

        /// <summary>
        /// Редактирование подразделения
        /// </summary>
        /// <param name="item">
        /// Подразделение
        /// </param>
        [OperationContract]
        void EditDivision(Division item);

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="item">
        /// Подразделение
        /// </param>
        [OperationContract]
        void DeleteDivision(Division item);

        /// <summary>
        /// Получение списка юнитов
        /// </summary>
        /// <returns>
        /// Список подразделений
        /// </returns>
        [OperationContract]
        List<Division> GetDivisionList();

        #endregion

        #region User

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="item">
        /// Пользователь
        /// </param>
        [OperationContract]
        void AddUser(User item);

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="item">
        /// Пользователь
        /// </param>
        [OperationContract]
        void EditUser(User item);

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="item">
        /// Пользователь
        /// </param>
        [OperationContract]
        void DeleteUser(User item);

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns>
        /// Список пользователей
        /// </returns>
        [OperationContract]
        List<User> GetUserList();

        #endregion
    }
}