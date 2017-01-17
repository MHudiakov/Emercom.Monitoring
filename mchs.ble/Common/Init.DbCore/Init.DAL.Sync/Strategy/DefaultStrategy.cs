// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// // <summary>
//   Перечень разрешений для потоков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.Strategy
{
    using System;
    using System.Linq;

    using Init.DbCore;

    /// <summary>
    /// Класс который заменяет свои операции над объектами в кэше
    /// на операции переданного ему в конструкторе объекта
    /// </summary>
    /// <typeparam name="T">
    /// Тип объекта к которому применяются изменения
    /// </typeparam>
    public class DefaultStrategy<T> : ProxyStrategy
        where T : DbObject
    {
        /// <summary>
        /// Метод для добавлления объекта в кэш
        /// </summary>
        private readonly Action<T> _updateCasheOnAdd;

        /// <summary>
        /// Метод для редактирования объекта в кэше
        /// </summary>
        private readonly Action<T, T> _updateCasheOnEdit;

        /// <summary>
        /// Метод для удаления объекта из кэша
        /// </summary>
        private readonly Action<T> _updateCasheOnDelete;

        /// <summary>
        /// Объект доступа к кэшу
        /// </summary>
        private readonly Cashe<T> _cashe;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DefaultStrategy{T}"/>. 
        /// Применение чейнжей для сущностей при синхронизации
        /// </summary>
        /// <param name="updateCasheOnAdd">
        /// Метод добавления объекта в кэш
        /// </param>
        /// <param name="updateCasheOnEdit">
        /// Метод для редактирования объекта в кэше
        /// </param>
        /// <param name="updateCasheOnDelete">
        /// Метод для удаления объекта из кэша
        /// </param>
        /// <param name="cashe">
        /// Доступ к управлению кэшем
        /// </param>
        public DefaultStrategy(
            Action<T> updateCasheOnAdd,
            Action<T, T> updateCasheOnEdit,
            Action<T> updateCasheOnDelete,
            Cashe<T> cashe)
            : this(updateCasheOnAdd, updateCasheOnEdit, updateCasheOnDelete, cashe, null)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DefaultStrategy{T}"/>. 
        /// Применение чейнжей для сущностей при синхронизации
        /// </summary>
        /// <param name="updateCasheOnAdd">
        /// Метод добавления объекта в кэш
        /// </param>
        /// <param name="updateCasheOnEdit">
        /// Метод для редактирования объекта в кэше
        /// </param>
        /// <param name="updateCasheOnDelete">
        /// Метод для удаления объекта из кэша
        /// </param>
        /// <param name="cashe">
        /// Доступ к управлению кэшем
        /// </param>
        /// <param name="syncStrategy">
        /// Behavior методы которого будут использоваться
        /// </param>
        public DefaultStrategy(
            Action<T> updateCasheOnAdd,
            Action<T, T> updateCasheOnEdit,
            Action<T> updateCasheOnDelete,
            Cashe<T> cashe,
            SyncStrategy syncStrategy)
            : base(syncStrategy)
        {
            if (updateCasheOnAdd == null)
                throw new ArgumentNullException("updateCasheOnAdd");

            if (updateCasheOnEdit == null)
                throw new ArgumentNullException("updateCasheOnEdit");

            if (updateCasheOnDelete == null)
                throw new ArgumentNullException("updateCasheOnDelete");

            if (cashe == null)
                throw new ArgumentNullException("cashe");

            this._updateCasheOnAdd = updateCasheOnAdd;
            this._updateCasheOnEdit = updateCasheOnEdit;
            this._updateCasheOnDelete = updateCasheOnDelete;
            this._cashe = cashe;
        }

        /// <summary>
        /// Добавление нового объекта
        /// </summary>
        /// <param name="change">
        /// Информация о добавлении нового объекта
        /// </param>
        protected override void AddItemOverride(Change change)
        {
            // выполняем обработку при добавлении в базовом классе
            base.AddItemOverride(change);
            var obj = change.GetItem<T>();

            // добавляем в кеш
            this._cashe.UpdateOrInsert(obj, (s, d) => s.CopyTo(d));
            this._updateCasheOnAdd(obj);
        }

        /// <summary>
        /// Редактирование объекта
        /// </summary>
        /// <param name="change">
        /// Информация о редактировании объекта
        /// </param>
        protected override void EditItemOverride(Change change)
        {
            // Выполняем редактирование в базовом классе
            base.EditItemOverride(change);

            var source = change.GetItem<T>();

            // если объекта нет в кеше, то не обновляем кеш
            var dest = this._cashe.Where(c => c.Equals(source)).SingleOrDefault();
            if (dest == null)
                return;

            // Редактируем в кэше
            this._cashe.UpdateOrInsert(source, (s, d) => s.CopyTo(d));
            this._updateCasheOnEdit(source, dest);
        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="change">
        /// Информация об удалении объекта
        /// </param>
        protected override void RemoveItemOverride(Change change)
        {
            // выполняем удаление в базовом классе
            base.RemoveItemOverride(change);

            var list = this._cashe.Where(e => e.PropEqualas(change.GetFilterProps())).ToList();
            if (list.Count == 0)
                return;

            // удаляем объект из кэша
            this._cashe.DeleteWhere(e => e.PropEqualas(change.GetFilterProps()));
            foreach (var item in list)
                this._updateCasheOnDelete(item);
        }
    }
}