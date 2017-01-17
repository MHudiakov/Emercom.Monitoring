// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataManager.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   �������� ������. �������������� �� ������� � ������� ����������������� ������������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using Init.DbCore.DataAccess;

    /// <summary>
    ///  �������� ������. �������������� �� ������� � ������� ����������������� ������������
    /// </summary>
    public class DataManager
    {
        /// <summary>
        /// ����� ��������� ������ �� �������� ������� � ��
        /// </summary>
        private readonly Func<object> _getContext;

        /// <summary>
        /// � ����������� �� ������� ������/������
        /// ���� ���������� ������ �������� (WcfClient ��� DB)
        /// </summary>
        /// <param name="context">
        /// ��������
        /// </param>
        public DataManager(object context)
            : this(() => context)
        {
        }

        /// <summary>
        /// ������� ����� ��������� ������
        /// </summary>
        /// <param name="getContext">������� ��������� ��������� ������� � ������</param>
        public DataManager(Func<object> getContext)
        {
            if (getContext == null)
                throw new ArgumentNullException("getContext");
            this._getContext = getContext;
            this._repositoryList = new List<object>();
        }

        /// <summary>
        /// �������� (WcfClient ��� DB)
        /// </summary>
        /// <returns>
        /// �������� ������� � ������
        /// </returns>
        public virtual object GetContext()
        {
            return this._getContext();
        }

        /// <summary>
        /// C��c�� ������������, 
        /// ���� ������������ ��������� �����������
        /// ����� ����������� ������� ���� ����������� ������ � �����
        /// ���������� ��� ������� DataManager
        /// </summary>
        private readonly List<object> _repositoryList;

        /// <summary>
        /// ���������� ����������� ���� T.
        /// </summary>
        /// <typeparam name="T">
        /// ��� �����������
        /// </typeparam>
        /// <returns>
        /// ����� ��� ��� ��������� �����������
        /// </returns>
        public virtual DataAccess<T> GetRepository<T>()
            where T : DbObject, new()
        {
            lock (this._repositoryList)
            {
                // ����� ���� � ������ ������������
                DataAccess<T> repository = this._repositoryList.OfType<DataAccess<T>>().FirstOrDefault();
                if (repository != null)
                    return repository;

                repository = this.CreateRepository<T>();
                this._repositoryList.Add(repository);
                return repository;
            }
        }

        /// <summary>
        /// ��������� ����� ��� �������� ���������� Repository T
        /// ������� ����� �����������, ��������� ��� this.DataManager
        /// (���� ����� ������������ ������ GetRepository T)
        /// ������������� �� ������� � �������
        /// ��� ���������� ������ ���� �����������, ����� ������� �������� ����� ��� ���������
        /// </summary>
        /// <typeparam name="T">��� �����������</typeparam>
        /// <returns>����� ��� ��� ��������� �����������</returns>
        protected DataAccess<T> CreateRepository<T>() where T : DbObject, new()
        {
            var type = typeof(T);
            if (!_repositoryBuilders.ContainsKey(type))
                throw new ArgumentException(string.Format("��� ����: {0} �� ��������������� ��������� �����������.", type.FullName));
            var builder = (Func<DataManager, DataAccess<T>>)_repositoryBuilders[type];
            return builder(this);
        }

        /// <summary>
        /// ������������ ��������� �����������
        /// </summary>
        /// <typeparam name="T">��� �����������</typeparam>
        /// <param name="dataAccess">��������� �����������</param>
        public virtual void RegisterRepository<T>(DataAccess<T> dataAccess)
            where T : DbObject
        {
            lock (this._repositoryList)
            {
                // ���������, ��� ������ ����������� ��� ���
                DataAccess<T> repository = this._repositoryList.OfType<DataAccess<T>>().FirstOrDefault();
                if (repository != null)
                    throw new ArgumentException(string.Format("����������� ��� ���� [{0}] ��� ���������������.", typeof(T).FullName), "dataAccess");

                // ������������ � ������ ������������
                this._repositoryList.Add(dataAccess);
            }
        }

        /// <summary>
        /// ������� ���������� ������������
        /// </summary>
        private readonly Dictionary<Type, Delegate> _repositoryBuilders = new Dictionary<Type, Delegate>();

        /// <summary>
        /// ������������ ������� ���������� �����������
        /// </summary>
        /// <typeparam name="T">��� �����������</typeparam>
        /// <param name="builder">������� ���������</param>
        public void RegiserBuilder<T>(Func<DataManager, DataAccess<T>> builder)
            where T : DbObject
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (_repositoryBuilders.ContainsKey(typeof(T)))
                throw new ArgumentException(string.Format("��� ���� {0} ��� ��������������� ���������.", typeof(T).FullName));

            _repositoryBuilders.Add(typeof(T), builder);
        }
    }

    /// <summary>
    /// �������� ������ � �������������� ����������
    /// </summary>
    /// <typeparam name="TContext">��� ���������</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Generic �������")]
    public class DataManager<TContext> : DataManager
        where TContext : class
    {
        /// <summary>
        /// �������� ������ � �������������� ����������
        /// </summary>
        /// <param name="getContext">������� ��������� ���������</param>
        public DataManager(Func<TContext> getContext)
            : base(() => getContext())
        {
        }

        /// <summary>
        /// �������� (WcfClient ��� DB)
        /// </summary>
        /// <returns>
        /// �������� ������� � ������
        /// </returns>
        public new TContext GetContext()
        {
            return (TContext)base.GetContext();
        }
    }
}