using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using DAL.WCF;
using Web.Models.Division;

namespace Web.Controllers
{
    using System.Collections.Generic;

    using DAL.WCF.ServiceReference;

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
            var divisionList = filter.SearchPattern.IsEmpty() ?
                DalContainer.WcfDataManager.DivisionList.ToList() :
                DalContainer.WcfDataManager.DivisionList.Where(division => division.Name.Contains(filter.SearchPattern)).ToList();
            var sortedList = new List<Division>();

            foreach (var division in divisionList.Where(d => !d.ParentId.HasValue))
            {
                sortedList.Add(division);
                GetGhilds(division, sortedList, divisionList);
            }

            var divisionModelList = divisionList.Select(division => new DivisionModel(division)).ToList();
            return PartialView(divisionModelList);
        }

        private void GetGhilds(Division parentDivision, List<Division> sortedList, List<Division> divisionList)
        {
            foreach (var division in divisionList.Where(x => x.ParentId == parentDivision.Id))
            {
                sortedList.Add(division);
                GetGhilds(division, sortedList, divisionList);
            }
        }

    }
}
