// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Movement.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Движение по складу
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.SQL.DataObjects
{
    using System;
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Движение по складу
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
        [DbIdentityAttribute]
        [DbKeyAttribute]
        public int Id { get; set; }

        /// <summary>
        /// Id наличия на складе
        /// </summary>
        [DataMember]
        [DbMember("EquipmentId", typeof(int?))]
        public int? EquipmentId { get; set; }

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
        [DbMember("DateOfMovement", typeof(DateTime))]
        public DateTime DateOfMovement { get; set; }

        /// <summary>
        /// Ид объекта
        /// </summary>
        [DataMember]
        [DbMember("UnitId", typeof(int))]
        public int UnitId { get; set; }

        /// <summary>
        /// Ссылка на объект
        /// </summary>
        public Equipment Equipment
        {
            get
            {
                return DalContainer.DataManager.EquipmentRepository.Get(this.EquipmentId);
            }
        }

    }
}