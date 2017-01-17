// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GHStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Стратегия выполнения операций для GridHelper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System.Collections.Generic;

    /// <summary>
    /// Стратегия выполнения операций для GridHelper
    /// </summary>
    /// <typeparam name="T">Переменная типа Generic с публичным конструктором</typeparam>
    public abstract class GhStrategy<T>
        where T : class
    {
        /// <summary>
        /// Операция добавления объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public abstract void Add(T item);

        /// <summary>
        /// Операция изменения объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public abstract void Edit(T item);

        /// <summary>
        /// Операция удаления объекта
        /// </summary>
        /// <param name="item">Объект типа TEntity</param>
        public abstract void Delete(T item);

        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <returns>Список объектов TEntity</returns>
        public abstract List<T> GetList();

        /// <summary>
        /// Создание нового объекта
        /// </summary>
        /// <returns>Объект TEntity</returns>
        public abstract T CreateItem();
    }
}
