using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Dal.SQL.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Поездки объектов
    /// </summary>
    [DataContract]
    [DbTable("Trip")]
    public class Trip : DbObject
    {
        /// <summary>
        /// Id поездки
        /// </summary>
        [DataMember]
        [DbMember("Id", typeof(int))]
        [DbIdentity]
        [DbKey]
        public int Id { get; set; }

        /// <summary>
        /// Id объекта
        /// </summary>
        [DataMember]
        [DbMember("UnitId", typeof(int))]
        public int UnitId { get; set; }

        /// <summary>
        /// Время начала
        /// </summary>
        [DataMember]
        [DbMember("StartTime", typeof(DateTime))]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время окончания
        /// </summary>
        [DataMember]
        [DbMember("EndTime", typeof(DateTime))]
        public DateTime EndTime { get; set; }
    }
}
