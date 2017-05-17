﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="kEquipmentRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Репозиторий оборудования
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Init.DbCore.DB.MsSql;
using Init.DbCore.Repository;
using Server.Dal.Sql.DataObjects;

namespace Server.Dal.Sql.Repositories
{
    /// <summary>
    /// Репозиторий классификаторов оборудования
    /// </summary>
    public class KEquipmentRepository : Repository<KEquipment> 
    {
        public KEquipmentRepository(DataManager dataManager)
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
                throw new ArgumentException(@"Не задано название оборудования", nameof(item));

            base.EditOverride(item);
        }
    }
}