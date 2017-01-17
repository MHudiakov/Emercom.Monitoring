// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectARepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Репозиторий ObjectA
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.Test.Mocks
{
    using Init.DbCore;
    using Init.DbCore.Repository;

    

    using Init.DAL.Sync.Strategy;
    using Init.DAL.Sync.Test.DataObjects;

    /// <summary>
    /// Репозиторий ObjectA
    /// </summary>
    internal class ObjectARepository : CashedRepository<ObjectA>
    {
        /// <summary>
        /// Репозиторий ObjectA
        /// </summary>
        /// <param name="dataManager">Менеджер данных</param>
        public ObjectARepository(MockDataManager dataManager)
            : base(new MockDataAccess<ObjectA>(), dataManager)
        {
            Strategy = new DefaultStrategy<ObjectA>(this.UpdateCacheOnAdd, this.UpdateCacheOnEdit, this.UpdateCacheOnDelete, this.Cache);
        }

        /// <summary>
        /// Стратегия синхронизации
        /// </summary>
        public SyncStrategy Strategy { get; private set; }

        /// <summary>
        /// Объект операций с кешем
        /// </summary>
        public new Cashe<ObjectA> Cache
        {
            get
            {
                return base.Cache;
            }
        }
    }
}