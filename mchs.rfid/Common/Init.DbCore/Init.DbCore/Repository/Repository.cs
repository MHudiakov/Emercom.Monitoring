// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Базовый класс репозиторя
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Repository
{
    using System;
    using System.Collections.Generic;

    using Init.DbCore.DataAccess;
    using Init.DbCore.Processing;

    /// <summary>
    /// Базовый класс репозиторя
    /// </summary>
    /// <typeparam name="T">Тип репозитория</typeparam>
    public class Repository<T> : DataAccess<T>, IObservableDataAccess<T>
        where T : DbObject
    {
        /// <summary>
        /// Шлюз доступа к данным
        /// </summary>
        private readonly DataAccess<T> _dataAccess;

        /// <summary>
        /// Менеджер данных
        /// </summary>
        private readonly DataManager _dataManager;

        /// <summary>
        /// Менеджер данных
        /// </summary>
        public virtual DataManager DataManager
        {
            get { return this._dataManager; }
        }

        /// <summary>
        /// Шлюз таблицы
        /// </summary>
        protected virtual DataAccess<T> DataAccess
        {
            get { return this._dataAccess; }
        }

        /// <summary>
        /// Базовый репозиторий
        /// </summary>
        /// <param name="dataAccess">Объект доступа к данным</param>
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
        /// Получение всех объектов
        /// </summary>
        /// <returns>
        /// Список всех объектов в таблице
        /// </returns>
        public override List<T> GetAll()
        {
            return this._dataAccess.GetAll();
        }

        /// <summary>
        /// Получение количества объектов
        /// </summary>
        /// <returns>
        /// Количество объектов в БД
        /// </returns>
        public override long GetCount()
        {
            return this._dataAccess.GetCount();
        }

        /// <summary>
        /// Добавление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void AddOverride(T item)
        {
            this.OnBeforeAdd(item);
            ModelProcessor<T>.Active.Process(item);
            this._dataAccess.Add(item);
            this.OnAfterAdd(item);
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void EditOverride(T item)
        {
            this.OnBeforeEdit(item);
            ModelProcessor<T>.Active.Process(item);
            this._dataAccess.Edit(item);
            this.OnAfterEdit(item);
        }

        /// <summary>
        /// Выполнят удаление записей по совпдению указанных полей
        /// </summary>
        /// <param name="whereArgs">
        /// Ключи объекта
        /// </param>
        protected override void DeleteWhereOverride(Dictionary<string, object> whereArgs)
        {
            this.OnBeforeDelete(whereArgs);
            this._dataAccess.DeleteWhere(whereArgs);
            this.OnAfterDelete(whereArgs);
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
        protected override List<T> GetItemsWhereOverride(Dictionary<string, object> whereArgs)
        {
            return this._dataAccess.GetItemsWhere(whereArgs);
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
        /// Список объектов начиная с idFrom длинной не более pageSize
        /// </returns>
        protected override List<T> GetPageOverride(int pageIndex, int pageSize)
        {
            return this._dataAccess.GetPage(pageIndex, pageSize);
        }

        #region OnAdd
        /// <summary>
        /// Генерирует события по завершении операции добавления
        /// </summary>
        /// <param name="item">
        /// Добавленный элемент
        /// </param>
        protected virtual void OnAfterAdd(T item)
        {
            Action<T> handler = this.AfterAdd;
            if (handler != null) handler(item);
        }

        /// <summary>
        /// Вызывается после добавлении элемента
        /// </summary>
        public event Action<T> AfterAdd;

        /// <summary>
        /// Вызывается перед добавлением элемента
        /// </summary>
        public event Action<T> BeforeAdd;

        /// <summary>
        /// Генерирует событие перед операцией добавления
        /// </summary>
        /// <param name="item">
        /// Добавляемый элемент
        /// </param>
        protected virtual void OnBeforeAdd(T item)
        {
            Action<T> handler = this.BeforeAdd;
            if (handler != null) handler(item);
        }
        #endregion

        #region OnDelete
        /// <summary>
        /// Генерирует событие после удаления элемента
        /// </summary>
        /// <param name="keys">
        /// Идентификатор удаленного элемента
        /// </param>
        protected virtual void OnAfterDelete(Dictionary<string, object> keys)
        {
            Action<Dictionary<string, object>> handler = this.AfterDelete;
            if (handler != null) handler(keys);
        }

        /// <summary>
        /// Вызывается после удаления элемента
        /// </summary>
        public event Action<Dictionary<string, object>> AfterDelete;

        /// <summary>
        /// Вызывается перед удалением элемента
        /// </summary>
        public event Action<Dictionary<string, object>> BeforeDelete;

        /// <summary>
        /// Генерирует событие перед удалением элемента
        /// </summary>
        /// <param name="whereArgs">
        /// Идентификатор удаляемого элемента
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
        /// Генерирует событие после редактирования элемента
        /// </summary>
        /// <param name="item">
        /// Редактируемый элемент
        /// </param>
        protected virtual void OnAfterEdit(T item)
        {
            Action<T> handler = this.AfterEdit;
            if (handler != null) handler(item);
        }

        /// <summary>
        /// Вызывается после редактирования объекта
        /// </summary>
        public event Action<T> AfterEdit;

        /// <summary>
        /// Вызывается перед редастированием элемента
        /// </summary>
        public event Action<T> BeforeEdit;

        /// <summary>
        /// Генерирует событие перед редактированием элемента
        /// </summary>
        /// <param name="item">
        /// Редактируемый элемент
        /// </param>
        protected virtual void OnBeforeEdit(T item)
        {
            Action<T> handler = this.BeforeEdit;
            if (handler != null) handler(item);
        }
        #endregion
    }
}