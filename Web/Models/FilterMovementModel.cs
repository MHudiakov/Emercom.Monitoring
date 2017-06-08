namespace Web.Models
{
    using System;
    using System.Web.Mvc;
    using DAL.WCF;

    /// <summary>
    /// Модель фильтра списка движений оборудования
    /// </summary>
    public class FilterMovementModel
    {
        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? DtBegin { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime? DtEnd { get; set; }

        /// <summary>
        /// Id Оборудования
        /// </summary>
        public int? EquipmentId { get; set; }

        /// <summary>
        /// Id юнита
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// Список оборудования
        /// </summary>
        public SelectList EquipmentList
        {
            get
            {
                // todo в списке должно быть только оборудование, которое есть в формуляре ПТВ для отображаемого юнита
                var equipmentList = DalContainer.WcfDataManager.EquipmentList;
                return new SelectList(equipmentList, "Id", "Tag");
            }
        }
    }
}