using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Dal.SQL.DataObjects
{
    using System.Runtime.Serialization;

    using DAL;
    using DAL.SQL.DataObjects;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Комплектация в конце и начале поездки
    /// </summary>
    [DataContract]
    [DbTable("TripComplectation")]
    public class TripComplectation : DbObject
    {
        /// <summary>
        /// Id 
        /// </summary>
        [DataMember]
        [DbMember("Id", typeof(int))]
        [DbIdentity]
        [DbKey]
        public int Id { get; set; }

        /// <summary>
        /// Id поездки
        /// </summary>
        [DataMember]
        [DbMember("TripId", typeof(int))]
        public int TripId { get; set; }

        /// <summary>
        /// Ид оборудования
        /// </summary>
        [DataMember]
        [DbMember("EquipmentId", typeof(int))]
        public int EquipmentId { get; set; }

        /// <summary>
        /// Начало/окончание поездки
        /// </summary>
        [DataMember]
        [DbMember("IsStart", typeof(int))]
        public bool IsStart { get; set; }

        /// <summary>
        /// Gets the equipment.
        /// </summary>
        public Equipment Equipment
        {
            get
            {
                return DalContainer.DataManager.EquipmentRepository.Get(EquipmentId);
            }
        }
    }
}
