// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NonUniqEquipmentObject.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Не уникальное оборудование для машины
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Server.Dal.SQL.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Не уникальное оборудование для машины
    /// </summary>
    [DataContract]
    [DbTable("NonUniqEquipmentObject")]
    public class NonUniqEquipmentObject : DbObject
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
        /// Id типа оборудования
        /// </summary>
        [DataMember]
        [DbMember("kEquipmentId", typeof(int))]
        public int kEquipmentId { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        [DataMember]
        [DbMember("Count", typeof(int))]
        public int Count { get; set; }
    }
}
