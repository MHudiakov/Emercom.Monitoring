using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.WCF.ServiceReference
{
    public partial class Unit
    {
        public Division GetDivision => DalContainer.WcfDataManager.DivisionList.Single(d => d.Id == DivisionId);

        public List<Equipment> GetEquipmentList => DalContainer.WcfDataManager.EquipmentList.Where(e => e.UnitId == Id)
            .ToList();
    }
}
