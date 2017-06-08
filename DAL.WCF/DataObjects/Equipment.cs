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

        public bool IsInTheUnit { get; set; }
    }
}