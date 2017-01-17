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

    [DataContract]
    [DbTable("Store")]
    public class Store: DbObject
    {
        /// <summary>
        /// Id базы
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
        /// Радиус базы
        /// </summary>
        [DataMember]
        [DbMember("Radius", typeof(double))]
        public double Radius { get; set; }

        /// <summary>
        /// Границы базы
        /// </summary>
        [DataMember]
        [DbMember("StoreBoundaries", typeof(string))]
        public string StoreBoundaries { get; set; }

    }
}
