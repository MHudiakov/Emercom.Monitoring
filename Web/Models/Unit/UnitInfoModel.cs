using System;
using System.Linq;

namespace Web.Models.Unit
{
    using System.Collections.Generic;

    using DAL.WCF;
    using DAL.WCF.ServiceReference;

    public class UnitInfoModel
    {
        public UnitModel Unit { get; }

        public List<MovementModel> MovmentList { get; }

        public UnitInfoModel(Unit unit)
        {
            this.Unit = new UnitModel(unit);
            
            this.MovmentList = DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByTimeAndUnitId(
                DateTime.Now.AddDays(-5), DateTime.Now, unit.Id).   // movemetns for last 5 days
                OrderByDescending(m => m.Date).Select(e => 
                new MovementModel(e)).ToList();
        }
    }
}