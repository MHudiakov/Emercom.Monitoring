// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tag.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Тег
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Server.Dal.SQL.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    [DataContract]
    [DbTable("Tag")]
    public class Tag : DbObject
    {
        /// <summary>
        /// Id 
        /// </summary>
        [DataMember]
        [DbMember("Id", typeof(int))]
        [DbIdentityAttribute]
        [DbKeyAttribute]
        public int Id { get; set; }

        /// <summary>
        /// Id оборудования к которому привязан тег
        /// </summary>
        [DataMember]
        [DbMember("EquipmentId", typeof(int?))]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// Тег 
        /// </summary>
        [DataMember]
        [DbMember("Rfid", typeof(string))]
        public string Rfid { get; set; }

    }
}

