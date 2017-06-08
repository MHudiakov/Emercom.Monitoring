using System.ComponentModel.DataAnnotations;

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
        public FilterMovementModel(int unitId)
        {
            var equipmentList = DalContainer.WcfDataManager.ServiceOperationClient.GetEquipmentListForUnit(unitId);
            this.EquipmentList = new SelectList(equipmentList, "Id", "Tag");
            this.UnitId = unitId;
        }

        public FilterMovementModel()
        {
        }

        /// <summary>
        /// Дата начала
        /// </summary>
        [Required(ErrorMessage = "Укажите значение поля")]
        public DateTime? DtBegin { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        [Required(ErrorMessage = "Укажите значение поля")]
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
        public SelectList EquipmentList { get; set; }
    }
}