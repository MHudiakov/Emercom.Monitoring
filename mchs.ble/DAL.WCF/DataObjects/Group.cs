namespace DAL.WCF.ServiceReference
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics;

    using DAL.WCF;

    using Init.DbCore.Metadata;

    /// <summary>
    /// Группа оборудования
    /// </summary>
    public partial class Group
    {
        /// <summary>
        /// Определяем метод преобразования к строке для всех объектов БД
        /// </summary>
        /// <returns>
        /// Название группы
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }

        #region Навигационные свойства

        private List<kEquipment> _listkEquipment = null;

        /// <summary>
        /// Оборудование группы
        /// </summary>
        public List<kEquipment> ListkEquipment
        {
            get
            {
                if (_listkEquipment == null)
                    _listkEquipment = DalContainer.WcfDataManager.kEquipmentList.Where(e => e.GroupId == this.Id).ToList();

                return _listkEquipment;
            }
        }

        private List<Group> _innerGroups = null;

        /// <summary>
        /// Вложенные каталоги
        /// </summary>
        public List<Group> InnerGroups
        {
            get
            {
                return this._innerGroups
                       ?? (this._innerGroups =
                           DalContainer.WcfDataManager.GroupList.Where(
                               e => e.ParentId == this.Id && e.Id != this.Id).ToList());
            }
        }

        #endregion

        public List<kEquipment> kEquipmentsList
        {
            get
            {
                var list = DalContainer.WcfDataManager.kEquipmentList.Where(x => x.GroupId == this.Id).ToList();

                foreach (var innerGroup in this.InnerGroups.Where(innerGroup => innerGroup.kEquipmentsList != null))
                    list.AddRange(innerGroup.kEquipmentsList);

                return list;
            }
        }


    }
}