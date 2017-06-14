using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.UnitComplectation
{
    public class UnitComplectationIndexModel
    {
        public int UnitId { get; set; }

        public UnitComplectationIndexModel(int unitId)
        {
            UnitId = unitId;
        }
    }
}