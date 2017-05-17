using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Dal.SQL.Repositories
{
    using System.Data;
    using System.Data.SqlClient;

    using DAL;

    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;
    using Init.Tools;

    using Server.Dal.SQL.DataObjects;

    /// <summary>
    /// Репозиторий комплектации машин
    /// </summary>
   public class TripComplectationRepository : Repository<TripComplectation>
    {
        /// <summary>
        /// Конструктор репозитория комплектации машин
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public TripComplectationRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<TripComplectation>(dataManager.GetContext), dataManager)
        {
        }
    }
}
