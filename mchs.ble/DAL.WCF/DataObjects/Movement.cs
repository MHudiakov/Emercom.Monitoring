namespace DAL.WCF.ServiceReference
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using DAL.WCF;

    using Init.DbCore.Metadata;

    /// <summary>
    /// Движение по складу
    /// </summary>
    public partial class Movement
    {
        /// <summary>
        /// Определяем метод преобразования к строке для всех объектов БД
        /// </summary>
        /// <returns>
        /// Название движения оборудования по складу
        /// </returns>
        public override string ToString()
        {
            return this.Id.ToString();
        }

        #region Навигационные свойства


        private Unit unit = null;
        /// <summary>
        /// Наличие оборудования на складе
        /// </summary>
        public Unit Unit
        {
            get
            {
                if (unit == null)
                    unit = DalContainer.WcfDataManager.UnitList.SingleOrDefault(e => e.Id == this.UnitId);

                return unit;
            }
        }

        public string UnitName { get { return this.Unit != null ? this.Unit.Name : ""; } }

        private Equipment equipment = null;
        /// <summary>
        /// Наличие оборудования на складе
        /// </summary>
        public Equipment Equipment 
        { 
            get
            {
                if (equipment == null)
                    equipment = DalContainer.WcfDataManager.EquipmentList.SingleOrDefault(e => e.Id == this.EquipmentId);
                    
                return equipment;
            }
        }


        private kEquipment kequipment = null;
        /// <summary>
        /// Оборудование
        /// </summary>
        public kEquipment kEquipment
        {
            get
            {
                if ((kequipment == null) && (kEquipmentId != 0))
                    kequipment = DalContainer.WcfDataManager.kEquipmentList.SingleOrDefault(e => e.Id == this.kEquipmentId);

                return kequipment;
            }
        }

        public string EquipmentName { get { return this.Equipment != null ? this.Equipment.EquipmentName : ""; } }

        public int kEquipmentId { get { return this.Equipment != null ? this.Equipment.kEquipmentId : 0; } }

        public int ArrivedInt { get { return this.IsArrived ? 1 : 0; } }

        private string arrived = null;

        /// <summary>
        /// Прибыло/Убыло
        /// </summary>
        public string Arrived 
        {
            get
            {
                if (arrived == null)
                    this.arrived = this.IsArrived ? "Прибыло" : "Убыло";
                
                return arrived;
            }
        }

        #endregion
    }
}
