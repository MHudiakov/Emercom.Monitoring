// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransferPart.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Часть пересылаемых данных
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.Transfer
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Часть пересылаемых данных
    /// </summary>
    [DataContract]
    public class TransferPart
    {
        /// <summary>
        /// Список пересылаемых объектов
        /// </summary>
        [DataMember]
        public List<object> Items { get; private set; }

        /// <summary>
        /// Идентифицирующая информация
        /// </summary>
        [DataMember]
        public TransferPartIdent TransferPartIdent { get; private set; }

        /// <summary>
        /// Флаг: true, если это последняя часть
        /// </summary>
        [DataMember]
        public bool IsLast { get; internal set; }

        /// <summary>
        /// Часть пересылаемых данных
        /// </summary>
        /// <param name="transferPartIdent">Идентифицирующая информация</param>
        /// <param name="items">Список пересылаемых объектов</param>
        public TransferPart(TransferPartIdent transferPartIdent, List<object> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            if (transferPartIdent == null)
                throw new ArgumentNullException("transferPartIdent");

            this.TransferPartIdent = transferPartIdent;
            Items = items;
        }
    }
}
