using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.WCF.ServiceReference
{
    public partial class Division
    {
        public Division GetParent => ParentId != null
            ? DalContainer.WcfDataManager.DivisionList.Single(d => d.Id == this.ParentId)
            : null;

        private List<Division> GetChildrenNodes(List<Division> nodes, Division node)
        {
            var directNodes = DalContainer.WcfDataManager.DivisionList.Where(d => d.ParentId == node.Id).ToList();

            nodes.AddRange(directNodes);
            foreach (var division in directNodes)
            {
                GetChildrenNodes(nodes, division);
            }

            return nodes;
        }

        public List<Division> ChildrenList
        {
            get
            {
                var childrenList = new List<Division>();
                return GetChildrenNodes(childrenList, this);
            }
        }

        public List<User> GetUserList =>
            DalContainer.WcfDataManager.UserList.Where(user => user.DivisionId == Id).ToList();
    }
}