namespace Web.Controllers
{
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

            var equipmentModelList = equipmentList.Select(equipment => new EquipmentModel(equipment))
                .OrderBy(equipment => equipment.KEquipmentId)
                .ToList();

            return View(equipmentModelList);
        }
    }
}
