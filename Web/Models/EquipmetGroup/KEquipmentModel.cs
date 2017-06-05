using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.EquipmetGroup
{
    using DAL.WCF.ServiceReference;

    public class KEquipmentModel
    {
        public KEquipmentModel(KEquipment kEquipment)
        {
            if (kEquipment == null)
                throw new ArgumentNullException(nameof(kEquipment));

            this.Id = kEquipment.Id;
            this.Name = kEquipment.Name;
            this.Description = kEquipment.Description;
            this.EquipmentGroupId = kEquipment.EquipmentGroupId;
            this.EquipmentList = kEquipment.EquipmentList;
        }

        public int Id { get; set; }

        /// <summary>
        /// Название объекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор родительского подразделения
        /// </summary>
        public int? EquipmentGroupId { get; set; }

        /// <summary>
        /// Описание/примечание объекта
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Список оборудования
        /// </summary>
        public List<Equipment> EquipmentList { get; set; }

    }
}