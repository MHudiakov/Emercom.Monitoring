using System.Linq;

namespace DAL.WCF.ServiceReference
{
    public partial class User
    {
        public Division GetDivision => DalContainer.WcfDataManager.DivisionList.Single(d => d.Id == DivisionId);
    }
}