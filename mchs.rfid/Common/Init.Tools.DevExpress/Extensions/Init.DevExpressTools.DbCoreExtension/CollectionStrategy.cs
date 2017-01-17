// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionStrategy.cs" company="����-�����">
//   ����-�����, 2013�.
// </copyright>
// <summary>
//   ��������� ������� ��� ������ � ���������� ��������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.DbCoreExtension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Init.Tools.DevExpress;

    /// <summary>
    /// ��������� ������� ��� ������ � ���������� ��������
    /// </summary>
    /// <typeparam name="T">��� ���������</typeparam>
    public class CollectionStrategy<T> : GhStrategy<T>
        where T : class, new()
    {
        /// <summary>
        /// ���������-�������� ������
        /// </summary>
        private readonly List<T> _collection;

        /// <summary>
        /// ��������� ����� �������� �������
        /// </summary>
        private readonly Func<T> _createItem;

        /// <summary>
        /// ��������� ������� ��� ������ � ���������� ��������
        /// </summary>
        /// <param name="collection">���������-�������� ������</param>
        public CollectionStrategy(List<T> collection)
            : this(collection, () => new T())
        {
        }

        /// <summary>
        /// ��������� ������� ��� ������ � ���������� ��������
        /// </summary>
        /// <param name="collection">���������-�������� ������</param>
        /// <param name="createItem">��������� ����� �������� �������</param>
        public CollectionStrategy(List<T> collection, Func<T> createItem)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (createItem == null)
                throw new ArgumentNullException("createItem");
            this._collection = collection;
            this._createItem = createItem;
        }

        /// <summary>
        /// �������� ���������� �������
        /// </summary>
        /// <param name="item">������ ���� TEntity</param>
        public override void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!this._collection.Exists(item.Equals))
                this._collection.Add(item);
        }

        /// <summary>
        /// �������� ��������� �������
        /// </summary>
        /// <param name="item">������ ���� TEntity</param>
        public override void Edit(T item)
        {
            // �������������� ��������� �����������
        }

        /// <summary>
        /// �������� �������� �������
        /// </summary>
        /// <param name="item">������ ���� TEntity</param>
        public override void Delete(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            this._collection.RemoveAll(item.Equals);
        }

        /// <summary>
        /// ��������� ������ ���� ��������
        /// </summary>
        /// <returns>������ �������� TEntity</returns>
        public override List<T> GetList()
        {
            return this._collection.ToList();
        }

        /// <summary>
        /// �������� ������ �������
        /// </summary>
        /// <returns>������ TEntity</returns>
        public override T CreateItem()
        {
            return this._createItem();
        }
    }
}