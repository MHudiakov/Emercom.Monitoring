// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovementRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Репозиторий движений оборудования
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
    /// Репозиторий движений оборудования
    /// </summary>
    public class MovementRepository : ClientClassifierRepository<Movement>
    {
        /// <summary>
        /// Создать репозитой для работы с движениями
        /// </summary>
        /// <param name="dataManager">
        /// Менеджер доступа к данным WCF
        /// </param>
        public MovementRepository(WcfDataManager dataManager)
            : base(new GeneralDataGateway<Movement, ServiceOperationClient>(dataManager.GetContext), dataManager)
        {
        }
    }
}
