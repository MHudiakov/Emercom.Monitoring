// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Репозиторий групп оборудования
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.SQL.Repositories
{
    using System;

    using Init.DbCore.DB.MsSql;

    using Init.DbCore.Repository;

    using Server.Dal.Sql.DataObjects;

    /// <summary>
    /// Репозиторий групп оборудования
    /// </summary>
    public class GroupRepository : Repository<EquipmentGroup>
    {
        /// <summary>
        /// Конструктор репозитория Групп оборудования
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public GroupRepository(DalDataManager dataManager)
            : base(new MsSqlDataAccess<EquipmentGroup>(dataManager.GetContext), dataManager)
        {
        }

        /// <summary>
        /// Редактировать группу оборудования в кэше и в базе
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void EditOverride(EquipmentGroup item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException(@"Не задано название группы оборудования", "item");

            base.EditOverride(item);
        }
    }
}