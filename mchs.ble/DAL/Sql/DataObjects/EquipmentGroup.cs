namespace Server.Dal.Sql.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Группа оборудования
    /// </summary>   
    [DbTable("EquipmentGroup")]
    [DataContract]
    public class EquipmentGroup : DbObject
    {
        /// <summary>
        /// Id группы оборудования
        /// </summary>
        [DataMember]
        [DbMember("Id", typeof(int))]
        [DbIdentity]
        [DbKey]
        public int Id { get; set; }

        /// <summary>
        /// Id родительской группы
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
    }
}