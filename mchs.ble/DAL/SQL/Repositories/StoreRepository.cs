// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StoreRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Репозиторий баз (складов)
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace Server.Dal.SQL.Repositories
{
    using DAL;

    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;

    using Server.Dal.SQL.DataObjects;

    /// <summary>
    /// Репозиторий баз (складов)
    /// </summary>
    public class StoreRepository : Repository<Store>
    {
        /// <summary>
        /// Конструктор репозитория баз
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public StoreRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<Store>(dataManager.GetContext), dataManager)
        {
        }

        /// <summary>
        /// Получить базу объекта
        /// </summary>
        /// <returns>
        /// The <see cref="Store"/>.
        /// </returns>
        public Store GetStore()
        {
            return this.GetAll().FirstOrDefault();
        }
    }
}