// --------------------------------------------------------------------------------------------------------------------
// <copyright file="kObjectRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Репозиторий типов объектов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Server.Dal.SQL.Repositories
{
    using DAL;
    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;
    using Server.Dal.SQL.DataObjects;

    /// <summary>
    /// Репозиторий типов объектов
    /// </summary>
    public class kObjectRepository : Repository<kObject>
    {
        /// <summary>
        /// Конструктор репозитория типов объектов
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public kObjectRepository(DalDataManager dataManager)
            : base(new MsSqlDataAccess<kObject>(dataManager.GetContext), dataManager)
        {
        }
    }
}
