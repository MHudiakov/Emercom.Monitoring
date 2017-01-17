// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Group.cs" company="ИНИТ-центр">
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
    [DbTable("kGroup")]
    [DataContract]
    public class Group : DbObject
    {
        /// <summary>
        /// Id группы оборудования
        /// </summary>
        [DataMember]
        [DbMember("Id", typeof(int))]
        [DbIdentityAttribute]
        [DbKeyAttribute]
        public int Id { get; set; }

        /// <summary>
        /// Id группы оборудования, которой принадлежит данная
        /// </summary>
        [DataMember]
        [DbMember("ParentId", typeof(int?))]
        public int? ParentId { get; set; }

        /// <summary>
        /// Название группы
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