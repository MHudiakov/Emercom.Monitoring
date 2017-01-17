// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterStorageModel.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Модель фильтра списка оборудования на складе
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
    /// Модель фильтра списка оборудования на складе
    /// </summary>
    public class FilterEquipmentModel
    {
        /// <summary>
        /// Id Оборудования
        /// </summary>
        public int? kEquipmentId { get; set; }

        /// <summary>
        /// Список оборудования
        /// </summary>
        public SelectList kEquipmentList
        {
            get
            {
                var kEquipmentList = DalContainer.WcfDataManager.kEquipmentList;
                return new SelectList(kEquipmentList.OrderBy(e => e.Name), "Id", "Name");
            }
        }
    }
}