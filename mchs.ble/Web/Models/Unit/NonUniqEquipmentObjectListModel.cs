using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    using DAL.WCF.ServiceReference;

    public class NonUniqEquipmentObjectListModel
    {
        public NonUniqEquipmentObjectListModel(IEnumerable<NonUniqEquipmentObject> list)
        {
            this.List = list.Select(e => new NonUniqEquipmentObjectModel(e)).ToList();
        }

        public List<NonUniqEquipmentObjectModel> List { get; set; }
    }
}