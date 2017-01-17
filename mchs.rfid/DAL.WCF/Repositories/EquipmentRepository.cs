// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EquipmentRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Репозиторий оборудования
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.WCF.Repositories
{
    using System;
    using System.Linq;

    using DAL.WCF;
    using DAL.WCF.Repositories.Base;
    using DAL.WCF.ServiceReference;

    using Init.DAL.Sync.GeneralDataPoint;

    /// <summary>
    /// Репозиторий оборудования
    /// </summary>
    public class EquipmentRepository : ClientClassifierRepository<Equipment>
    {
        /// <summary>
        /// Создать репозитой для работы с оборудованием
        /// </summary>
        /// <param name="dataManager">
        /// Менеджер доступа к данным WCF
        /// </param>
        public EquipmentRepository(WcfDataManager dataManager)
            : base(new GeneralDataGateway<Equipment, ServiceOperationClient>(dataManager.GetContext), dataManager)
        {
        }
    }
}
