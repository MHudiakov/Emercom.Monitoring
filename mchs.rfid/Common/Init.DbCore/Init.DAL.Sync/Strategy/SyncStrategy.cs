// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SyncStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// // <summary>
//   Перечень разрешений для потоков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.Strategy
{
    using System;

    /// <summary>
    /// Стратегия применения изменений к БД
    /// </summary>
    public abstract class SyncStrategy
    {
        /// <summary>
        /// Добавление нового объекта
        /// </summary>
        /// <param name="change">
        /// Информация о добавлении нового объекта
        /// </param>
        public void AddItem(Change change)
        {
            if (change == null)
                throw new ArgumentNullException("change");
            this.AddItemOverride(change);
        }

        /// <summary>
        /// Добавление нового объекта
        /// </summary>
        /// <param name="change">
        /// Информация о добавлении нового объекта
        /// </param>
        protected abstract void AddItemOverride(Change change);

        /// <summary>
        /// Редактирование объекта
        /// </summary>
        /// <param name="change">
        /// Информация о редактировании объекта
        /// </param>
        public void EditItem(Change change)
        {
            if (change == null)
                throw new ArgumentNullException("change");
            if (change.GetFilterProps().Count == 0)
                throw new NotSupportedException(string.Format("Объект изменений не имеет ключевого поля."));
            this.EditItemOverride(change);
        }

        /// <summary>
        /// Редактирование объекта
        /// </summary>
        /// <param name="change">
        /// Информация о редактировании объекта
        /// </param>
        protected abstract void EditItemOverride(Change change);

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="change">
        /// Информация об удалении объекта
        /// </param>
        public void RemoveItem(Change change)
        {
            if (change == null)
                throw new ArgumentNullException("change");
            if (change.GetFilterProps().Count == 0)
                throw new NotSupportedException(string.Format("Объект изменений не имеет ключевого поля."));
            this.RemoveItemOverride(change);
        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="change">
        /// Информация об удалении объекта
        /// </param>
        protected abstract void RemoveItemOverride(Change change);
    }
}