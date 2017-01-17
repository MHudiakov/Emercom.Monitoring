namespace DAL.WCF.ServiceReference
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using DAL.WCF;

    using Init.DbCore.Metadata;

    /// <summary>
    /// Оборудование
    /// </summary>
    public partial class kEquipment
    {
        /// <summary>
        /// Определяем метод преобразования к строке для всех объектов БД
        /// </summary>
        /// <returns>
        /// Название класса оборудования
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }

        #region Навигационные свойства

        /// <summary>
        /// Группа, к которой относится оборудование
        /// </summary> 
        public Group Group 
        {
            get
            {
                if (group == null)
                    group = DalContainer.WcfDataManager.GroupList.SingleOrDefault(e => e.Id == this.GroupId);
                    
                return group;
            } 
        }
        
        private Group group = null;

        /// <summary>
        /// Имя группы оборудования
        /// </summary>
        public string GroupName { get { return Group.Name ?? ""; } }

        #endregion

    }
}
