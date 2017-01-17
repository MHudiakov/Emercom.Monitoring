// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterMovementModel.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Модель фильтра списка движений
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Models
{
    using System;
    using System.Web.Mvc;
    using System.Linq;

    using DAL.WCF;
    using DAL.WCF.ServiceReference;
    

    /// <summary>
    /// Модель фильтра списка движений
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
        /// Список оборудования
        /// </summary>
        public SelectList EquipmentList
        {
            get
            {
                var equipmentList = DalContainer.WcfDataManager.EquipmentList;
                return new SelectList(equipmentList.OrderBy(e => e.RFId), "Id", "RFId");
            }
        }
    }
}