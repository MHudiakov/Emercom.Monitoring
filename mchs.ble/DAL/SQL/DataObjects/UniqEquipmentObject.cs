// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UniqEquipmentObject.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Уникальное оборудование для машины
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Server.Dal.SQL.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;
    
    /// <summary>
    /// Уникальное оборудование для машины
    /// </summary>
    [DataContract]
    [DbTable("UniqEquipmentObject")]
    public class UniqEquipmentObject : DbObject
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
        /// Id объекта
        /// </summary>
        [DataMember]
        [DbMember("UnitId", typeof(int))]
        public int UnitId { get; set; }

        /// <summary>
        /// Id оборудования
        /// </summary>
        [DataMember]
        [DbMember("EquipmentId", typeof(int))]
        public int EquipmentId { get; set; }
    }
}
