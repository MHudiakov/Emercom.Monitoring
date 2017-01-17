// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Стратегия для GridHelperWPF при наличии шлюза IRepository
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Стратегия для GridHelperWPF при наличии шлюза IRepository
    /// </summary>
    /// <typeparam name="T">Тип объекта шлюза</typeparam>
    public class RepositoryStrategy<T> : GhStrategy<T>
        where T : class, new()
    {
        /// <summary>
        /// Операция по созданию нового объекта
        /// </summary>
        private readonly Func<T> _createNewItem;

        /// <summary>
        /// Метод получения списка объектов
        /// </summary>
        private readonly Func<List<T>> _getItems;

        /// <summary>
        /// Шлюз таблицы
        /// </summary>
        private readonly IRepository<T> _repository;

        /// <summary>
        /// Конструктор для работы с репозиторием
        /// </summary>
        /// <param name="repository">Шлюз таблицы</param>
        /// <param name="createItem">Операция по созданию нового объекта</param>
        /// <param name="getList">Метод получения списка объектов</param>
        public RepositoryStrategy(IRepository<T> repository, Func<T> createItem, Func<List<T>> getList)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");
            if (createItem == null)
                throw new ArgumentNullException("createItem");
            if (getList == null)
                throw new ArgumentNullException("getList");

            this._createNewItem = createItem;
            this._getItems = getList;
            this._repository = repository;
        }

        /// <summary>
        /// Конструктор для работы с репозиторием (по умолчанию)
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        public RepositoryStrategy(IRepository<T> repository)
            : this(repository, () => new T(), () => repository.List)
        {
        }

        /// <summary>
        /// Операция добавления в репозиторий
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public override void Add(T item)
        {
            this._repository.Add(item);
        }

        /// <summary>
        /// Операция изменения объекта в репозитории
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public override void Edit(T item)
        {
            this._repository.Edit(item);
        }

        /// <summary>
        /// Операция удаления из репозитория
        /// </summary>
        /// <param name="item">TEntity</param>
        public override void Delete(T item)
        {
            this._repository.Delete(item);
        }

        /// <summary>
        /// Получение списка всех объектов репозитория
        /// </summary>
        /// <returns>Список объектов TEntity</returns>
        public override List<T> GetList()
        {
            return this._getItems();
        }

        /// <summary>
        /// Создание нового объекта репозитория
        /// </summary>
        /// <returns>Новый объект TEntity</returns>
        public override T CreateItem()
        {
            return this._createNewItem();
        }
    }
}
