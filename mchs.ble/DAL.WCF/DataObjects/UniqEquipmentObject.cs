namespace DAL.WCF.ServiceReference
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class UniqEquipmentObject
    {

        #region Оборудование

        private Equipment _equipment = null;

        /// <summary>
        /// Наличие оборудования на складе
        /// </summary>
        public Equipment Equipment
        {
            get
            {
                return this._equipment
                       ?? (this._equipment =
                           DalContainer.WcfDataManager.EquipmentList.SingleOrDefault(e => e.Id == this.EquipmentId));
            }
        }

        #endregion

        #region Тип оборудования

        private kEquipment _kequipment = null;

        /// <summary>
        /// Наличие оборудования на складе
        /// </summary>
        public kEquipment kEquipment
        {
            get
            {
                if ((_kequipment == null) && (Equipment != null))
                    _kequipment = DalContainer.WcfDataManager.kEquipmentList.SingleOrDefault(e => e.Id == this.Equipment.kEquipmentId);

                return _kequipment;
            }
        }

        #endregion

        #region Поля в таблице
        
        /// <summary>
        /// Имя оборудования
        /// </summary>
        public string EquipmentName { get { return this.kEquipment != null ? this.kEquipment.Name : ""; } }

        /// <summary>
        /// Группа оборудования
        /// </summary>
        public string EquipmentGroup { get { return this.kEquipment != null ? this.kEquipment.GroupName : ""; } }

        /// <summary>
        /// Описание оборудования
        /// </summary>
        public string EquipmentDescription { get { return this.Equipment != null ? this.Equipment.Description : ""; } }

        /// <summary>
        /// Задаётся номер, которому соответствует цвет раскраски таблицы
        /// </summary>
        public int ColorForAppearance { get; set; }

        #endregion
    }
}
