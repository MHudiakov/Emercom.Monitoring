// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Equipment.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Наличие оборудования на складе
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.SQL.DataObjects
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;
    using Init.Tools;

    /// <summary>
    /// Наличие оборудования на складе
    /// </summary>
    [DataContract]
    [DbTable("Equipment")]
    public class Equipment : DbObject
    {
        /// <summary>
        /// Id оборудования
        /// </summary>
        [DataMember]
        [DbMember("Id", typeof(int))]
        [DbIdentityAttribute]
        [DbKeyAttribute]
        public int Id { get; set; }

        /// <summary>
        /// Id класса оборудования, к которому относится данное
        /// </summary>
        [DataMember]
        [DbMember("kEquipmentId", typeof(int))]
        public int kEquipmentId { get; set; }

        /// <summary>
        /// Описание/примечание
        /// </summary>
        [DataMember]
        [DbMember("Description", typeof(string))]
        public string Description { get; set; }

        /// <summary>
        /// Ссылка на последнее движение
        /// </summary>
        [DataMember]
        [DbMember("LastMovementId", typeof(int?))]
        public int? LastMovementId { get; set; }

        /// <summary>
        /// RF id оборудования
        /// </summary>
        [DataMember]
        [DbMember("TagId", typeof(int))]
        public int TagId { get; set; }


        /// <summary>
        /// Ссылка на последнее передвижение
        /// </summary>
        public Movement LastMovement
        {
            get
            {
                var lastMovementId = this.LastMovementId;
                return lastMovementId != null ? DalContainer.DataManager.MovementRepository.Get(lastMovementId.Value) : null;
            }
        }

        /// <summary>
        /// Id последнего объекта, с/на которого(ый) происходило движение
        /// </summary>
        public int? LastUnitId
        {
            get
            {
                return this.LastMovement != null ? this.LastMovement.UnitId : (int?)null;
            }
        }

        /// <summary>
        /// Тип последнего движения - пришло/ушло
        /// </summary>
        public bool IsArrived
        {
            get
            {
                return this.LastMovement != null && this.LastMovement.IsArrived;
            }
        }
    }
}