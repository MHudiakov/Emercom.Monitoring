// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataManager.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Менеджер данных. Доопределяется на клиенте и сервере соответствуюущими реализациями
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
    ///  Менеджер данных. Доопределяется на клиенте и сервере соответствуюущими реализациями
    /// </summary>
    public class DataManager
    {
        /// <summary>
        /// Метод получения ссылки на контекст доступа к БД
        /// </summary>
        private readonly Func<object> _getContext;

        /// <summary>
        /// В зависимости от стороны Клиент/сервер
        /// сюда передается разный контекст (WcfClient или DB)
        /// </summary>
        /// <param name="context">
        /// Контекст
        /// </param>
        public DataManager(object context)
            : this(() => context)
        {
        }

        /// <summary>
        /// Базовый класс менеджера данных
        /// </summary>
        /// <param name="getContext">Функция получения контекста доступа к данным</param>
        public DataManager(Func<object> getContext)
        {
            if (getContext == null)
                throw new ArgumentNullException("getContext");
            this._getContext = getContext;
            this._repositoryList = new List<object>();
        }

        /// <summary>
        /// Контекст (WcfClient или DB)
        /// </summary>
        /// <returns>
        /// Контекст доступа к данным
        /// </returns>
        public virtual object GetContext()
        {
            return this._getContext();
        }

        /// <summary>
        /// Cпиcок репозиториев, 
        /// сюда скдадываются созданные репозитории
        /// чтобы репозиторий каждого типа существовал только в одном
        /// экземпляре для данного DataManager
        /// </summary>
        private readonly List<object> _repositoryList;

        /// <summary>
        /// Возвращает репозиторий типа T.
        /// </summary>
        /// <typeparam name="T">
        /// Тип репозитория
        /// </typeparam>
        /// <returns>
        /// Новый или уже созданный репозиторий
        /// </returns>
        public virtual DataAccess<T> GetRepository<T>()
            where T : DbObject, new()
        {
            lock (this._repositoryList)
            {
                // сразу ищем в списке репозиториев
                DataAccess<T> repository = this._repositoryList.OfType<DataAccess<T>>().FirstOrDefault();
                if (repository != null)
                    return repository;

                repository = this.CreateRepository<T>();
                this._repositoryList.Add(repository);
                return repository;
            }
        }

        /// <summary>
        /// Фабричный метод для создания репозиторя Repository T
        /// создает новый репозиторий, выставляя ему this.DataManager
        /// (этот метод используется внутри GetRepository T)
        /// перекрывается на клиенте и сервере
        /// при добавлении нового типа репозитория, нужно обучить фабричый метод его создавать
        /// </summary>
        /// <typeparam name="T">Тип репозитория</typeparam>
        /// <returns>Новый или уже созданный репозиторий</returns>
        protected DataAccess<T> CreateRepository<T>() where T : DbObject, new()
        {
            var type = typeof(T);
            if (!_repositoryBuilders.ContainsKey(type))
                throw new ArgumentException(string.Format("Для типа: {0} не зарегистрирован строитель репозитория.", type.FullName));
            var builder = (Func<DataManager, DataAccess<T>>)_repositoryBuilders[type];
            return builder(this);
        }

        /// <summary>
        /// Регистрирует экземпляр репозитория
        /// </summary>
        /// <typeparam name="T">Тип репозитория</typeparam>
        /// <param name="dataAccess">Экземпляр репозитория</param>
        public virtual void RegisterRepository<T>(DataAccess<T> dataAccess)
            where T : DbObject
        {
            lock (this._repositoryList)
            {
                // проверяем, что такого репозитория еще нет
                DataAccess<T> repository = this._repositoryList.OfType<DataAccess<T>>().FirstOrDefault();
                if (repository != null)
                    throw new ArgumentException(string.Format("Репозиторий для типа [{0}] уже зарегистрирован.", typeof(T).FullName), "dataAccess");

                // регистрируем в списке репозиториев
                this._repositoryList.Add(dataAccess);
            }
        }

        /// <summary>
        /// Функции построения репозиториев
        /// </summary>
        private readonly Dictionary<Type, Delegate> _repositoryBuilders = new Dictionary<Type, Delegate>();

        /// <summary>
        /// Регистрирует функцию построения репозитория
        /// </summary>
        /// <typeparam name="T">Тип репозитория</typeparam>
        /// <param name="builder">Функция построеня</param>
        public void RegiserBuilder<T>(Func<DataManager, DataAccess<T>> builder)
            where T : DbObject
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (_repositoryBuilders.ContainsKey(typeof(T)))
                throw new ArgumentException(string.Format("Для типа {0} уже зарегистрирован строитель.", typeof(T).FullName));

            _repositoryBuilders.Add(typeof(T), builder);
        }
    }

    /// <summary>
    /// Менеджер данных с типизированным контекстом
    /// </summary>
    /// <typeparam name="TContext">Тип контекста</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Generic обертка")]
    public class DataManager<TContext> : DataManager
        where TContext : class
    {
        /// <summary>
        /// Менеджер данных с типизированным контекстом
        /// </summary>
        /// <param name="getContext">Функция получения контекста</param>
        public DataManager(Func<TContext> getContext)
            : base(() => getContext())
        {
        }

        /// <summary>
        /// Контекст (WcfClient или DB)
        /// </summary>
        /// <returns>
        /// Контекст доступа к данным
        /// </returns>
        public new TContext GetContext()
        {
            return (TContext)base.GetContext();
        }
    }
}