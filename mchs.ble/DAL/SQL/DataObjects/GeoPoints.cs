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
    /// Гео метка
    /// </summary>
    [DataContract]
    [DbTable("GeoPoints")]
    public class GeoPoints : DbObject
    {
        /// <summary>
        /// Id точки
        /// </summary>
        [DataMember]
        [DbMember("Id", typeof(int))]
        [DbIdentity]
        [DbKey]
        public int Id { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        [DataMember]
        [DbMember("Latitude", typeof(double))]
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [DataMember]
        [DbMember("Longitude", typeof(double))]
        public double Longitude { get; set; }

        /// <summary>
        /// Id объекта
        /// </summary>
        [DataMember]
        [DbMember("UnitId", typeof(int))]
        public int UnitId { get; set; }

        /// <summary>
        /// Id поездки
        /// </summary>
        [DataMember]
        [DbMember("TripId", typeof(int))]
        public int TripId { get; set; }

        /// <summary>
        /// Время
        /// </summary>
        [DataMember]
        [DbMember("Time", typeof(DateTime))]
        public DateTime Time { get; set; }

        /// <summary>
        /// Скорость (км/ч)
        /// </summary>
        [DataMember]
        [DbMember("Speed", typeof(double))]
        public double Speed { get; set; }
    }
}
