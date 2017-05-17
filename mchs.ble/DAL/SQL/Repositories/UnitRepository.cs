// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Репозиторий объектов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Server.Dal.SQL.Repositories
{
    using DAL;

    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;

    using Server.Dal.SQL.DataObjects;

    /// <summary>
    /// Репозиторий объектов
    /// </summary>
    public class UnitRepository : Repository<Unit>
    {
        /// <summary>
        /// Конструктор репозитория объектов
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public UnitRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<Unit>(dataManager.GetContext), dataManager)
        {
        }
    }
}
