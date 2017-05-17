namespace Server.Dal.Sql.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Классификатор оборудования
    /// </summary>
    [DataContract]
    [DbTable("kEquipment")]
    public sealed class KEquipment : DbObject
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
        /// Название
        /// </summary>
        [DataMember]
        [DbMember("Name", typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [DataMember]
        [DbMember("Description", typeof(string))]
        public string Description { get; set; }

        /// <summary>
        /// Id группы, к которой относится оборудование
        /// </summary>
        [DataMember]
        [DbMember("EquipmentGroupId", typeof(int))]
        public int EquipmentGroupId { get; set; }
    }
}