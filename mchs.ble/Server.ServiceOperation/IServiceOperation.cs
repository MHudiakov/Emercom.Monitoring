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










        #region kEquipment

        /// <summary>
        /// Добавить классификатора оборудования
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
        List<Equipment> GetAllEquipment();

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
        /// Получить список перемещений оборудования
        /// </summary>
        /// <returns>
        /// Список движений
        /// </returns>
        [OperationContract]
        List<Movement> GetAllMovement();

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

        #endregion
    }
}