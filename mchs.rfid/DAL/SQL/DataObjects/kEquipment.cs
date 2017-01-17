// --------------------------------------------------------------------------------------------------------------------
// <copyright file="kEquipment.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Оборудование
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.SQL.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Оборудование
    /// </summary>
    [DataContract]
    [DbTable("kEquipment")]
    public class kEquipment : DbObject
    {
        /// <summary>
        /// Id оборудования
        /// </summary>
        [DataMember]
        [DbMember("Id", typeof(int))]
        [DbIdentityAttribute]
        [DbKeyAttribute]
        public int Id { get; set; }

        /// <summary>
        /// Название оборудования
        /// </summary>
        [DataMember]
        [DbMember("Name", typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// Описание/примечание оборудования
        /// </summary>
        [DataMember]
        [DbMember("Description", typeof(string))]
        public string Description { get; set; }

        /// <summary>
        /// Id группы, к которой относится оборудование
        /// </summary>
        [DataMember]
        [DbMember("kGroupId", typeof(int))]
        public int GroupId { get; set; }

        /// <summary>
        /// Флаг, указывающий на уникальность этого оборудования
        /// </summary>
        [DataMember]
        [DbMember("IsUniq", typeof(int))]
        public bool IsUniq { get; set; }
    }
}