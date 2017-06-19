// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Контроллер главной страницы
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using DAL.WCF;
using Web.Models.Unit;
using Web.Wrappers;

namespace Web.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Контроллер главной страницы
    /// </summary>
    [CustomAuthorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Получение главной страницы
        /// </summary>
        /// <returns>Главная страницы</returns>
        public ActionResult Index()
        {
            var filter = new FilterUnitModel();
            return View(filter);
        }

        public PartialViewResult List(FilterUnitModel filter)
        {
            var unitList = filter.DivisionId == null ?
                DalContainer.WcfDataManager.UnitList :
                DalContainer.WcfDataManager.UnitList.Where(user => user.DivisionId == filter.DivisionId);

            var unitModelList = unitList.Select(unit => new UnitModel(unit)).OrderBy(user => user.DivisionName).ThenBy(user => user.Name).ToList();
            return PartialView(unitModelList);
        }
    }
}
