// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TagRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Репозиторий тегов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Server.Dal;

namespace DAL.SQL.Repositories
{
    using System;
    using System.Linq;

    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;

    using Server.Dal.SQL.DataObjects;

    /// <summary>
    /// Репозиторий тегов
    /// </summary>
    public class TagRepository : Repository<Tag>
    {
        /// <summary>
        /// Конструктор репозитория тегов
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public TagRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<Tag>(dataManager.GetContext), dataManager)
        {
        }
    }
}