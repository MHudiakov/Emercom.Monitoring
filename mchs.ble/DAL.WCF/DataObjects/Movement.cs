namespace DAL.WCF.ServiceReference
{
    using System.Linq;
    using WCF;

    /// <summary>
    /// Движение по складу
    /// </summary>
    public partial class Movement
    {
        #region Навигационные свойства

        private Unit _unit;
        
        public Unit Unit
        {
            get
            {
                return _unit ?? (_unit = DalContainer.WcfDataManager.UnitList.SingleOrDefault(e => e.Id == UnitId));
            }
        }

        public string UnitName => Unit != null ? Unit.Name : "";

        private Equipment _equipment;
        
        public Equipment Equipment 
        { 
            get
            {
                return _equipment ?? (_equipment =
                           DalContainer.WcfDataManager.EquipmentList.SingleOrDefault(e => e.Id == EquipmentId));
            }
        }

        public string EquipmentName => Equipment != null ? Equipment.Name : "";
        
        public int ArrivedInt => IsArrived ? 1 : 0;

        /// <summary>
        /// Прибыло/Убыло
        /// </summary>
        public string Arrived => IsArrived ? "Прибыло" : "Убыло";

        #endregion
    }
}