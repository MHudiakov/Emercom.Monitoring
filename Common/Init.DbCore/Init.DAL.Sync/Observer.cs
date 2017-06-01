// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Observer.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// // <summary>
//   Перечень разрешений для потоков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync
{
    using System;
    using System.Collections.Generic;

    using Init.DAL.Sync.Common;
    using Init.DbCore;
    using Init.DbCore.DataAccess;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Объект регистрации изменений
    /// </summary>
    /// <typeparam name="T">
    /// Тип наблюдаемого объекта
    /// </typeparam>
    internal class Observer<T>
        where T : DbObject
    {
        /// <summary>
        /// Метаданыне объекта T
        /// </summary>
        private readonly DbObjectMetadata _metadata = new DbObjectMetadata(typeof(T));

        /// <summary>
        /// Наименование типа, к объекту которого было применено изменение
        /// </summary>
        public string ItemTypeName { get; private set; }

        /// <summary>
        /// Объект учёта изменений
        /// </summary>
        public ChangeRegistrationManager ChangeRegistrationManager { get; private set; }

        /// <summary>
        /// Обект наблюдаемого репозитория
        /// </summary>
        public IObservableDataAccess<T> DataAccess { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Observer{T}"/>. 
        /// Создать объект регистрации изменений
        /// </summary>
        /// <param name="itemTypeName">
        /// Наименование типа
        /// </param>
        /// <param name="dataAccess">
        /// Наблюдаемый DataAccess
        /// </param>
        /// <param name="changeRegistrationManager">
        /// Объект учёта изменений
        /// </param>
        public Observer(
            string itemTypeName, IObservableDataAccess<T> dataAccess, ChangeRegistrationManager changeRegistrationManager)
        {
            if (string.IsNullOrEmpty(itemTypeName))
                throw new ArgumentNullException("itemTypeName");

            if (dataAccess == null)
                throw new ArgumentNullException("dataAccess");

            if (changeRegistrationManager == null)
                throw new ArgumentNullException("changeRegistrationManager");

            ItemTypeName = itemTypeName;

            DataAccess = dataAccess;
            this.ChangeRegistrationManager = changeRegistrationManager;

            dataAccess.AfterAdd += this.OnAdd;

            dataAccess.AfterEdit += this.OnEdit;

            dataAccess.AfterDelete += this.OnDelete;
        }

        /// <summary>
        /// Регистрация записи о добавлении
        /// </summary>
        /// <param name="item">
        /// Добавляемый объект
        /// </param>
        private void OnAdd(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (this._metadata.Key == null)
                throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", typeof(T).FullName));
            var change = new Change(ChangeTypeEnum.Add, ItemTypeName, DateTime.Now, new Dictionary<string, object> { { _metadata.Key.PropertyInfo.Name, item.KeyValue } }, item);
            this.ChangeRegistrationManager.AddChange(change);
        }

        /// <summary>
        /// Регистрация записи об изменении
        /// </summary>
        /// <param name="item">
        /// Изменяемый объект
        /// </param>
        private void OnEdit(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            var change = new Change(ChangeTypeEnum.Edit, ItemTypeName, DateTime.Now, item.GetProperties(), item);
            this.ChangeRegistrationManager.AddChange(change);
        }

        /// <summary>
        /// Регистрация записи об удалении
        /// </summary>
        /// <param name="key">
        /// Ключ удаляемого объекта
        /// </param>
        private void OnDelete(Dictionary<string, object> key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (this._metadata.Key == null)
                throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", typeof(T).FullName));
            var change = new Change(ChangeTypeEnum.Delete, ItemTypeName, DateTime.Now, key, null);
            this.ChangeRegistrationManager.AddChange(change);
        }
    }
}