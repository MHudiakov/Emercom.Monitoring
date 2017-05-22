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

        public List<EquipmentGroup> ChildrenList => GetChildrenNodes(_childrenList, this);

        private readonly List<EquipmentGroup> _childrenList = new List<EquipmentGroup>();

        private List<EquipmentGroup> GetChildrenNodes(List<EquipmentGroup> nodes, EquipmentGroup node)
        {
            var directNodes = DalContainer.WcfDataManager.EquipmentGroupList.Where(d => d.ParentId == node.Id).ToList();
            nodes.AddRange(directNodes);
            foreach (var group in directNodes)
            {
                GetChildrenNodes(nodes, group);
            }

            return nodes;
        }
    }
}