// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Стратегия таблицы для работы с коллекцией объектов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.DbCoreExtension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Init.Tools.DevExpress;

    /// <summary>
    /// Стратегия таблицы для работы с коллекцией объектов
    /// </summary>
    /// <typeparam name="T">Тип поллекции</typeparam>
    public class CollectionStrategy<T> : GhStrategy<T>
        where T : class, new()
    {
        /// <summary>
        /// Коллекция-источник данных
        /// </summary>
        private readonly List<T> _collection;

        /// <summary>
        /// Фабричный метод создания объекта
        /// </summary>
        private readonly Func<T> _createItem;

        /// <summary>
        /// Стратегия таблицы для работы с коллекцией объектов
        /// </summary>
        /// <param name="collection">Коллекция-источник данных</param>
        public CollectionStrategy(List<T> collection)
            : this(collection, () => new T())
        {
        }

        /// <summary>
        /// Стратегия таблицы для работы с коллекцией объектов
        /// </summary>
        /// <param name="collection">Коллекция-источник данных</param>
        /// <param name="createItem">Фабричный метод создания объекта</param>
        public CollectionStrategy(List<T> collection, Func<T> createItem)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (createItem == null)
                throw new ArgumentNullException("createItem");
            this._collection = collection;
            this._createItem = createItem;
        }

        /// <summary>
        /// Операция добавления объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public override void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!this._collection.Exists(item.Equals))
                this._collection.Add(item);
        }

        /// <summary>
        /// Операция изменения объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public override void Edit(T item)
        {
            // предполагается ссылочная целостность
        }

        /// <summary>
        /// Операция удаления объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public override void Delete(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            this._collection.RemoveAll(item.Equals);
        }

        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <returns>Список объектов TEntity</returns>
        public override List<T> GetList()
        {
            return this._collection.ToList();
        }

        /// <summary>
        /// Создание нового объекта
        /// </summary>
        /// <returns>Объект TEntity</returns>
        public override T CreateItem()
        {
            return this._createItem();
        }
    }
}