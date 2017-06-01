// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransferPartIdent.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Описывает идентифицирующую информацию части пересылаемых данных
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.Transfer
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Описывает идентифицирующую информацию части пересылаемых данных
    /// </summary>
    [DataContract]
    public class TransferPartIdent
    {
        /// <summary>
        /// Номер части
        /// </summary>
        [DataMember]
        public int PartNumber { get; private set; }

        /// <summary>
        /// Идентификатор ресурса
        /// </summary>
        [DataMember]
        public int ResourceId { get; private set; }
        
        /// <summary>
        /// Идентифицирующая информация части пересылаемых данных
        /// </summary>
        /// <param name="partNumber">Номер части</param>
        /// <param name="resourceId">Идентификатор ресурса</param>
        public TransferPartIdent(int partNumber, int resourceId)
        {
            if (partNumber < 0)
                throw new ArgumentException("Номер части должен быть положительным числом", "partNumber");
            if (resourceId < 0)
                throw new ArgumentException("Идентфикатор ресурса должен быть положительным числом", "resourceId");

            PartNumber = partNumber;
            ResourceId = resourceId;
        }
    }
}
