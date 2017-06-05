using System;
using System.Collections.Generic;

namespace Web.Models.EquipmetGroup
{
    using DAL.WCF.ServiceReference;

    public class EquipmentGroupModel
    {
        public EquipmentGroupModel(EquipmentGroup equipmentGroup)
        {
            if (equipmentGroup == null)
                throw new ArgumentNullException(nameof(equipmentGroup));

            this.Id = equipmentGroup.Id;
            this.Name = equipmentGroup.Name;
            this.Description = equipmentGroup.Description;
            this.ParentId = equipmentGroup.ParentId;
            this.KEquipmentList = equipmentGroup.KEquipmentList;
        }

        public int Id { get; set; }

        /// <summary>
        /// Название объекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор родительского подразделения
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Описание/примечание объекта
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Список классификаторов
        /// </summary>
        public List<KEquipment> KEquipmentList { get; set; }
    }
}