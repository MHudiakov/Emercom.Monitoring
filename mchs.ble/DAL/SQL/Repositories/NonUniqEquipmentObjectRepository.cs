// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NonUniqEquipmentObjectRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Репозиторий не уникального оборудования для машины
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Server.Dal.SQL.Repositories
{
    using DAL;
    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;
    using Server.Dal.SQL.DataObjects;

    /// <summary>
    /// Репозиторий не уникального оборудования для машины
    /// </summary>
    public class NonUniqEquipmentObjectRepository : Repository<NonUniqEquipmentObject>
    {
        /// <summary>
        /// Конструктор репозитория не уникального оборудования для машины
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public NonUniqEquipmentObjectRepository(DalDataManager dataManager)
            : base(new MsSqlDataAccess<NonUniqEquipmentObject>(dataManager.GetContext), dataManager)
        {
        }
    }
}
