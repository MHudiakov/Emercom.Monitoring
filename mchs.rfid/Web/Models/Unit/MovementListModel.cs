using System.Collections.Generic;
using System.Linq;

namespace Web.Models.Unit
{
    using DAL.WCF.ServiceReference;

    public class MovementListModel
    {
        public MovementListModel(IEnumerable<Movement> list)
        {
            this.List = list.Select(e => new MovementModel(e)).ToList();
        }

        public List<MovementModel> List { get; set; }
    }
}