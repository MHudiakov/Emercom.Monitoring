using System;
using System.Linq;

namespace Web.Models.Unit
{
    using System.Collections.Generic;

    using DAL.WCF;
    using DAL.WCF.ServiceReference;

    public class UnitInfoModel
    {
        // сделать отдельными моделями
        public UnitModel Unit { get; }

        public List<MovementModel> MovmentList { get; }

        public UnitInfoModel(Unit unit)
        {
            this.Unit = new UnitModel(unit);

            // движения за последние 5 дней
            this.MovmentList = DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByTimeAndUnitId(
                DateTime.Now.AddDays(-5), DateTime.Now, unit.Id).
                OrderByDescending(m => m.Date).Select(e => 
                new MovementModel(e)).ToList();
        }
    }
}