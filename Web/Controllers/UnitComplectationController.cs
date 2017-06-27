using DAL.WCF.ServiceReference;
using Web.Models.UnitComplectation;
using Web.Wrappers;
using WebGrease.Css.Extensions;

namespace Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using DAL.WCF;

    [CustomAuthorize]
    public class UnitComplectationController : Controller
    {
        public ActionResult Index(int unitId)
        {
            // Load PTV for unit
            var model = new UnitComplectationIndexModel(unitId);
            return View(model);
        }


        public PartialViewResult UnitComplectationTablePartial(int unitId)
        {
            IEnumerable<Equipment> equipmentList = DalContainer.WcfDataManager.ServiceOperationClient.GetEquipmentListForUnit(unitId);

            // Load current unit complectation
            IEnumerable<Equipment> currentComplectation = DalContainer.WcfDataManager.ServiceOperationClient
                .GetCurrentComplectationForUnit(unitId);

            // Check for every item in PTV if this item in the unit now
            equipmentList.ForEach(equipment => equipment.IsInTheUnit = currentComplectation.Any(item => item.Id == equipment.Id));

            // Group equipment by equipment froups
            IEnumerable<IGrouping<EquipmentGroup, Equipment>> equipmentGroups =
                equipmentList.GroupBy(equipment => equipment.KEquipment.EquipmentGroup).OrderBy(group => group.Key.Name);

            List<UnitComplectationModel> unitComplectationModelList =
                equipmentGroups.Select(equipmentGroup => new UnitComplectationModel(equipmentGroup)).ToList();

            var unitComplectationListModel = new UnitComplectationListModel(unitComplectationModelList, unitId);

            return PartialView(unitComplectationListModel);
        }
    }
}