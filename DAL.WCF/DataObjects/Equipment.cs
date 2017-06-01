namespace DAL.WCF.ServiceReference
{
    using System.Linq;
    using WCF;

    public partial class Equipment
    {
        /// <summary>
        /// Класс оборудования
        /// </summary>
        public KEquipment KEquipment => DalContainer.WcfDataManager.KEquipmentList.SingleOrDefault(e => e.Id == this.KEquipmentId);
        
        public Movement LastMovement => LastMovementId != null 
            ? DalContainer.WcfDataManager.ServiceOperationClient.GetMovement((int)LastMovementId) 
            : null;
    }
}