// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Репозиторий групп оборудования
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
    /// Репозиторий групп оборудования
    /// </summary>
    public class GroupRepository : ClientClassifierRepository<Group>
    {
        /// <summary>
        /// Создать репозитой для работы с группами
        /// </summary>
        /// <param name="dataManager">
        /// Менеджер доступа к данным WCF
        /// </param>
        public GroupRepository(WcfDataManager dataManager)
            : base(new GeneralDataGateway<Group, ServiceOperationClient>(dataManager.GetContext), dataManager)
        {
        }
    }
}
