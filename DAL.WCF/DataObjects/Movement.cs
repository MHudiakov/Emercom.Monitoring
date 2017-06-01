namespace DAL.WCF.ServiceReference
{
    using System.Linq;
    using WCF;

    /// <summary>
    /// Движение по складу
    /// </summary>
    public partial class Movement
    {
        public Unit Unit => DalContainer.WcfDataManager.UnitList.SingleOrDefault(e => e.Id == UnitId);
        
        public Equipment Equipment =>
            DalContainer.WcfDataManager.EquipmentList.SingleOrDefault(e => e.Id == EquipmentId);
    }
}