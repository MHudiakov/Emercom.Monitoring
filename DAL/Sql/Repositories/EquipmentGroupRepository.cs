using System;
using Init.DbCore.DB.MsSql;
using Init.DbCore.Repository;
using Server.Dal.Sql.DataObjects;

namespace Server.Dal.Sql.Repositories
{
    /// <summary>
    /// Репозиторий групп оборудования
    /// </summary>
    public sealed class EquipmentGroupRepository : Repository<EquipmentGroup>
    {
        /// <summary>
        /// Конструктор репозитория Групп оборудования
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public EquipmentGroupRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<EquipmentGroup>(dataManager.GetContext), dataManager)
        {}

        /// <summary>
        /// Редактировать группу оборудования в кэше и в базе
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void EditOverride(EquipmentGroup item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException(@"Не задано название группы оборудования", nameof(item));

            base.EditOverride(item);
        }
    }
}