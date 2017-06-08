namespace Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using DAL.WCF;
    using Models.Unit;
    
    [Authorize(Roles = UserRolesConsts.Administrator)]
    public class UnitController : Controller
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