using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using DAL.WCF;
using Web.Models.Division;

namespace Web.Controllers
{
    /// <summary>
    /// Контроллер подразделений
    /// </summary>
    [Authorize(Roles = UserRolesConsts.Administrator)]
    public class DivisionController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            var filter = new FilterDivisionModel();
            return View(filter);
        }

        public PartialViewResult List(FilterDivisionModel filter)
        {
            var treeSortedDivisionList = DalContainer.WcfDataManager.ServiceOperationClient.GetTreeSortedDivisionList();
            var result = filter.SearchPattern.IsEmpty() ?
                treeSortedDivisionList :
                treeSortedDivisionList.Where(division => division.Name.ToLower().Contains(filter.SearchPattern.ToLower())).ToList();

            var divisionModelList = result.Select(division => new DivisionModel(division)).ToList();
            return PartialView(divisionModelList);
        }
    }
}
