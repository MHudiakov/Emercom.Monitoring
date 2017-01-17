// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectBRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Репозиторий ObjectB
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.Test.Mocks
{
    using System;

    using Init.DbCore;
    using Init.DbCore.DataAccess;
    using Init.DbCore.Repository;

    

    using Init.DAL.Sync.Strategy;
    using Init.DAL.Sync.Test.DataObjects;

    /// <summary>
    /// Репозиторий ObjectB
    /// </summary>
    internal class ObjectBRepository : CashedRepository<ObjectB>
    {
        /// <summary>
        /// Репозиторий ObjectB
        /// </summary>
        /// <param name="dataManager">Менеджер данных</param>
        public ObjectBRepository(MockDataManager dataManager)
            : base(new MockDataAccess<ObjectB>(), dataManager)
        {
            Strategy = new DefaultStrategy<ObjectB>(this.UpdateCacheOnAdd, this.UpdateCacheOnEdit, this.UpdateCacheOnDelete, this.Cache);
        }

        /// <summary>
        /// Стратегия синхронизации
        /// </summary>
        public SyncStrategy Strategy { get; private set; }

        /// <summary>
        /// Внутренний кеш шлюза
        /// </summary>
        public new Cashe<ObjectB> Cache
        {
            get
            {
                return base.Cache;
            }
        }

        /// <summary>
        /// Шлюз таблицы данных
        /// </summary>
        public new DataAccess<ObjectB> DataAccess
        {
            get
            {
                return base.DataAccess;
            }
        }

        /// <summary>
        /// Выполняет дополнительные операции синхронизации кеша при добавлении
        /// </summary>
        /// <param name="item">
        /// Добавленый элемент
        /// </param>
        protected override void UpdateCacheOnAdd(ObjectB item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            item.ObjectA.ObjectBList.Add(item);

            base.UpdateCacheOnAdd(item);
        }

        /// <summary>
        /// Выполняет дополнительные операции синхронизации кэша при редактировании
        /// </summary>
        /// <param name="source">Отредактированный объект</param>
        /// <param name="dest">Редактируемый объект </param>
        protected override void UpdateCacheOnEdit(ObjectB source, ObjectB dest)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (dest == null)
                throw new ArgumentNullException("dest");

            if (source.ObjectAId != dest.ObjectAId)
            {
                dest.ObjectA.ObjectBList.RemoveAll(e => e.Id == dest.Id);
                if (!source.ObjectA.ObjectBList.Exists(objB => objB.Id == source.Id))
                    source.ObjectA.ObjectBList.Add(source);
            }

            base.UpdateCacheOnEdit(source, dest);
        }

        /// <summary>
        /// Выполняет дополнительные операции синхронизации кэша при удалении
        /// </summary>
        /// <param name="item">Удаляемый объект</param>
        protected override void UpdateCacheOnDelete(ObjectB item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            item.ObjectA.ObjectBList.RemoveAll(e => e.Id == item.Id);

            base.UpdateCacheOnDelete(item);
        }
    }
}