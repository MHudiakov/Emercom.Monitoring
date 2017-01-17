namespace Web.Models.Unit
{
    using System.Collections.Generic;
    using System.Linq;

    using DAL.WCF.ServiceReference;

    public class UniqEquipmentObjectListModel
    {
        public UniqEquipmentObjectListModel(IEnumerable<UniqEquipmentObject> list)
        {
            this.List = list.Select(e => new UniqEquipmentObjectModel(e)).ToList();
        }

        public List<UniqEquipmentObjectModel> List { get; set; }
    }
}