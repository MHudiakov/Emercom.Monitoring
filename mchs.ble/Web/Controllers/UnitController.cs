// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EquipmentController.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Контроллер раздела "Оборудование на складе"
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Web.Controllers
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using DAL.WCF;
    using Web.Models.Unit;

    /// <summary>
    /// Контроллер раздела "Оборудование на складе"
    /// </summary>
    public class UnitController : Controller
    {
        /// <summary>
        /// Главная страница раздела "Наличие на складе"
        /// </summary>
        /// <returns>Представление</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var model = DalContainer.WcfDataManager.UnitList.Select(u => new UnitInfoModel(u)).ToList();
            return View(model);
        }

        public ActionResult UnitInfo(int id)
        {
            var unit = DalContainer.WcfDataManager.UnitList.FirstOrDefault(u => u.Id == id);
            var model = new UnitInfoModel(unit);
            return View(model);
        }

        public ActionResult UnitInfoPartial(int id)
        {
            var unit = DalContainer.WcfDataManager.UnitList.FirstOrDefault(u => u.Id == id);
            var model = new UnitInfoModel(unit);
            return PartialView(model);
        }

        public ActionResult UnitMovementPartial(int id)
        {
            var unit = DalContainer.WcfDataManager.UnitList.FirstOrDefault(u => u.Id == id);
            var model = new UnitInfoModel(unit);
            return PartialView(model);
        }

        public JsonResult GetJsonUnitData()
        {
            var units = DalContainer.WcfDataManager.UnitList;
            var model = units.Select(e => new UnitJsonModel(e)).ToList();
            var sjSerializer = new JavaScriptSerializer();
            var json = sjSerializer.Serialize(model);
            var array = sjSerializer.DeserializeObject(json);
            return new JsonResult { Data = array, JsonRequestBehavior = JsonRequestBehavior.AllowGet, ContentType = "application/json" };
        }
    }
}