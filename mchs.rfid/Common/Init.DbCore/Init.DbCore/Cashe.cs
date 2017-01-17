// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cashe.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Управление кэшем
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Управление кэшем
    /// </summary>
    /// <typeparam name="T">Тип в кэше которого будут вносится изменения</typeparam>
    public class Cashe<T>
        where T : class
    {
        /// <summary>
        /// Lock
        /// </summary>
        private readonly ReaderWriterLockSlim _writerLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Cashe
        /// </summary>
        private readonly List<T> _innerList = new List<T>();

        /// <summary>
        /// Количество элементов в кеше
        /// </summary>
        public long Count
        {
            get { return this._innerList.Count; }
        }

        /// <summary>
        /// Удаление объекта из кэша
        /// </summary>
        /// <param name="filter">
        /// Филтьтр удаляемых объектов
        /// </param>
        public void DeleteWhere(Predicate<T> filter)
        {
            if (filter == null)
                throw new ArgumentNullException("filter");

            this._writerLock.EnterWriteLock();
            try
            {
                this._innerList.RemoveAll(filter);
            }
            finally
            {
                this._writerLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Добавить или обновить элемент в кэше
        /// </summary>
        /// <param name="item">Добавляемый или обновляемый элемент</param>
        /// <param name="copy">Метод копирования, первый аргумент - объект источник, второй - объект назначение</param>
        /// <returns>Исходный объект, если в кеше его небыло или обновленная копия из кеша</returns>
        public T UpdateOrInsert(T item, Action<T, T> copy)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (copy == null)
                throw new ArgumentNullException("copy");

            T cashedItem;
            try
            {
                this._writerLock.EnterReadLock();
                cashedItem = this._innerList.SingleOrDefault(e => e.Equals(item));
            }
            finally
            {
                this._writerLock.ExitReadLock();
            }

            if (cashedItem == null)
            {
                this._writerLock.EnterUpgradeableReadLock();
                try
                {
                    if (item is IEquatable<T>)
                        cashedItem = this._innerList.SingleOrDefault(e => ((IEquatable<T>)item).Equals(e));
                    else
                        cashedItem = this._innerList.SingleOrDefault(item.Equals);

                    if (cashedItem == null)
                    {
                        this._writerLock.EnterWriteLock();
                        try
                        {
                            this._innerList.Add(item);
                        }
                        finally
                        {
                            this._writerLock.ExitWriteLock();
                        }
                    }
                }
                finally
                {
                    this._writerLock.ExitUpgradeableReadLock();
                }
            }

            if (cashedItem != null)
            {
                copy(item, cashedItem);
                return cashedItem;
            }

            return item;
        }

        /// <summary>
        /// Получить список объектов из кэша по условию
        /// </summary>
        /// <param name="filter">Условие</param>
        /// <returns>Список объектов</returns>
        public List<T> Where(Func<T, bool> filter)
        {
            if (filter == null)
                throw new ArgumentNullException("filter");

            this._writerLock.EnterReadLock();
            try
            {
                return this._innerList.Where(filter).ToList();
            }
            finally
            {
                this._writerLock.ExitReadLock();
            }
        }

        /// <summary>
        /// Выполнить функцию над элемнтами кеша в потокобезопасном режиме
        /// </summary>
        /// <typeparam name="TResult">
        /// Тип результата
        /// </typeparam>
        /// <param name="func">
        /// Последовательность преобразований IEnumerable
        /// </param>
        /// <returns>
        /// Рузультат преобразований IEnumerable
        /// </returns>
        public List<TResult> ProcessEnumerable<TResult>(Func<IList<T>, IList<TResult>> func)
        {
            if (func == null)
                throw new ArgumentNullException("func");

            try
            {
                this._writerLock.EnterReadLock();
                return func(this._innerList).ToList();
            }
            finally
            {
                if (this._writerLock.IsReadLockHeld)
                    this._writerLock.ExitReadLock();
            }
        }
    }
}