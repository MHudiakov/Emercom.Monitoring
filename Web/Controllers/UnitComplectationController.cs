namespace Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using DAL.WCF;

    using Web.Models;

    public class UnitComplectationController : Controller
    {
        [System.Web.Http.HttpGet]
        public ActionResult Index(int id)
        {
            var equipmentList = DalContainer.WcfDataManager.ServiceOperationClient.GetEquipmentListForUnit(id);
            var equipmentGroupList = DalContainer.WcfDataManager.EquipmentGroupList;

            var equipmentModelList= equipmentList.Select(equipment => new EquipmentModel(equipment))
                .OrderBy(equipment => equipment.KEquipmentId)
                .ToList();

            var unitComplectationModelList = equipmentGroupList.
                Select(equipmentGroup => new UnitComplectationModel(equipmentGroup, equipmentModelList.Where(item => item.KEquipment.EquipmentGroup.Id == equipmentGroup.Id).ToList())).
                Where(unitComplectationModel => unitComplectationModel.EquipmentModels.Count > 0).ToList();

            return View(unitComplectationModelList);
        }
    }
}
