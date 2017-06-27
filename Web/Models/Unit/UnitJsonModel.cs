using System.Collections.Generic;
using System.Linq;

namespace Web.Models.Unit
{
    using System;

    using DAL.WCF;
    using DAL.WCF.ServiceReference;

    public class UnitJsonModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public List<MovementModel> MovmentList { get; }

        public UnitJsonModel(Unit unit)
        {
            this.Id = unit.Id;
            this.Name = unit.Name;
            this.Description = unit.Description;
            
            // movements for last 5 days
            this.MovmentList = DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByTimeAndUnitId(
                    DateTime.Now.AddDays(-5), DateTime.Now, unit.Id).
                OrderByDescending(m => m.Date).Select(e =>
                    new MovementModel(e)).ToList();
        }
    }
}