namespace DAL.WCF.ServiceReference
{
    using System.Linq;
    using System.Collections.Generic;
    using WCF;

    /// <summary>
    /// Группа оборудования
    /// </summary>
    public partial class EquipmentGroup
    {
        /// <summary>
        /// Определяем метод преобразования к строке для всех объектов БД
        /// </summary>
        /// <returns>
        /// Название группы
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        #region Навигационные свойства

        private List<KEquipment> _kEquipmentList;

        /// <summary>
        /// Оборудование группы
        /// </summary>
        public List<KEquipment> KEquipmentList
        {
            get
            {
                return _kEquipmentList ?? (_kEquipmentList =
                           DalContainer.WcfDataManager.KEquipmentList.Where(e => e.EquipmentGroupId == Id)
                               .ToList());
            }
        }

        private List<EquipmentGroup> _innerGroups;

        /// <summary>
        /// Вложенные каталоги
        /// </summary>
        public List<EquipmentGroup> InnerGroups
        {
            get
            {
                return _innerGroups
                       ?? (_innerGroups =
                           DalContainer.WcfDataManager.EquipmentGroupList.Where(
                               e => e.ParentId == Id && e.Id != Id).ToList());
            }
        }

        #endregion

        public List<KEquipment> KEquipmentsList
        {
            get
            {
                var list = KEquipmentList.ToList();

                foreach (var innerGroup in InnerGroups.Where(innerGroup => innerGroup.KEquipmentsList != null))
                    list.AddRange(innerGroup.KEquipmentsList);

                return list;
            }
        }
    }
}