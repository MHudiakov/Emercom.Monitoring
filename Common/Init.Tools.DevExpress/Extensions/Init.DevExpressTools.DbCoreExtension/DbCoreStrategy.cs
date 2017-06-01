// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbCoreStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Стратегия GridHelper для рпозиториев DbCore
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.DbCoreExtension
{
    using System;
    using System.Collections.Generic;

    using Init.DbCore;
    using Init.DbCore.DataAccess;
    using Init.Tools.DevExpress;

    /// <summary>
    /// Стратегия GridHelper для рпозиториев DbCore
    /// </summary>
    /// <typeparam name="T">
    /// Тип объкта стратегии
    /// </typeparam>
    public class DbCoreStrategy<T> : GhStrategy<T>
        where T : DbObject, new()
    {
        /// <summary>
        /// Шлюз доступа к данным
        /// </summary>
        private readonly DataAccess<T> _dataAccess;

        /// <summary>
        /// Фабричный метод создания объекта
        /// </summary>
        private readonly Func<T> _createItem;

        /// <summary>
        /// Метод получения списка объектов
        /// </summary>
        private readonly Func<List<T>> _getList;

        /// <summary>
        /// Стратегия GridHelper для рпозиториев DbCore
        /// </summary>
        /// <param name="dataAccess">Шлюз доступа к данным</param>
        public DbCoreStrategy(DataAccess<T> dataAccess)
            : this(dataAccess, () => new T(), dataAccess.GetAll)
        {
        }

        /// <summary>
        /// Стратегия GridHelper для рпозиториев DbCore
        /// </summary>
        /// <param name="dataAccess">Шлюз доступа к данным</param>
        /// <param name="createItem">Фабричный метод создания объекта</param>
        /// <param name="getList">Метод получения списка объектов</param>
        public DbCoreStrategy(DataAccess<T> dataAccess, Func<T> createItem, Func<List<T>> getList)
        {
            if (dataAccess == null)
                throw new ArgumentNullException("dataAccess");
            if (createItem == null)
                throw new ArgumentNullException("createItem");
            if (getList == null)
                throw new ArgumentNullException("getList");

            this._dataAccess = dataAccess;
            this._createItem = createItem;
            this._getList = getList;
        }

        /// <summary>
        /// Операция добавления объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public override void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._dataAccess.Add(item);
        }

        /// <summary>
        /// Операция изменения объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public override void Edit(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._dataAccess.Edit(item);
        }

        /// <summary>
        /// Операция удаления объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public override void Delete(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._dataAccess.Delete(item);
        }

        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <returns>Список объектов TEntity</returns>
        public override List<T> GetList()
        {
            return this._getList();
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
