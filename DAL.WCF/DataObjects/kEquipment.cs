using System.Collections.Generic;

namespace DAL.WCF.ServiceReference
{
    using System.Linq;
    using WCF;

    public partial class KEquipment
    {
        public EquipmentGroup EquipmentGroup => 
            DalContainer.WcfDataManager.EquipmentGroupList.Single(e => e.Id == this.EquipmentGroupId);

        public List<Equipment> EquipmentList => DalContainer.WcfDataManager.EquipmentList
            .Where(e => e.KEquipmentId == this.Id).ToList();

        // todo: for test only
        public int ParentId => 0;
    }
}
