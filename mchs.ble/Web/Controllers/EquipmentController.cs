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
    using System.Web.Mvc;

    using DAL.WCF;
    using Web.Models;

    /// <summary>
    /// Контроллер раздела "Оборудование на складе"
    /// </summary>
    public class EquipmentController : Controller
    {
        /// <summary>
        /// Главная страница раздела "Наличие на складе"
        /// </summary>
        /// <returns>Представление</returns>
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
        public ActionResult List(FilterEquipmentModel filter)
        {
            var equipmentList = DalContainer.WcfDataManager.EquipmentList;

            if (filter.kEquipmentId != null)
               equipmentList = equipmentList.Where(equipment => equipment.kEquipmentId == filter.kEquipmentId).ToList();

            var equipmentModelList = equipmentList.Select(equipment => new EquipmentModel(equipment))
                .OrderBy(equipment => equipment.Equipment.RFId)
                .ToList();

            return PartialView(equipmentModelList);
        }
    }
}