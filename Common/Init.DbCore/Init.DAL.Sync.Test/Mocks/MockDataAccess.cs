// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockDataAccess.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Шлюз записи-заглушка
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.Test.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Init.DbCore;
    using Init.DbCore.DataAccess;

    /// <summary>
    /// Шлюз записи-заглушка
    /// </summary>
    /// <typeparam name="T">
    /// Тип шлюза записи
    /// </typeparam>
    internal class MockDataAccess<T> : DataAccess<T>
        where T : DbObject
    {
        /// <summary>
        /// Счетчик идентификаторов
        /// </summary>
        private int _counter = 1;

        /// <summary>
        /// Содержимое таблицы
        /// </summary>
        public Cashe<T> Cashe { get; private set; }

        /// <summary>
        /// Шлюз записи-заглушка
        /// </summary>
        public MockDataAccess()
        {
            Cashe = new Cashe<T>();
        }

        #region Overrides of DataAccess<T>

        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <returns>
        /// Список всех объектов в таблице
        /// </returns>
        public override List<T> GetAll()
        {
            return this.Cashe.Where(e => true);
        }

        /// <summary>
        /// Получение количества объектов
        /// </summary>
        /// <returns>
        /// Количество объектов в БД
        /// </returns>
        public override long GetCount()
        {
            return this.Cashe.Count;
        }

        /// <summary>
        /// Добавление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void AddOverride(T item)
        {
            object ident = _counter;
            if (item.Identity != null)
            {
                if (item.Identity.PropertyInfo.PropertyType == typeof(Guid))
                    ident = Guid.NewGuid();
                item.Identity.Setter(item, ident);
                _counter++;
            }

            Cashe.UpdateOrInsert(item, (s, d) => s.CopyTo(d));
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void EditOverride(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            Cashe.UpdateOrInsert(item, (s, d) => s.CopyTo(d));
        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="keys">
        /// Ключи объекта
        /// </param>
        protected override void DeleteWhereOverride(Dictionary<string, object> keys)
        {
            if (keys == null)
                throw new ArgumentNullException("keys");

            Cashe.DeleteWhere(e => e.PropEqualas(keys));
        }

        /// <summary>
        /// Получение сиска объектов по ключу
        /// </summary>
        /// <param name="fields">Ключ отбора объектов</param>
        /// <returns>Список объектов подходящий под критерий</returns>
        protected override List<T> GetItemsWhereOverride(Dictionary<string, object> fields)
        {
            var cashedItems = Cashe.Where(e =>
            {
                var itemKeys = e.GetProperties();
                return fields.All(k => itemKeys.ContainsKey(k.Key) && itemKeys[k.Key].Equals(k.Value));
            });
            return cashedItems;
        }

        /// <summary>
        /// Получение части объектов
        /// </summary>
        /// <param name="pageIndex">
        /// Номер страницы
        /// </param>
        /// <param name="pageSize">
        /// Количество объектов
        /// </param>
        /// <returns>
        /// Список объектов со страницы pageIndex
        /// </returns>
        protected override List<T> GetPageOverride(int pageIndex, int pageSize)
        {
            var cachedItems = this.GetAll();

            cachedItems.Sort(
                     (left, right) =>
                     {
                         var propertyInfo = Metadata.Key;
                         var leftValue = (IComparable)propertyInfo.Getter(left);
                         var rightValue = (IComparable)propertyInfo.Getter(right);
                         return leftValue.CompareTo(rightValue);
                     });
            return cachedItems.Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }

        #endregion
    }
}
