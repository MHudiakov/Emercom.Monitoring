// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SafeTransferChangeList.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Список гарантировнной доставки изменений клиенту
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Список гарантировнной доставки изменений клиенту
    /// </summary>
    public class SafeTransferChangeList
    {
        /// <summary>
        /// Дата на момент которой были собраны изменения
        /// </summary>
        public DateTime LastActivityDate { get; set; }

        /// <summary>
        /// Целостный список изменений
        /// </summary>
        public List<Change> ChangeList { get; private set; }

        /// <summary>
        /// Создать список гарантированной доставки
        /// </summary>
        public SafeTransferChangeList()
        {
            this.LastActivityDate = DateTime.Now;
            this.ChangeList = new List<Change>();
        }
    }
}
