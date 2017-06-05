namespace DAL.WCF.ServiceReference
{
    using System.Linq;
    using System.Collections.Generic;
    using WCF;

    /// <summary>
    /// Группа оборудования
    /// </summary>
    public partial class EquipmentGroup
    {
        /// <summary>
        /// Оборудование группы
        /// </summary>
        public List<KEquipment> KEquipmentList => DalContainer.WcfDataManager.KEquipmentList
            .Where(e => e.EquipmentGroupId == Id)
            .ToList();

        public List<EquipmentGroup> ChildrenList
        {
            get
            {
                var childrenList = new List<EquipmentGroup>();
                return GetChildrenNodes(childrenList, this);
            }
        }

        private List<EquipmentGroup> GetChildrenNodes(List<EquipmentGroup> nodes, EquipmentGroup node)
        {
            var directNodes = DalContainer.WcfDataManager.EquipmentGroupList.Where(d => d.ParentId == node.Id).ToList();
            foreach (var group in directNodes)
            {
                nodes.Add(group);
                GetChildrenNodes(nodes, group);
            }

            return nodes;
        }
    }
}