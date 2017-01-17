namespace DAL.WCF.ServiceReference
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using DAL.WCF;
    using Init.DbCore.Metadata;

    public partial class Equipment
    {
        // public Equipment()
        // {
        //     if (this.LastMovementId != null)
        //         _movement = DalContainer.WcfDataManager.ServiceOperationClient.GetMovement(this.LastMovementId);
        // }

        #region Группа

        private Group group = null;

        /// <summary>
        /// Группа оборудования
        /// </summary>
        public Group Group
        {
            get
            {
                if (group == null)
                    group = DalContainer.WcfDataManager.GroupList.SingleOrDefault(e => e.Id == this.kEquipment.GroupId);

                return group;
            }
            set
            {
                group = value;
            }
        }

        #endregion

        #region Тип оборудования

        private kEquipment equipment = null;

        /// <summary>
        /// Класс оборудования
        /// </summary>
        public kEquipment kEquipment
        {
            get
            {
                if (equipment == null)
                    equipment = DalContainer.WcfDataManager.kEquipmentList.SingleOrDefault(e => e.Id == this.kEquipmentId);

                return equipment;
            }
        }

        #endregion

        #region Метка RFId

        private Tag _tag = null;

        /// <summary>
        /// Класс оборудования
        /// </summary>
        public Tag Tag
        {
            get
            {
                return this._tag
                       ?? (this._tag = DalContainer.WcfDataManager.TagList.SingleOrDefault(e => e.Id == this.TagId));
            }
        }

        #endregion

        #region Поля в таблице

        public string RFId { get { return this.Tag.Rfid ?? ""; } }

        public string EquipmentName { get { return this.kEquipment.Name ?? ""; } }

        public string EquipmentGroupName { get { return this.kEquipment.GroupName ?? ""; } }

        public bool IsUniq { get { return this.kEquipment != null ? this.kEquipment.IsUniq : false; } }

        #endregion

        #region Последнее движение

        private Movement _movement = null;
        public Movement LastMovement
        {
            get
            {
                if ((_movement == null) && (this.LastMovementId != null))
                    _movement = DalContainer.WcfDataManager.MovementList.FirstOrDefault(m => m.Id == this.LastMovementId); // ServiceOperationClient.GetMovement(this.LastMovementId);

                return _movement;
            }
        }

        /// <summary>
        /// Id последнего объекта, с/на которого(ый) происходило движение
        /// </summary>
        public int LastUnitId
        {
            get
            {
                return this.LastMovement != null ? this.LastMovement.UnitId : 0;
            }
        }

        /// <summary>
        /// Тип последнего движения - пришло/ушло
        /// </summary>
        public bool IsArrivedBool
        {
            get
            {
                return LastMovement.IsArrived;
            }
        }

        /// <summary>
        /// Является ли последний объект движения складом ?
        /// </summary>
        public bool IsInStore
        {
            get
            {
                if (LastUnitId != 0)
                {
                    var unit = DalContainer.WcfDataManager.UnitList.FirstOrDefault(u => u.Id == this.LastUnitId);

                    return unit.IsStore;
                }
                else
                    return true;
            }
        }
        #endregion

        public int flag { get; set; }
        public int RealCount { get; set; }
    }
}
