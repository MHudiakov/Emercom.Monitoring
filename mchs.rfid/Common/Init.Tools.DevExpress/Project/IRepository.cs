// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Представляет методы для работы с репозиториями
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System.Collections.Generic;

    /// <summary>
    /// Хранит список методов, подлежащих вызову при возникновении события OnChangeList
    /// </summary>
    /// <param name="list">
    /// Список объектов
    /// </param>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public delegate void OnChangeListDeleagte<TEntity>(List<TEntity> list)
            where TEntity : class, new();

    /// <summary>
    /// Представляет методы для работы с репозиториями
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public interface IRepository<TEntity>
            where TEntity : class, new()
    {
        /// <summary>
        /// Событие, возникающее при изменении списка оотображаемых элементов репозитория
        /// </summary>
        event OnChangeListDeleagte<TEntity> OnChangeList;

        /// <summary>
        /// Список элементов
        /// </summary>
        List<TEntity> List { get; set; }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="item">
        /// Добавляемый элемент
        /// </param>
        /// <returns>
        /// True - если добавление прошло успешно, иначе false
        /// </returns>
        bool Add(TEntity item);

        /// <summary>
        /// Изменение
        /// </summary>
        /// <param name="item">
        /// Изменяемый элемент
        /// </param>
        void Edit(TEntity item);

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="item">
        /// Удаляемый элемент
        /// </param>
        /// <returns>
        /// True - если удаление прошло успешно, иначе false
        /// </returns>
        bool Delete(TEntity item);
    }
}