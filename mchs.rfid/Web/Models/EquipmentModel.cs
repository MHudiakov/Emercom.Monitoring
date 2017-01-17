// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StorageModel.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Модель списка оборудования на складе
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
    /// Модель списка оборудования на складе
    /// </summary>
    public class EquipmentModel
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EquipmentModel"/>.
        /// </summary>
        /// <param name="movement">
        /// Оборудование на складе
        /// </param>
        public EquipmentModel(Equipment equipment)
        {
            this.Equipment = equipment;

            if (equipment != null)
            {
                Id = equipment.Id;
                RFId = equipment.RFId;
                kEquipmentId = equipment.kEquipmentId;
                Description = equipment.Description;
                kEquipment = equipment.kEquipment;
            }
        }

        /// <summary>
        /// Оборудование на складе
        /// </summary>
        public Equipment Equipment { get; set; }


        [DisplayName("Оборудование")]
        public kEquipment kEquipment { get; set; }


        #region DataFields
        /// <summary>
        /// Id
        /// </summary>
        [DisplayName("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DisplayName("Название хранения на складе")]
        public string RFId { get; set; }


        /// <summary>
        /// Id оборудования
        /// </summary>
        [DisplayName("Id оборудования")]
        public int kEquipmentId { get; set; }


        /// <summary>
        /// Описание/Примечание
        /// </summary>
        [DisplayName("Описание/Примечание")]
        public string Description { get; set; }

        #endregion
    }
}