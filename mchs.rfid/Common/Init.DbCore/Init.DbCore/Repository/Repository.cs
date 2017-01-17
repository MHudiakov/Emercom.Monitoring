// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ������� ����� ����������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Repository
{
    using System;
    using System.Collections.Generic;

    using Init.DbCore.DataAccess;
    using Init.DbCore.Processing;

    /// <summary>
    /// ������� ����� ����������
    /// </summary>
    /// <typeparam name="T">��� �����������</typeparam>
    public class Repository<T> : DataAccess<T>, IObservableDataAccess<T>
        where T : DbObject
    {
        /// <summary>
        /// ���� ������� � ������
        /// </summary>
        private readonly DataAccess<T> _dataAccess;

        /// <summary>
        /// �������� ������
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// �������� ������
        /// </summary>
        public virtual DataManager DataManager
        {
            get { return this._dataManager; }
        }

        /// <summary>
        /// ���� �������
        /// </summary>
        protected virtual DataAccess<T> DataAccess
        {
            get { return this._dataAccess; }
        }

        /// <summary>
        /// ������� �����������
        /// </summary>
        /// <param name="dataAccess">������ ������� � ������</param>
        /// <param name="dataManager">DataManager</param>
        public Repository(DataAccess<T> dataAccess, DataManager dataManager)
        {
            if (dataAccess == null)
                throw new ArgumentNullException("dataAccess");

            if (dataManager == null)
                throw new ArgumentNullException("dataManager");

            this._dataManager = dataManager;
            this._dataAccess = dataAccess;
        }

        /// <summary>
        /// ��������� ���� ��������
        /// </summary>
        /// <returns>
        /// ������ ���� �������� � �������
        /// </returns>
        public override List<T> GetAll()
        {
            return this._dataAccess.GetAll();
        }

        /// <summary>
        /// ��������� ���������� ��������
        /// </summary>
        /// <returns>
        /// ���������� �������� � ��
        /// </returns>
        public override long GetCount()
        {
            return this._dataAccess.GetCount();
        }

        /// <summary>
        /// ���������� �������
        /// </summary>
        /// <param name="item">������</param>
        protected override void AddOverride(T item)
        {
            this.OnBeforeAdd(item);
            ModelProcessor<T>.Active.Process(item);
            this._dataAccess.Add(item);
            this.OnAfterAdd(item);
        }

        /// <summary>
        /// ���������� �������
        /// </summary>
        /// <param name="item">������</param>
        protected override void EditOverride(T item)
        {
            this.OnBeforeEdit(item);
            ModelProcessor<T>.Active.Process(item);
            this._dataAccess.Edit(item);
            this.OnAfterEdit(item);
        }

        /// <summary>
        /// �������� �������� ������� �� ��������� ��������� �����
        /// </summary>
        /// <param name="whereArgs">
        /// ����� �������
        /// </param>
        protected override void DeleteWhereOverride(Dictionary<string, object> whereArgs)
        {
            this.OnBeforeDelete(whereArgs);
            this._dataAccess.DeleteWhere(whereArgs);
            this.OnAfterDelete(whereArgs);
        }

        /// <summary>
        /// ��������� ����� �������� �� ������ ������ �����
        /// </summary>
        /// <param name="whereArgs">
        /// ���� ������ ��������
        /// </param>
        /// <returns>
        /// ������ �������� ���������� ��� ��������
        /// </returns>
        protected override List<T> GetItemsWhereOverride(Dictionary<string, object> whereArgs)
        {
            return this._dataAccess.GetItemsWhere(whereArgs);
        }

        /// <summary>
        /// ��������� ����� ��������
        /// </summary>
        /// <param name="pageIndex">
        /// ����� ��������
        /// </param>
        /// <param name="pageSize">
        /// ���������� ��������
        /// </param>
        /// <returns>
        /// ������ �������� ������� � idFrom ������� �� ����� pageSize
        /// </returns>
        protected override List<T> GetPageOverride(int pageIndex, int pageSize)
        {
            return this._dataAccess.GetPage(pageIndex, pageSize);
        }

        #region OnAdd
        /// <summary>
        /// ���������� ������� �� ���������� �������� ����������
        /// </summary>
        /// <param name="item">
        /// ����������� �������
        /// </param>
        protected virtual void OnAfterAdd(T item)
        {
            Action<T> handler = this.AfterAdd;
            if (handler != null) handler(item);
        }

        /// <summary>
        /// ���������� ����� ���������� ��������
        /// </summary>
        public event Action<T> AfterAdd;

        /// <summary>
        /// ���������� ����� ����������� ��������
        /// </summary>
        public event Action<T> BeforeAdd;

        /// <summary>
        /// ���������� ������� ����� ��������� ����������
        /// </summary>
        /// <param name="item">
        /// ����������� �������
        /// </param>
        protected virtual void OnBeforeAdd(T item)
        {
            Action<T> handler = this.BeforeAdd;
            if (handler != null) handler(item);
        }
        #endregion

        #region OnDelete
        /// <summary>
        /// ���������� ������� ����� �������� ��������
        /// </summary>
        /// <param name="keys">
        /// ������������� ���������� ��������
        /// </param>
        protected virtual void OnAfterDelete(Dictionary<string, object> keys)
        {
            Action<Dictionary<string, object>> handler = this.AfterDelete;
            if (handler != null) handler(keys);
        }

        /// <summary>
        /// ���������� ����� �������� ��������
        /// </summary>
        public event Action<Dictionary<string, object>> AfterDelete;

        /// <summary>
        /// ���������� ����� ��������� ��������
        /// </summary>
        public event Action<Dictionary<string, object>> BeforeDelete;

        /// <summary>
        /// ���������� ������� ����� ��������� ��������
        /// </summary>
        /// <param name="whereArgs">
        /// ������������� ���������� ��������
        /// </param>
        protected virtual void OnBeforeDelete(Dictionary<string, object> whereArgs)
        {
            Action<Dictionary<string, object>> handler = this.BeforeDelete;
            if (handler != null)
                handler(whereArgs);
        }
        #endregion

        #region OnEdit
        /// <summary>
        /// ���������� ������� ����� �������������� ��������
        /// </summary>
        /// <param name="item">
        /// ������������� �������
        /// </param>
        protected virtual void OnAfterEdit(T item)
        {
            Action<T> handler = this.AfterEdit;
            if (handler != null) handler(item);
        }

        /// <summary>
        /// ���������� ����� �������������� �������
        /// </summary>
        public event Action<T> AfterEdit;

        /// <summary>
        /// ���������� ����� ��������������� ��������
        /// </summary>
        public event Action<T> BeforeEdit;

        /// <summary>
        /// ���������� ������� ����� ��������������� ��������
        /// </summary>
        /// <param name="item">
        /// ������������� �������
        /// </param>
        protected virtual void OnBeforeEdit(T item)
        {
            Action<T> handler = this.BeforeEdit;
            if (handler != null) handler(item);
        }
        #endregion
    }
}