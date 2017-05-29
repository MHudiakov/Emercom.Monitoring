using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DAL.WCF;
using DAL.WCF.ServiceReference;

namespace Web.Controllers
{
    public class TestApiController : Controller
    {
        [HttpGet]
        public JsonResult GetJsonCarData()
        {
            Unit unit = DalContainer.WcfDataManager.UnitList[0];
            
            var movementList = unit.MovementList.Select(item => new
            {
                item.Id,
                item.UnitId,
                item.EquipmentId,
                item.IsArrived,
                Date = item.Date.ToFileTimeUtc()
            });

            var equipmentList = unit.GetEquipmentList.Select(item => new
            {
                item.Id,
                CarId = item.UnitId,
                item.Name, item.Tag,
                kEquipmentId = item.KEquipmentId,
                item.Description,
                item.LastMovementId
            });

            var unitModel = new
            {
                unit.Id,
                unit.Name,
                unit.Number,
                unit.Description,
                MovementList = movementList,
                EquipmentList = equipmentList
            };

            var jsonData = new JavaScriptSerializer().Serialize(unitModel);
            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet, ContentType = "application/json" };
        }

        [HttpGet]
        public JsonResult GetJsonkEquipment()
        {
            var kEquipmentList =
                DalContainer.WcfDataManager.KEquipmentList.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Description,
                    x.ParentId
                });
            
            var jsonData = new JavaScriptSerializer().Serialize(kEquipmentList);
            return new JsonResult { Data = jsonData, JsonRequestBehavior = JsonRequestBehavior.AllowGet, ContentType = "application/json" };
        }
    }
}
