using System.Linq;
using System.Web.Mvc;
using DAL.WCF;

namespace Web.Models.User
{
    public class FilterUserModel
    {
        public int? DivisionId { get; set; }

        public SelectList DivisionList
        {
            get
            {
                var dividionList = DalContainer.WcfDataManager.DivisionList;
                return new SelectList(dividionList.OrderBy(e => e.Name), "Id", "Name");
            }
        }
    }
}