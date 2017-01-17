// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientClassifierRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// // <summary>
//   Перечень разрешений для потоков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.WCF.Repositories.Base
{
    using Init.DAL.Sync.Strategy;
    using Init.DbCore;
    using Init.DbCore.DataAccess;
    using Init.DbCore.Repository;

    /// <summary>
    /// Репозиторий-классификатор со стандартной стратегией
    /// </summary>
    /// <typeparam name="T">
    /// Тип шлюза записи
    /// </typeparam>
    public class ClientClassifierRepository<T> : ClassifierRepository<T>
        where T : DbObject
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClientClassifierRepository{T}"/>. 
        /// Репозиторий-справочник c расширенными функциями доступа к БД
        /// </summary>
        /// <param name="dataAccess">
        /// Объект доступа к данным
        /// </param>
        /// <param name="dataManager">
        /// Менеджер данных
        /// </param>
        public ClientClassifierRepository(DataAccess<T> dataAccess, DataManager dataManager)
            : base(dataAccess, dataManager)
        {
            this.SyncStrategy = new DefaultStrategy<T>(
                this.UpdateCacheOnAdd,
                this.UpdateCacheOnEdit,
                this.UpdateCacheOnDelete,
                this.Cache);
        }

        /// <summary>
        /// Стратегия синхронизации кэша
        /// </summary>
        public SyncStrategy SyncStrategy { get; protected set; }
    }
}