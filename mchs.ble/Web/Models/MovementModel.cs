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
                
                IsArrived = movement.IsArrived;
                Date = movement.Date;
                DateOfMovementStr = movement.Date.ToString("G");
            }
        }

        #region DataFields

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
        public DateTime Date { get; set; }

        public string DateOfMovementStr { get; set; }

        #endregion
    }
}