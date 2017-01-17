namespace DAL.WCF.ServiceReference
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Не уникальное оборудование
    /// </summary>
    public partial class NonUniqEquipmentObject
    {

        #region Тип оборудования

        private kEquipment _kequipment = null;

        /// <summary>
        /// Тип оборудования
        /// </summary>
        public kEquipment kEquipment
        {
            get
            {
                return this._kequipment
                       ?? (this._kequipment =
                           DalContainer.WcfDataManager.kEquipmentList.SingleOrDefault(e => e.Id == this.kEquipmentId));
            }
        }
        #endregion

        #region Поля в таблице

        /// <summary>
        /// Имя оборудования
        /// </summary>
        public string kEquipmentName { get { return this.kEquipment != null ? this.kEquipment.Name : ""; } }

        /// <summary>
        /// Группа оборудования
        /// </summary>
        public string kEquipmentGroup { get { return this.kEquipment != null ? this.kEquipment.GroupName : ""; } }

        /// <summary>
        /// Описание оборудования
        /// </summary>
        public string kEquipmentDescription { get { return this.kEquipment != null ? this.kEquipment.Description : ""; } }

        /// <summary>
        /// Задаётся номер, которому соответствует цвет раскраски таблицы
        /// </summary>
        public int ColorForAppearance { get; set; }

        /// <summary>
        /// Кол-во оборудования в объекте
        /// </summary>
        public int RealCount { get; set; }

        /// <summary>
        /// Поле для таблицы
        /// </summary>
        public string CountStr { get { return this.Count.ToString(); } }

        /// <summary>
        /// Поле для таблицы
        /// </summary>
        public string RealCountStr { get { return this.RealCount.ToString(); } }

        #endregion
    }
}
