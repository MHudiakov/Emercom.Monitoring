// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ��������� ����� ������� � ������ ������������� ����
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using Init.DbCore.Metadata;

    /// <summary>
    /// ������� ����� ����� �������
    /// </summary>
    /// <typeparam name="T">��� ������</typeparam>
    public abstract class DataAccess<T>
        where T : DbObject
    {
        /// <summary>
        /// ��������������� ����� ��� ������ � DbObject
        /// </summary>
        protected DbObjectMetadata Metadata { get; private set; }

        /// <summary>
        /// ������� ����� ����� �������
        /// </summary>
        protected DataAccess()
        {
            this.Metadata = new DbObjectMetadata(typeof(T));
        }

        /// <summary>
        /// ��������� ������ ���� ��������
        /// </summary>
        /// <returns>
        /// ������ ���� �������� � �������
        /// </returns>
        public abstract List<T> GetAll();

        /// <summary>
        /// ��������� ���������� ��������
        /// </summary>
        /// <returns>
        /// ���������� �������� � ��
        /// </returns>
        public abstract long GetCount();

        /// <summary>
        /// ���������� �������
        /// </summary>
        /// <param name="item">������</param>
        protected abstract void AddOverride(T item);

        /// <summary>
        /// ���������� �������
        /// </summary>
        /// <param name="item">������</param>
        protected abstract void EditOverride(T item);

        /// <summary>
        /// �������� �������� ������� �� ��������� ��������� �����
        /// </summary>
        /// <param name="whereArgs">
        /// ����� �������
        /// </param>
        protected abstract void DeleteWhereOverride(Dictionary<string, object> whereArgs);

        /// <summary>
        /// ��������� ����� �������� �� ������ ������ �����
        /// </summary>
        /// <param name="whereArgs">
        /// ���� ������ ��������
        /// </param>
        /// <returns>
        /// ������ �������� ���������� ��� ��������
        /// </returns>
        protected abstract List<T> GetItemsWhereOverride(Dictionary<string, object> whereArgs);

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
        /// ������ �������� �� �������� pageIndex
        /// </returns>
        protected abstract List<T> GetPageOverride(int pageIndex, int pageSize);

        /// <summary>
        /// ���������� �������
        /// </summary>
        /// <param name="item">������</param>
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            AddOverride(item);
        }

        /// <summary>
        /// ���������� �������
        /// </summary>
        /// <param name="item">������</param>
        public void Edit(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (this.Metadata.Key == null)
                throw new NotSupportedException(string.Format("������ {0} �� ����� ��������� ����. �������� �� ������ �������� ��� ��������� [DbKey]", typeof(T).FullName));

            this.EditOverride(item);
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
        /// ������ �������� �� �������� pageIndex
        /// </returns>
        public List<T> GetPage(int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
                throw new ArgumentException(@"����� �������� �� ����� ���� �������������", "pageIndex");
            if (pageSize <= 0)
                throw new ArgumentException(@"������ �������� ������ ���� �������������", "pageSize");
            if (this.Metadata.Key == null)
                throw new NotSupportedException(string.Format("������ {0} �� ����� ��������� ����. �������� �� ������ �������� ��� ��������� [DbKey]", typeof(T).FullName));

            return this.GetPageOverride(pageIndex, pageSize);
        }

        /// <summary>
        /// �������� �������� �� �������
        /// </summary>
        /// <param name="whereArgs">
        /// ����� �������
        /// </param>
        public void DeleteWhere(Dictionary<string, object> whereArgs)
        {
            // ��������� �������� �������� �� ���������
            foreach (var pair in whereArgs)
                if (!this.Metadata.Properties.ContainsKey(pair.Key))
                    throw new ArgumentException(string.Format("������ [{1}] �� �������� ��������: {0}. ��������, �������� �� ���� �������� ��������� [DataMember].", pair.Key, typeof(T).FullName), "whereArgs");
            this.DeleteWhereOverride(whereArgs);
        }

        /// <summary>
        /// �������� �������� �� ���������� ����
        /// </summary>
        /// <typeparam name="TSelectorType">��� ���������</typeparam>
        /// <param name="selector">�������� ����</param>
        /// <param name="key">�������� ����</param>
        public void DeleteWhere<TSelectorType>(Expression<Func<T, TSelectorType>> selector, TSelectorType key)
        {
            if (selector == null)
                throw new ArgumentNullException("selector");

            var memberExpression = selector.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException(@"�������� ������ ���� MemberExpression", "selector");

            var member = memberExpression.Member as PropertyInfo;

            if (member == null)
                throw new ArgumentException(@"�������� ������ ���������� ��������", "selector");

            if (!this.Metadata.Properties.ContainsKey(member.Name))
                throw new ArgumentException(string.Format("�������� ������ ���������� �������� ������ [{0}]", typeof(T).FullName), "selector");

            this.DeleteWhere(new Dictionary<string, object> { { member.Name, key } });
        }

        /// <summary>
        /// �������� �������
        /// </summary>
        /// <typeparam name="TKey">��� ����� (��� �������� ����� �� ����� ����������)</typeparam>
        /// <param name="key">
        /// ���� �������
        /// </param>
        public void Delete<TKey>(TKey key)
        {
            if (this.Metadata.Key == null)
                throw new NotSupportedException(string.Format("������ {0} �� ����� ��������� ����. �������� �� ������ �������� ��� ��������� [DbKey]", typeof(T).FullName));
            if (!this.Metadata.Key.PropertyInfo.PropertyType.IsInstanceOfType(key))
                throw new NotSupportedException(string.Format("�������������� ����� �����. �������� ��� {0}", this.Metadata.Key.PropertyInfo.PropertyType.FullName));
            this.DeleteWhere(new Dictionary<string, object> { { this.Metadata.Key.PropertyInfo.Name, key } });
        }

        /// <summary>
        /// ������� ������
        /// </summary>
        /// <param name="item">������ ��� ��������</param>
        public void Delete(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (this.Metadata.Key == null)
                throw new NotSupportedException(string.Format("������ {0} �� ����� ��������� ����. �������� �� ������ �������� ��� ��������� [DbKey]", this.GetType().FullName));
            if (!item.IsKeysInitialised())
                throw new ArgumentException(@"����� ������� �� ����������������", "item");
            this.Delete(item.KeyValue);
        }

        /// <summary>
        /// ��������� ������� �� ������������� ID
        /// </summary>
        /// <param name="key">
        /// ���� �������
        /// </param>
        /// <typeparam name="TKey">��� ����� (��� �������� ����� �� ����� ����������)</typeparam>
        /// <returns>������</returns>
        public T Get<TKey>(TKey key)
        {
            if (this.Metadata.Key == null)
                throw new NotSupportedException(string.Format("������ {0} �� ����� ��������� ����. �������� �� ������ �������� ��� ��������� [DbKey]", this.GetType().FullName));

            return this.GetItemsWhere(new Dictionary<string, object> { { this.Metadata.Key.PropertyInfo.Name, key } }).SingleOrDefault();
        }

        /// <summary>
        /// ��������� �������� �� �����
        /// </summary>
        /// <typeparam name="TSelectorType">
        /// ��� �����
        /// </typeparam>
        /// <param name="selector">
        /// �������� �����
        /// </param>
        /// <param name="key">
        /// �������� �����
        /// </param>
        /// <returns>
        /// ������ �������� ��������������� ����� ������
        /// </returns>
        public List<T> GetItemsWhere<TSelectorType>(Expression<Func<T, TSelectorType>> selector, TSelectorType key)
        {
            if (selector == null)
                throw new ArgumentNullException("selector");

            var memberExpression = selector.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException(@"�������� ������ ���� MemberExpression", "selector");

            var member = memberExpression.Member as PropertyInfo;

            if (member == null)
                throw new ArgumentException(@"�������� ������ ���������� ��������", "selector");

            if (!this.Metadata.Properties.ContainsKey(member.Name))
                throw new ArgumentException(string.Format("�������� ������ ���������� �������� ������ [{0}]", typeof(T).FullName), "selector");

            return this.GetItemsWhere(new Dictionary<string, object> { { member.Name, key } });
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
        public List<T> GetItemsWhere(Dictionary<string, object> whereArgs)
        {
            // ��������� �������� �������� �� ���������
            foreach (var pair in whereArgs)
                if (!this.Metadata.Properties.ContainsKey(pair.Key))
                    throw new ArgumentException(string.Format("������ [{1}] �� �������� ��������: {0}. ��������, �������� �� ���� �������� ��������� [DataMember].", pair.Key, typeof(T).FullName), "whereArgs");
            return this.GetItemsWhereOverride(whereArgs);
        }
    }

    /// <summary>
    /// ������� ����� ����� �������
    /// </summary>
    /// <typeparam name="T">��� ������</typeparam>
    /// <typeparam name="TContext">��� ���������</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "����������-�������")]
    public abstract class DataAccess<T, TContext> : DataAccess<T>
        where T : DbObject
    {
        /// <summary>
        /// ������� ��������� ������ �� �������� ������� � ������
        /// </summary>
        private readonly Func<TContext> _getContext;

        /// <summary>
        /// �������� ������� � ��
        /// </summary>
        protected virtual TContext Context
        {
            get { return this._getContext(); }
        }

        /// <summary>
        ///  ������� ����� ����� �������
        /// </summary>
        /// <param name="getContext">������� ��������� �������� ������� � ��</param>
        protected DataAccess(Func<TContext> getContext)
        {
            if (getContext == null)
                throw new ArgumentNullException("getContext");
            _getContext = getContext;
        }
    }
}