// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CashedRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Кешируюущий репозиторий. ищет данные в кеше, если их нет, то в БД
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Init.DbCore.DataAccess;
    using Init.Tools;

    /// <summary>
    /// Кешируюущий репозиторий. ищет данные в кеше, если их нет, то в БД
    /// </summary>
    /// <typeparam name="T">
    /// Тип объекта, хранящийся в репозитории
    /// </typeparam>
    public class CashedRepository<T> : Repository<T>
        where T : DbObject
    {
        /// <summary>
        /// Кешируюущий репозиторий. ищет данные в кеше, если их нет, то в БД
        /// </summary>
        /// <param name="dataAccess">Объект доступа к данным</param>
        /// <param name="dataManager">DataManager</param>
        public CashedRepository(DataAccess<T> dataAccess, DataManager dataManager)
            : base(dataAccess, dataManager)
        {
            this._cache = new Cashe<T>();
        }

        /// <summary>
        /// Объект операций с кэшем
        /// </summary>
        protected virtual Cashe<T> Cache
        {
            get
            {
                return this._cache;
            }
        }

        /// <summary>
        /// Объект операций с кэшем
        /// </summary>
        private readonly Cashe<T> _cache;

        /// <summary>
        /// Получение количества объектов
        /// </summary>
        /// <returns>
        /// Количество объектов в БД
        /// </returns>
        public override long GetCount()
        {
            if (this.Cache.Count == 0)
                return base.GetCount();
            return this.Cache.Count;
        }

        /// <summary>
        /// Добавление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void AddOverride(T item)
        {
            if (this.Cache.Where(e => e.Equals(item)).Any())
                throw new ArgumentException(string.Format("Нельзя добавить экземпляр объекта {0} т.к. он уже добавлен.", typeof(T).FullName), "item").AddData("item", item);

            // пишем в базу
            base.AddOverride(item);

            // добавляем в кеш
            this.Cache.UpdateOrInsert(item, (s, d) => s.CopyTo(d));

            try
            {
                this.UpdateCacheOnAdd(item);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Cинхронизации кэша при добавлении обекта [{0}]", typeof(T).FullName), ex).AddData("item", item);
            }
        }

        /// <summary>
        /// Выполняет дополнительные операции синхронизации кеша при добавлении
        /// </summary>
        /// <param name="item">
        /// Добавленый элемент
        /// </param>
        protected virtual void UpdateCacheOnAdd(T item)
        {
        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="whereArgs">
        /// Ключи объекта
        /// </param>
        protected override void DeleteWhereOverride(Dictionary<string, object> whereArgs)
        {
            var cachedItems = this.Cache.Where(e => e.PropEqualas(whereArgs)).ToList();

            // удаляем из базы
            base.DeleteWhereOverride(whereArgs);

            // чистим кеш
            this.Cache.DeleteWhere(e => e.PropEqualas(whereArgs));

            foreach (var item in cachedItems)
                this.UpdateCacheOnDelete(item);
        }

        /// <summary>
        /// Выполняет дополнительные операции синхронизации кэша при удалении
        /// </summary>
        /// <param name="item">Удаляемый объект</param>
        protected virtual void UpdateCacheOnDelete(T item)
        {
        }

        /// <summary>
        /// Редактировать объект в кэше и в базе
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void EditOverride(T item)
        {
            T oldItem = this.Cache.Where(e => e.Equals(item)).SingleOrDefault();

            if (oldItem == null || ReferenceEquals(oldItem, item))
                oldItem = this.DataAccess.Get(item.KeyValue);

            if (oldItem == null)
                throw new ArgumentException(string.Format("Невозможно отредактировать объект [{0}] т.к. нет данных об объекте в базе.", typeof(T).FullName), "item").AddData("item", item);

            // редактируем в базе
            base.EditOverride(item);

            // редактируем в кэше
            this.Cache.UpdateOrInsert(item, (s, d) => s.CopyTo(d));
            try
            {
                this.UpdateCacheOnEdit(item, oldItem);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ошибка синхронизации кэша при редактировании обекта [{0}]", typeof(T).FullName), ex).AddData("item", item);
            }
        }

        /// <summary>
        /// Выполняет дополнительные операции синхронизации кэша при редактировании
        /// </summary>
        /// <param name="item">Отредактированный объект</param>
        /// <param name="oldItem">Редактируемый объект </param>
        protected virtual void UpdateCacheOnEdit(T item, T oldItem)
        {
        }

        /// <summary>
        /// Получение сиска объектов по ключу
        /// </summary>
        /// <param name="whereArgs">
        /// Ключ отбора объектов
        /// </param>
        /// <returns>
        /// Список объектов подходящий под критерий
        /// </returns>
        protected override List<T> GetItemsWhereOverride(Dictionary<string, object> whereArgs)
        {
            // Проверяем кеш
            var cashedItems = this.Cache.Where(e =>
                {
                    var itemFields = e.GetProperties();
                    return whereArgs.All(k =>
                        {
                            // если поля вообще нет, то объект не подходит 
                            if (!itemFields.ContainsKey(k.Key))
                                return false;

                            // если поле есть и задано, проверяем по значению
                            var field = itemFields[k.Key];
                            if (field != null)
                                return itemFields[k.Key].Equals(k.Value);

                            // если поле не задано, то и ключ должен быть не задан
                            return k.Value == null;
                        });
                });

            if (cashedItems.Any())
                return cashedItems;

            // если в кэше данных нет то мщем обект в базе
            cashedItems = base.GetItemsWhereOverride(whereArgs);

            if (cashedItems.Any())
                cashedItems = cashedItems.Select(item => this.Cache.UpdateOrInsert(item, (s, d) => s.CopyTo(d))).ToList();

            return cashedItems;
        }

        /// <summary>
        /// Получение части объектов
        /// </summary>
        /// <param name="pageIndex">
        /// Номер страницы
        /// </param>
        /// <param name="pageSize">
        /// Количество объектов
        /// </param>
        /// <returns>
        /// Список объектов начиная с idFrom длинной не более pageSize
        /// </returns>
        protected override List<T> GetPageOverride(int pageIndex, int pageSize)
        {
            var cachedItems = this.GetAll();

            // todo: держать кеш отсортированным
            cachedItems.Sort(
                     (left, right) =>
                     {
                         var propertyInfo = this.Metadata.Key;
                         var leftValue = (IComparable)propertyInfo.Getter(left);
                         var rightValue = (IComparable)propertyInfo.Getter(right);
                         return leftValue.CompareTo(rightValue);
                     });
            var catcedItems = cachedItems.Skip(pageSize * pageIndex).Take(pageSize).ToList();
            if (!catcedItems.Any())
                catcedItems = base.GetPageOverride(pageIndex, pageSize).Select(e => this.Cache.UpdateOrInsert(e, (s, d) => s.CopyTo(d))).ToList();

            return catcedItems;
        }

        /// <summary>
        /// Получение всех объектов
        /// </summary>
        /// <returns>
        /// Список всех объектов в таблице
        /// </returns>
        public override List<T> GetAll()
        {
            var list = this.Cache.Where(e => true);
            if (list.Count == 0)
                list = base.GetAll().Select(arg => this.Cache.UpdateOrInsert(arg, (s, d) => s.CopyTo(d))).ToList();
            return list;
        }
    }
}