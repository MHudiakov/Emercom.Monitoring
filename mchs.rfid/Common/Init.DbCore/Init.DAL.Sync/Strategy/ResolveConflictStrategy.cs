// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResolveConflictStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// // <summary>
//   Перечень разрешений для потоков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.Strategy
{
    using System;

    using Init.DAL.Sync.Common;
    using Init.DbCore;
    using Init.DbCore.DataAccess;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Класс разрешающий конфликты при применении изменений
    /// </summary>
    /// <typeparam name="T">
    /// Тик шлюза записи
    /// </typeparam>
    public class ResolveConflictStrategy<T> : ProxyStrategy
        where T : DbObject
    {
        /// <summary>
        /// Шлюз к таблице
        /// </summary>
        private readonly DataAccess<T> _dataAccess;

        /// <summary>
        /// Метеданные объекта T
        /// </summary>
        private readonly DbObjectMetadata _metadata = new DbObjectMetadata(typeof(T));

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ResolveConflictStrategy{T}"/>. 
        /// Создать Объект разрешающий конфликты при применении изменений
        /// </summary>
        /// <param name="dataAccess">
        /// Интерфейс шлюза таблицы
        /// </param>
        public ResolveConflictStrategy(DataAccess<T> dataAccess)
            : this(dataAccess, null)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ResolveConflictStrategy{T}"/>. 
        /// Создать Объект разрешающий конфликты при применении изменений
        /// </summary>
        /// <param name="dataAccess">
        /// Интерфейс шлюза таблицы
        /// </param>
        /// <param name="syncStrategy">
        /// Объект применяющий изменения в модели
        /// </param>
        public ResolveConflictStrategy(DataAccess<T> dataAccess, SyncStrategy syncStrategy)
            : base(syncStrategy)
        {
            if (dataAccess == null)
                throw new ArgumentNullException("dataAccess");

            if (syncStrategy == null)
                throw new ArgumentNullException("syncStrategy");

            this._dataAccess = dataAccess;
        }

        /// <summary>
        /// Разрешение конфликтной ситуации при добавлении
        /// </summary>
        /// <param name="change">
        /// Информация об изменении
        /// </param>
        protected override void AddItemOverride(Change change)
        {
            try
            {
                object keyValue = change.GetFilterProps()[_metadata.Key.PropertyInfo.Name];
                var obj = this._dataAccess.Get(keyValue);

                if (obj != null)
                {
                    var newChange = new Change(ChangeTypeEnum.Edit, change.TypeName, change.DateTime, change.GetFilterProps(), change.GetItem<T>());
                    this.EditItem(newChange);
                }
                else
                    base.AddItemOverride(change);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ошибка в разрешении конфликта при добавлении с объектом типа: {0}", change.TypeName), ex);
            }
        }
    }
}