using System.Collections.Generic;
using System.Linq;
using DAL.WCF.ServiceReference;

namespace Web.Models.UnitComplectation
{
    public class UnitComplectationModel
    {
        public UnitComplectationModel(IGrouping<EquipmentGroup, Equipment> equipmentGroup)
        {
            EquipmentGroup group = equipmentGroup.Key;
            this.Id = group.Id;
            this.Name = group.Name;

            var equipmentList = equipmentGroup.ToList();
            this.IsFullyEquipped = equipmentList.TrueForAll(equipment => equipment.IsInTheUnit);
            this.EquipmentModelList = equipmentList.Select(equipment => 
            new EquipmentModel(equipment)).ToList();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsFullyEquipped { get; set; }

        public List<EquipmentModel> EquipmentModelList { get; set; }
    }
}