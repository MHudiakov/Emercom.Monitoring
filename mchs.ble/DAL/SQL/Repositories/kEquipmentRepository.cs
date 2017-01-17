// --------------------------------------------------------------------------------------------------------------------
// <copyright file="kEquipmentRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Репозиторий оборудования
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.SQL.Repositories
{
    using System;

    using DAL.SQL.DataObjects;

    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;

    /// <summary>
    /// Репозиторий оборудования
    /// </summary>
    public class kEquipmentRepository : Repository<kEquipment> 
    {
        /// <summary>
        /// Конструктор репозитория оборудования
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public kEquipmentRepository(DalDataManager dataManager)
            : base(new MsSqlDataAccess<kEquipment>(dataManager.GetContext), dataManager)         
        {
        }

        /// <summary>
        /// Редактировать оборудование в кэше и в базе
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void EditOverride(kEquipment item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException(@"Не задано название оборудования", "item");

            base.EditOverride(item);
        }
    }
}