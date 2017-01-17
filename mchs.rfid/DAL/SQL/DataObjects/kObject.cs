// --------------------------------------------------------------------------------------------------------------------
// <copyright file="kObject.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Тип объекта
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Server.Dal.SQL.DataObjects
{
    using System;
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Тип объекта
    /// </summary>
    [DataContract]
    [DbTable("kObject")]
    public class kObject : DbObject
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
        /// Тип объекта
        /// </summary>
        [DataMember]
        [DbMember("Type", typeof(string))]
        public string Type { get; set; }

        /// <summary>
        /// Описание/примечание типа объекта
        /// </summary>
        [DataMember]
        [DbMember("Description", typeof(string))]
        public string Description { get; set; }
        
        /// <summary>
        /// Является ли тип объекта складом
        /// </summary>
        [DataMember]
        [DbMember("IsStore", typeof(int))]
        public bool IsStore { get; set; }
    }
}
