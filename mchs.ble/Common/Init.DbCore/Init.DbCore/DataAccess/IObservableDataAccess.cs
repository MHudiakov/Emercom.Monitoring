// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IObservableDataAccess.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Наблюдаемый репозиторй.
//   При размещении объектов в нем можно организовать автоматическое обновление навигационных свойств в объектах
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DataAccess
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Наблюдаемый репозиторй. 
    /// При размещении объектов в нем можно организовать автоматическое обновление навигационных свойств в объектах
    /// </summary>
    /// <typeparam name="T">тип шлюза записи</typeparam>
    public interface IObservableDataAccess<T>
        where T : class
    {
        /// <summary>
        /// Вызывается после добавлении элемента
        /// </summary>
        event Action<T> AfterAdd;

        /// <summary>
        /// Вызывается перед добавлением элемента
        /// </summary>
        event Action<T> BeforeAdd;

        /// <summary>
        /// Вызывается после удаления элемента
        /// </summary>
        event Action<Dictionary<string, object>> AfterDelete;

        /// <summary>
        /// Вызывается перед удалением элемента
        /// </summary>
        event Action<Dictionary<string, object>> BeforeDelete;

        /// <summary>
        /// Вызывается после редактирования объекта
        /// </summary>
        event Action<T> AfterEdit;

        /// <summary>
        /// Вызывается перед редастированием элемента
        /// </summary>
        event Action<T> BeforeEdit;
    }
}