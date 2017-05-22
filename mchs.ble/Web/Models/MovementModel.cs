// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovementModel.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Модель списка движений по складу
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using DAL.WCF;
    using DAL.WCF.ServiceReference;

    /// <summary>
    /// Модель списка движений по складу
    /// </summary>
    public class MovementModel
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MovementModel"/>.
        /// </summary>
        /// <param name="movement">
        /// Движение по складу
        /// </param>
        public MovementModel(Movement movement)
        {
            if (movement != null)
            {
                Id = movement.Id;
                UnitId = movement.UnitId;
                EquipmentId = movement.EquipmentId;
                kEquipmentId = movement.kEquipmentId;
                IsArrived = movement.IsArrived;
                DateOfMovement = movement.DateOfMovement;
                DateOfMovementStr = movement.DateOfMovement.ToString("G");
                EquipmentRFId = movement.Equipment != null ? movement.Equipment.RFId : "";
                kEquipmentName = movement.KEquipment != null ? movement.KEquipment.Name : "";
            }
        }
        #region DataFields

        /// <summary>
        /// Оборудование
        /// </summary>
        public int kEquipmentId { get; set; }

        public string kEquipmentName { get; set; }

        public string EquipmentRFId { get; set; }

        public int UnitId { get; set; }

        /// <summary>
        /// Id перемещения
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id наличия на складе
        /// </summary>
        public int? EquipmentId { get; set; }

        /// <summary>
        /// Прибыло/Убыло
        /// </summary>
        public bool IsArrived { get; set; }

        /// <summary>
        /// Дата перемещения
        /// </summary>
        public DateTime DateOfMovement { get; set; }

        public String DateOfMovementStr { get; set; }
        #endregion
    }
}