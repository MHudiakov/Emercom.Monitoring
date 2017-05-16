﻿namespace Server.Dal.Sql.DataObjects
{
    using System;
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Движение оборудования
    /// </summary>
    [DataContract]
    [DbTable("Movement")]
    public class Movement : DbObject
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
        /// Id оборудования
        /// </summary>
        [DataMember]
        [DbMember("EquipmentId", typeof(int))]
        public int EquipmentId { get; set; }

        /// <summary>
        /// Ид машины
        /// </summary>
        [DataMember]
        [DbMember("CarId", typeof(int))]
        public int CarId { get; set; }

        /// <summary>
        /// Прибыло/Убыло
        /// </summary>
        [DataMember]
        [DbMember("IsArrived", typeof(int))]
        public bool IsArrived { get; set; }

        /// <summary>
        /// Дата перемещения
        /// </summary>
        [DataMember]
        [DbMember("Date", typeof(DateTime))]
        public DateTime Date { get; set; }
    }
}