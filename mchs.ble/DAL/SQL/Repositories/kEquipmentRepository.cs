// --------------------------------------------------------------------------------------------------------------------
// <copyright file="kEquipmentRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Репозиторий оборудования
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Server.Dal;

namespace DAL.SQL.Repositories
{
    using System;

    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;

    using Server.Dal.Sql.DataObjects;

    /// <summary>
    /// Репозиторий оборудования
    /// </summary>
    public class kEquipmentRepository : Repository<KEquipment> 
    {
        /// <summary>
        /// Конструктор репозитория оборудования
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public kEquipmentRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<KEquipment>(dataManager.GetContext), dataManager)         
        {
        }

        /// <summary>
        /// Редактировать оборудование в кэше и в базе
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void EditOverride(KEquipment item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException(@"Не задано название оборудования", "item");

            base.EditOverride(item);
        }
    }
}