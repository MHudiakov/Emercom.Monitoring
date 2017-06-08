namespace Server.Dal.Sql.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Оборудование
    /// </summary>
    [DataContract]
    [DbTable("Equipment")]
    public sealed class Equipment : DbObject
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
        /// Id машины, к которой приписано оборудование
        /// </summary>
        [DataMember]
        [DbMember("UnitId", typeof(int))]
        public int UnitId { get; set; }

        /// <summary>
        /// Id классификатора оборудования
        /// </summary>
        [DataMember]
        [DbMember("KEquipmentId", typeof(int))]
        public int KEquipmentId { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DataMember]
        [DbMember("Name", typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// Тэг BLE метки
        /// </summary>
        [DataMember]
        [DbMember("Tag", typeof(string))]
        public string Tag { get; set; }

        /// <summary>
        /// Описание/примечание группы
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
        
        private Movement _lastMovement;

        /// <summary>
        /// Получить последнее движение оборудования
        /// </summary>
        public Movement LastMovement
        {
            get
            {
                if (this._lastMovement == null && this.LastMovementId.HasValue)
                    this._lastMovement = DalContainer.GetDataManager.MovementRepository.Get(LastMovementId);

                return _lastMovement;
            }
            set
            {
                _lastMovement = value;
                LastMovementId = value?.Id;
            }
        }
    }
}