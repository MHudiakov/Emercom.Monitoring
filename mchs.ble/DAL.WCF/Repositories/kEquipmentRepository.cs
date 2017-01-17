// --------------------------------------------------------------------------------------------------------------------
// <copyright file="kEquipmentRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Репозиторий классов оборудования
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.WCF.Repositories
{
    using System;
    using System.Linq;

    using DAL.WCF.Repositories.Base;
    using DAL.WCF.ServiceReference;

    using Init.DAL.Sync.GeneralDataPoint;

    /// <summary>
    /// Репозиторий классов оборудования
    /// </summary>
    public class kEquipmentRepository : ClientClassifierRepository<kEquipment>
    {
        /// <summary>
        /// Создать репозитой для работы с классами оборудования
        /// </summary>
        /// <param name="dataManager">
        /// Менеджер доступа к данным WCF
        /// </param>
        public kEquipmentRepository(WcfDataManager dataManager)
            : base(new GeneralDataGateway<kEquipment, ServiceOperationClient>(dataManager.GetContext), dataManager)
        {
        }
    }
}
