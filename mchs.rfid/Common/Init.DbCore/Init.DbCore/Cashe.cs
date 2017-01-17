// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cashe.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ���������� �����
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// ���������� �����
    /// </summary>
    /// <typeparam name="T">��� � ���� �������� ����� �������� ���������</typeparam>
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
        /// ���������� ��������� � ����
        /// </summary>
        public long Count
        {
            get { return this._innerList.Count; }
        }

        /// <summary>
        /// �������� ������� �� ����
        /// </summary>
        /// <param name="filter">
        /// ������� ��������� ��������
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
        /// �������� ��� �������� ������� � ����
        /// </summary>
        /// <param name="item">����������� ��� ����������� �������</param>
        /// <param name="copy">����� �����������, ������ �������� - ������ ��������, ������ - ������ ����������</param>
        /// <returns>�������� ������, ���� � ���� ��� ������ ��� ����������� ����� �� ����</returns>
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
        /// �������� ������ �������� �� ���� �� �������
        /// </summary>
        /// <param name="filter">�������</param>
        /// <returns>������ ��������</returns>
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
        /// ��������� ������� ��� ��������� ���� � ���������������� ������
        /// </summary>
        /// <typeparam name="TResult">
        /// ��� ����������
        /// </typeparam>
        /// <param name="func">
        /// ������������������ �������������� IEnumerable
        /// </param>
        /// <returns>
        /// ��������� �������������� IEnumerable
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