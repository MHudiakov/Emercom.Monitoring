namespace DAL.WCF.ServiceReference
{
    using System.Linq;
    using WCF;

    public partial class Equipment
    {

        #region Группа

        private EquipmentGroup _group;

        /// <summary>
        /// Группа оборудования
        /// </summary>
        public EquipmentGroup EquipmentGroup
        {
            get
            {
                return _group ?? (_group =
                           DalContainer.WcfDataManager.EquipmentGroupList.SingleOrDefault(
                               e => e.Id == KEquipment.EquipmentGroupId));
            }
        }

        #endregion

        #region Тип оборудования

        private KEquipment _kEquipment;

        /// <summary>
        /// Класс оборудования
        /// </summary>
        public KEquipment KEquipment
        {
            get
            {
                if (_kEquipment == null)
                    _kEquipment = DalContainer.WcfDataManager.KEquipmentList.SingleOrDefault(e => e.Id == this.KEquipmentId);

                return _kEquipment;
            }
        }

        #endregion

        #region Последнее движение

        private Movement _lastMovement;
        public Movement LastMovement
        {
            get
            {
                if (_lastMovement != null || LastMovementId == null) return _lastMovement;
                if (LastMovementId != null)
                    _lastMovement = DalContainer.WcfDataManager.ServiceOperationClient.GetMovement((int)LastMovementId);

                return _lastMovement;
            }
        }

        /// <summary>
        /// Id последнего объекта, с/на которого(ый) происходило движение
        /// </summary>
        public int LastUnitId => LastMovement?.UnitId ?? 0;

        /// <summary>
        /// Тип последнего движения - пришло/ушло
        /// </summary>
        public bool IsArrivedBool => LastMovement.IsArrived;

        #endregion
    }
}