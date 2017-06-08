using System;
using System.Collections.Generic;

namespace Web.Models.UnitComplectation
{
    public class UnitComplectationListModel
    {
        public UnitComplectationListModel(List<UnitComplectationModel> unitComplectationModels, int unitId)
        {
            if (unitComplectationModels == null)
            {
                throw new NullReferenceException(nameof(unitComplectationModels));
            }

            this.UnitComplectationModels = unitComplectationModels;
            this.UnitId = unitId;
        }

        public List<UnitComplectationModel> UnitComplectationModels { get; set; }

        public int UnitId { get; set; }
    }
}