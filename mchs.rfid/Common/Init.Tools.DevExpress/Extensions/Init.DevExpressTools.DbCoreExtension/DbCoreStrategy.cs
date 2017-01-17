// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbCoreStrategy.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ��������� GridHelper ��� ����������� DbCore
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.DbCoreExtension
{
    using System;
    using System.Collections.Generic;

    using Init.DbCore;
    using Init.DbCore.DataAccess;
    using Init.Tools.DevExpress;

    /// <summary>
    /// ��������� GridHelper ��� ����������� DbCore
    /// </summary>
    /// <typeparam name="T">
    /// ��� ������ ���������
    /// </typeparam>
    public class DbCoreStrategy<T> : GhStrategy<T>
        where T : DbObject, new()
    {
        /// <summary>
        /// ���� ������� � ������
        /// </summary>
        private readonly DataAccess<T> _dataAccess;

        /// <summary>
        /// ��������� ����� �������� �������
        /// </summary>
        private readonly Func<T> _createItem;

        /// <summary>
        /// ����� ��������� ������ ��������
        /// </summary>
        private readonly Func<List<T>> _getList;

        /// <summary>
        /// ��������� GridHelper ��� ����������� DbCore
        /// </summary>
        /// <param name="dataAccess">���� ������� � ������</param>
        public DbCoreStrategy(DataAccess<T> dataAccess)
            : this(dataAccess, () => new T(), dataAccess.GetAll)
        {
        }

        /// <summary>
        /// ��������� GridHelper ��� ����������� DbCore
        /// </summary>
        /// <param name="dataAccess">���� ������� � ������</param>
        /// <param name="createItem">��������� ����� �������� �������</param>
        /// <param name="getList">����� ��������� ������ ��������</param>
        public DbCoreStrategy(DataAccess<T> dataAccess, Func<T> createItem, Func<List<T>> getList)
        {
            if (dataAccess == null)
                throw new ArgumentNullException("dataAccess");
            if (createItem == null)
                throw new ArgumentNullException("createItem");
            if (getList == null)
                throw new ArgumentNullException("getList");

            this._dataAccess = dataAccess;
            this._createItem = createItem;
            this._getList = getList;
        }

        /// <summary>
        /// �������� ���������� �������
        /// </summary>
        /// <param name="item">������ ���� TEntity</param>
        public override void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._dataAccess.Add(item);
        }

        /// <summary>
        /// �������� ��������� �������
        /// </summary>
        /// <param name="item">������ ���� TEntity</param>
        public override void Edit(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._dataAccess.Edit(item);
        }

        /// <summary>
        /// �������� �������� �������
        /// </summary>
        /// <param name="item">������ ���� TEntity</param>
        public override void Delete(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._dataAccess.Delete(item);
        }

        /// <summary>
        /// ��������� ������ ���� ��������
        /// </summary>
        /// <returns>������ �������� TEntity</returns>
        public override List<T> GetList()
        {
            return this._getList();
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
