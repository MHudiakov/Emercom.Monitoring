// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Интерфейс шлюза таблицы с ключем произвольного типа
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
    /// Базовый класс шлюза таблицы
    /// </summary>
    /// <typeparam name="T">Тип записи</typeparam>
    public abstract class DataAccess<T>
        where T : DbObject
    {
        /// <summary>
        /// Вспомогательный класс для работы с DbObject
        /// </summary>
        protected DbObjectMetadata Metadata { get; private set; }

        /// <summary>
        /// Базовый класс шлюза таблицы
        /// </summary>
        protected DataAccess()
        {
            this.Metadata = new DbObjectMetadata(typeof(T));
        }

        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <returns>
        /// Список всех объектов в таблице
        /// </returns>
        public abstract List<T> GetAll();

        /// <summary>
        /// Получение количества объектов
        /// </summary>
        /// <returns>
        /// Количество объектов в БД
        /// </returns>
        public abstract long GetCount();

        /// <summary>
        /// Добавление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        protected abstract void AddOverride(T item);

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        protected abstract void EditOverride(T item);

        /// <summary>
        /// Выполнят удаление записей по совпдению указанных полей
        /// </summary>
        /// <param name="whereArgs">
        /// Ключи объекта
        /// </param>
        protected abstract void DeleteWhereOverride(Dictionary<string, object> whereArgs);

        /// <summary>
        /// Получение сиска объектов по набору ключей ключу
        /// </summary>
        /// <param name="whereArgs">
        /// Ключ выбора объектов
        /// </param>
        /// <returns>
        /// Список объектов подходящий под критерий
        /// </returns>
        protected abstract List<T> GetItemsWhereOverride(Dictionary<string, object> whereArgs);

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
        protected abstract List<T> GetPageOverride(int pageIndex, int pageSize);

        /// <summary>
        /// Добавление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            AddOverride(item);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        public void Edit(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (this.Metadata.Key == null)
                throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", typeof(T).FullName));

            this.EditOverride(item);
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
        public List<T> GetPage(int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
                throw new ArgumentException(@"Номер страницы не может быть отрицательным", "pageIndex");
            if (pageSize <= 0)
                throw new ArgumentException(@"Размер страницы должен быть положительным", "pageSize");
            if (this.Metadata.Key == null)
                throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", typeof(T).FullName));

            return this.GetPageOverride(pageIndex, pageSize);
        }

        /// <summary>
        /// Удаление объектов по условию
        /// </summary>
        /// <param name="whereArgs">
        /// Ключи объекта
        /// </param>
        public void DeleteWhere(Dictionary<string, object> whereArgs)
        {
            // выполняет проверку удаления по свойствам
            foreach (var pair in whereArgs)
                if (!this.Metadata.Properties.ContainsKey(pair.Key))
                    throw new ArgumentException(string.Format("Объект [{1}] не содержит свойство: {0}. Возможно, свойство не было отмечено атрибутом [DataMember].", pair.Key, typeof(T).FullName), "whereArgs");
            this.DeleteWhereOverride(whereArgs);
        }

        /// <summary>
        /// Удаление элемента по совпадению поля
        /// </summary>
        /// <typeparam name="TSelectorType">Тип селектора</typeparam>
        /// <param name="selector">Селектор поля</param>
        /// <param name="key">Значение поля</param>
        public void DeleteWhere<TSelectorType>(Expression<Func<T, TSelectorType>> selector, TSelectorType key)
        {
            if (selector == null)
                throw new ArgumentNullException("selector");

            var memberExpression = selector.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException(@"Селектор должен быть MemberExpression", "selector");

            var member = memberExpression.Member as PropertyInfo;

            if (member == null)
                throw new ArgumentException(@"Селектор должен возвращать свойство", "selector");

            if (!this.Metadata.Properties.ContainsKey(member.Name))
                throw new ArgumentException(string.Format("Селектор должен возвращать свойство класса [{0}]", typeof(T).FullName), "selector");

            this.DeleteWhere(new Dictionary<string, object> { { member.Name, key } });
        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <typeparam name="TKey">Тип ключа (для проверки типов во время компиляции)</typeparam>
        /// <param name="key">
        /// Ключ объекта
        /// </param>
        public void Delete<TKey>(TKey key)
        {
            if (this.Metadata.Key == null)
                throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", typeof(T).FullName));
            if (!this.Metadata.Key.PropertyInfo.PropertyType.IsInstanceOfType(key))
                throw new NotSupportedException(string.Format("Несоответствие типов ключа. Ожидался тип {0}", this.Metadata.Key.PropertyInfo.PropertyType.FullName));
            this.DeleteWhere(new Dictionary<string, object> { { this.Metadata.Key.PropertyInfo.Name, key } });
        }

        /// <summary>
        /// Удаляет объект
        /// </summary>
        /// <param name="item">Объект для удаления</param>
        public void Delete(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (this.Metadata.Key == null)
                throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", this.GetType().FullName));
            if (!item.IsKeysInitialised())
                throw new ArgumentException(@"Ключи объекта не инициализированы", "item");
            this.Delete(item.KeyValue);
        }

        /// <summary>
        /// Получение объекта по единственному ID
        /// </summary>
        /// <param name="key">
        /// Ключ объекта
        /// </param>
        /// <typeparam name="TKey">Тип ключа (для проверки типов во время компиляции)</typeparam>
        /// <returns>Объект</returns>
        public T Get<TKey>(TKey key)
        {
            if (this.Metadata.Key == null)
                throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", this.GetType().FullName));

            return this.GetItemsWhere(new Dictionary<string, object> { { this.Metadata.Key.PropertyInfo.Name, key } }).SingleOrDefault();
        }

        /// <summary>
        /// Получение объектов по ключу
        /// </summary>
        /// <typeparam name="TSelectorType">
        /// Тип ключа
        /// </typeparam>
        /// <param name="selector">
        /// Селектор ключа
        /// </param>
        /// <param name="key">
        /// Значение ключа
        /// </param>
        /// <returns>
        /// Список объектов соответсвуюущих ключу выбора
        /// </returns>
        public List<T> GetItemsWhere<TSelectorType>(Expression<Func<T, TSelectorType>> selector, TSelectorType key)
        {
            if (selector == null)
                throw new ArgumentNullException("selector");

            var memberExpression = selector.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException(@"Селектор должен быть MemberExpression", "selector");

            var member = memberExpression.Member as PropertyInfo;

            if (member == null)
                throw new ArgumentException(@"Селектор должен возвращать свойство", "selector");

            if (!this.Metadata.Properties.ContainsKey(member.Name))
                throw new ArgumentException(string.Format("Селектор должен возвращать свойство класса [{0}]", typeof(T).FullName), "selector");

            return this.GetItemsWhere(new Dictionary<string, object> { { member.Name, key } });
        }

        /// <summary>
        /// Получение сиска объектов по набору ключей ключу
        /// </summary>
        /// <param name="whereArgs">
        /// Ключ выбора объектов
        /// </param>
        /// <returns>
        /// Список объектов подходящий под критерий
        /// </returns>
        public List<T> GetItemsWhere(Dictionary<string, object> whereArgs)
        {
            // выполняет проверку удаления по свойствам
            foreach (var pair in whereArgs)
                if (!this.Metadata.Properties.ContainsKey(pair.Key))
                    throw new ArgumentException(string.Format("Объект [{1}] не содержит свойство: {0}. Возможно, свойство не было отмечено атрибутом [DataMember].", pair.Key, typeof(T).FullName), "whereArgs");
            return this.GetItemsWhereOverride(whereArgs);
        }
    }

    /// <summary>
    /// Базовый класс шлюза таблицы
    /// </summary>
    /// <typeparam name="T">Тип записи</typeparam>
    /// <typeparam name="TContext">Тип контекста</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Абстракция-обертка")]
    public abstract class DataAccess<T, TContext> : DataAccess<T>
        where T : DbObject
    {
        /// <summary>
        /// Функция получения ссылки на контекст доступа к данным
        /// </summary>
        private readonly Func<TContext> _getContext;

        /// <summary>
        /// Контекст доступа к БД
        /// </summary>
        protected virtual TContext Context
        {
            get { return this._getContext(); }
        }

        /// <summary>
        ///  Базовый класс шлюза таблицы
        /// </summary>
        /// <param name="getContext">Функция получения конекста доступа к БД</param>
        protected DataAccess(Func<TContext> getContext)
        {
            if (getContext == null)
                throw new ArgumentNullException("getContext");
            _getContext = getContext;
        }
    }
}