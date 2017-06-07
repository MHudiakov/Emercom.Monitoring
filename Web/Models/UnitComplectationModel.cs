using System;
using System.Collections.Generic;

namespace Web.Models
{
    using DAL.WCF.ServiceReference;

    public class UnitComplectationModel
    {
        public UnitComplectationModel(EquipmentGroup equipmentGroup, List<EquipmentModel> equipmentModels )
        {
            if (equipmentGroup == null)
                throw new ArgumentNullException(nameof(equipmentGroup));

            this.Id = equipmentGroup.Id;
            this.Name = equipmentGroup.Name;
            this.EquipmentModels = equipmentModels;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<EquipmentModel> EquipmentModels { get; set; }
    }
}