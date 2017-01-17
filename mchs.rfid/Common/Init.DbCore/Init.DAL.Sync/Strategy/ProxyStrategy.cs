// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyStrategy.cs" company="ИНИТ-центр">
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
    /// Класс который заменяет свои операции над объектами
    /// на операции переданного ему в конструкторе объекта
    /// </summary>
    public class ProxyStrategy : SyncStrategy
    {
        /// <summary>
        /// SyncStrategy методы которой используем
        /// </summary>
        private readonly SyncStrategy _syncStrategy;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProxyStrategy"/>. 
        /// Создать объект который использует методы передаваемого ему Behaviora
        /// в своих операциях по работе над изменением
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Нельзя передавать пустой Behavior
        /// </exception>
        public ProxyStrategy()
            : this(null)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ProxyStrategy"/>. 
        /// Создать объект который использует методы передаваемого ему Behaviora
        /// в своих операциях по работе над изменением
        /// </summary>
        /// <param name="syncStrategy">
        /// SyncStrategy методы которой будут использоваться
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Нельзя передавать пустой Behavior
        /// </exception>
        public ProxyStrategy(SyncStrategy syncStrategy)
        {
            this._syncStrategy = syncStrategy;
        }

        /// <summary>
        /// Добавление нового объекта
        /// </summary>
        /// <param name="change">
        /// Информация о добавлении нового объекта
        /// </param>
        protected override void AddItemOverride(Change change)
        {
            if (this._syncStrategy != null)
                this._syncStrategy.AddItem(change);
        }

        /// <summary>
        /// Редактирование объекта
        /// </summary>
        /// <param name="change">
        /// Информация о редактировании объекта
        /// </param>
        protected override void EditItemOverride(Change change)
        {
            if (this._syncStrategy != null)
                this._syncStrategy.EditItem(change);
        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="change">
        /// Информация об удалении объекта
        /// </param>
        protected override void RemoveItemOverride(Change change)
        {
            if (change == null)
                throw new ArgumentNullException("change");

            if (this._syncStrategy != null)
                this._syncStrategy.RemoveItem(change);
        }
    }
}