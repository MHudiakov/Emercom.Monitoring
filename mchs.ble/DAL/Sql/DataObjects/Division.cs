using System.Collections.Generic;
using System.Linq;

namespace Server.Dal.Sql.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Подразделение МЧС
    /// </summary>
    [DataContract]
    [DbTable("Division")]
    public sealed class Division : DbObject
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        [DbMember("Id", typeof(int))]
        [DbIdentity]
        [DbKey]
        public int Id { get; set; }
 
        /// <summary>
        /// Id родительского подразделения
        /// </summary>
        [DataMember]
        [DbMember("ParentId", typeof(int?))]
        public int? ParentId { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DataMember]
        [DbMember("Name", typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// Описание/примечание группы
        /// </summary>
        [DataMember]
        [DbMember("Description", typeof(string))]
        public string Description { get; set; }

        private readonly List<Division> _childrenList = new List<Division>();

        private List<Division> GetChildrenNodes(List<Division> nodes, Division node)
        {
            var directNodes = DalContainer.GetDataManager.DivisionRepository.GetAll().Where(d => d.ParentId == node.Id).ToList();
            nodes.AddRange(directNodes);
            foreach (var division in directNodes)
            {
                GetChildrenNodes(nodes, division);
            }

            return nodes;
        }

        public List<Division> ChildrenList => GetChildrenNodes(_childrenList, this);

        public List<Unit> GetUnitList =>
            DalContainer.GetDataManager.UnitRepository.GetAll().Where(unit => unit.DivisionId == Id).ToList();
    }
}