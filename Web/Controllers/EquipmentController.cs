namespace Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using DAL.WCF;
    using Models;

    /// <summary>
    /// Контроллер раздела "Оборудование"
    /// </summary>
    public class EquipmentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var filter = new FilterEquipmentModel();
            return View(filter);
        }

        /// <summary>
        /// Отображает отчет аналитики
        /// </summary>
        /// <param name="filter">Настройки отчёта</param>
        /// <returns>Частичное представление</returns>
        public PartialViewResult List(FilterEquipmentModel filter)
        {
            var equipmentList = DalContainer.WcfDataManager.EquipmentList;

            if (filter.kEquipmentId != null)
                equipmentList = equipmentList.Where(equipment => equipment.KEquipmentId == filter.kEquipmentId).ToList();

            var equipmentModelList = equipmentList.OrderBy(equipment => equipment.KEquipmentId).
                Select(equipment => new EquipmentModel(equipment)).ToList();

            return PartialView(equipmentModelList);
        }
    }
}