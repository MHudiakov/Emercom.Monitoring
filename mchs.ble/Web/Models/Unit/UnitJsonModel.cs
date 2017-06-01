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

        /// <summary>
        /// Название объекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание/примечание объекта
        /// </summary>
        public string Description { get; set; }

        public List<MovementModel> MovmentList { get; }

        public UnitJsonModel(Unit unit)
        {
            this.Id = unit.Id;
            this.Name = unit.Name;
            this.Description = unit.Description;
            
            // последние 50 движений которые отображаем
            this.MovmentList = DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByTimeAndUnitId(
                    DateTime.Now.AddDays(-5), DateTime.Now, unit.Id).
                OrderByDescending(m => m.Date).Select(e =>
                    new MovementModel(e)).ToList();
        }
    }
}