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

    using DAL.WCF.Repositories;
    using DAL.WCF.ServiceReference;

    using Init.DAL.Sync.GeneralDataPoint;
using System.Collections.Generic;

    /// <summary>
    /// Репозиторий групп оборудования
    /// </summary>
    public class GroupRepository
    {
        private List<Group> _list;

        private DateTime _lastUpdateDate;

        public List<Group> List()
        {
            if (_list == null || (DateTime.Now - _lastUpdateDate).Seconds > 120)
            {
                _list = DalContainer.WcfDataManager.ServiceOperationClient.GetAllGroup();
                _lastUpdateDate = DateTime.Now;
                return _list;
            }

            return _list;
        }
    }
}
