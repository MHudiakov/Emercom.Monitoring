// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlainStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Открытая стартегия грид хелпера предоставляет возможност самостоятельно реализовать методы работы с базой
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Открытая стартегия грид хелпера предоставляет возможност самостоятельно реализовать методы работы с базой
    /// </summary>
    /// <typeparam name="TEntity">Тип объекта репозитория</typeparam>
    public class PlainStrategy<TEntity> : GhStrategy<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Метод созданя нового элемента
        /// </summary>
        private readonly Func<TEntity> _createNewItem;

        /// <summary>
        /// Метод получения списка элементов
        /// </summary>
        private readonly Func<List<TEntity>> _getList;

        /// <summary>
        /// Метод добавления в БД
        /// </summary>
        private readonly Action<TEntity> _add;

        /// <summary>
        /// Метод редактирования в БД
        /// </summary>
        private readonly Action<TEntity> _edit;

        /// <summary>
        /// Метод удаления в БД
        /// </summary>
        private readonly Action<TEntity> _delete;

        /// <summary>
        /// Конструктор для работы GridHelper с коллекцией объектов без репозитория
        /// </summary>
        /// <param name="add">Метод добавления объекта</param>
        /// <param name="edit">Метод редактирования объекта</param>
        /// <param name="delete">Метод удаления объекта</param>
        /// <param name="createItem">Метод создания нового объекта TEntity</param>
        /// <param name="getList">Получение списка объектов</param>
        public PlainStrategy(Action<TEntity> add, Action<TEntity> edit, Action<TEntity> delete, Func<TEntity> createItem, Func<List<TEntity>> getList)
        {
            if (createItem == null)
                throw new ArgumentNullException("createItem");
            if (getList == null)
                throw new ArgumentNullException("getList");
            if (add == null)
                throw new ArgumentNullException("add");
            if (edit == null)
                throw new ArgumentNullException("edit");
            if (delete == null)
                throw new ArgumentNullException("delete");
            this._createNewItem = createItem;
            this._getList = getList;
            this._add = add;
            this._edit = edit;
            this._delete = delete;
        }

        /// <summary>
        /// Метод добавления объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public override void Add(TEntity item)
        {
            this._add(item);
        }

        /// <summary>
        /// Метод редактирования объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public override void Edit(TEntity item)
        {
            this._edit(item);
        }

        /// <summary>
        /// Метод удаления объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public override void Delete(TEntity item)
        {
            this._delete(item);
        }

        /// <summary>
        /// Метод создания нового объекта
        /// </summary>
        /// <returns>Объект типа TEntity</returns>
        public override TEntity CreateItem()
        {
            return this._createNewItem();
        }

        /// <summary>
        /// Метод получения списка объектов
        /// </summary>
        /// <returns>Список объектов типа TEntity</returns>
        public override List<TEntity> GetList()
        {
            return this._getList();
        }
    }
}
