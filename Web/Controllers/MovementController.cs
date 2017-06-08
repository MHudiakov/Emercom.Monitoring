// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovementController.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Контроллер раздела "Движение по складу"
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using DAL.WCF.ServiceReference;

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
        public ActionResult Index(int unitId)
        {
            var filter = new FilterMovementModel();
            filter.UnitId = unitId;
            return View(filter);
        }

        /// <summary>
        /// Отображает отчет аналитики
        /// </summary>
        /// <param name="filter">Настройки отчёта</param>
        /// <returns>Частичное представление</returns>
        public ActionResult List(FilterMovementModel filter)
        {
            var movementList = new List<Movement>();

            if ((filter.DtBegin != null) && (filter.DtEnd != null))
                movementList = movementList.Where(e => e.Date.Date >= filter.DtBegin && e.Date.Date <= filter.DtEnd).ToList();

            else if (filter.DtBegin != null)
                movementList = movementList.Where(e => e.Date.Date >= filter.DtBegin).ToList();

            else if (filter.DtEnd != null)
                movementList = movementList.Where(e => e.Date.Date <= filter.DtEnd).ToList();

            if (filter.EquipmentId != null)
                movementList = movementList.Where(movement => movement.EquipmentId == filter.EquipmentId).ToList();


            var movementModelList = movementList.Select(movement => new MovementModel(movement))
                .OrderBy(movement => movement.Date)
                .ToList();

            return PartialView(movementModelList);
        }
	}
}