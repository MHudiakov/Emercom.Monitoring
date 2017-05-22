namespace DAL.WCF.ServiceReference
{
    using System.Linq;
    using WCF;

    /// <summary>
    /// Оборудование
    /// </summary>
    public partial class KEquipment
    {
        /// <summary>
        /// Определяем метод преобразования к строке для всех объектов БД
        /// </summary>
        /// <returns>
        /// Название класса оборудования
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        #region Навигационные свойства

        /// <summary>
        /// Группа, к которой относится оборудование
        /// </summary> 
        public EquipmentGroup EquipmentGroup 
        {
            get
            {
                return _group ?? (_group =
                           DalContainer.WcfDataManager.EquipmentGroupList.SingleOrDefault(
                               e => e.Id == this.EquipmentGroupId));
            } 
        }
        
        private EquipmentGroup _group;

        #endregion

    }
}
