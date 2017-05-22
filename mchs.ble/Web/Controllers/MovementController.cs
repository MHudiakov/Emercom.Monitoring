// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovementController.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Контроллер раздела "Движение по складу"
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using DAL.WCF;
    using Web.Models;

    /// <summary>
    /// Контроллер раздела "Движение по складу"
    /// </summary>
    public class MovementController : Controller
    {
        /// <summary>
        /// Главная страница раздела "Движение по складу"
        /// </summary>
        /// <returns>Представление</returns>
        [HttpGet]
        public ActionResult Index()
        {
            var filter = new FilterMovementModel();

            return View(filter);
        }

        /// <summary>
        /// Отображает отчет аналитики
        /// </summary>
        /// <param name="filter">Настройки отчёта</param>
        /// <returns>Частичное представление</returns>
        public ActionResult List(FilterMovementModel filter)
        {
            var movementList = DalContainer.WcfDataManager.MovementList;

            if ((filter.DtBegin != null) && (filter.DtEnd != null))
                movementList = movementList.Where(e => e.DateOfMovement.Date >= filter.DtBegin && e.DateOfMovement.Date <= filter.DtEnd).ToList();

            else if (filter.DtBegin != null)
                movementList = movementList.Where(e => e.DateOfMovement.Date >= filter.DtBegin).ToList();

            else if (filter.DtEnd != null)
                movementList = movementList.Where(e => e.DateOfMovement.Date <= filter.DtEnd).ToList();

            if (filter.EquipmentId != null)
                movementList = movementList.Where(movement => movement.KEquipment.Id == filter.EquipmentId).ToList();


            var movementModelList = movementList.Select(movement => new MovementModel(movement))
                .OrderBy(movement => movement.DateOfMovement)
                .ToList();

            return PartialView(movementModelList);
        }
	}
}