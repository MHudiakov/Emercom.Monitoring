// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChangeRegistrationManager.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// // <summary>
//   Перечень разрешений для потоков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Init.DbCore;
    using Init.DbCore.DataAccess;

    /// <summary>
    /// Менеджер рпегистрации изменений в шлюзах доступа к данным
    /// </summary>
    public class ChangeRegistrationManager
    {
        /// <summary>
        /// Список наблюдателей за шлюзами доступа к данным
        /// </summary>
        private readonly List<object> _observers = new List<object>();

        /// <summary>
        /// Справочник списка изменений
        /// </summary>
        protected readonly Dictionary<Guid, SafeTransferChangeList> _safeTransferDictionary;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ChangeRegistrationManager"/>. 
        /// Создать обект учета изменений
        /// </summary>
        public ChangeRegistrationManager()
        {
            _safeTransferDictionary = new Dictionary<Guid, SafeTransferChangeList>();
        }

        /// <summary>
        /// Зарегистрировать шлюз доступа к данным для регистрации изменений
        /// </summary>
        /// <typeparam name="T">
        /// Тип шлюза
        /// </typeparam>
        /// <param name="itemTypeName">
        /// Ключ-название изменения
        /// </param>
        /// <param name="dataAccess">
        /// Шлюз доступа к данным
        /// </param>
        public void RegisterObservable<T>(string itemTypeName, IObservableDataAccess<T> dataAccess) where T : DbObject
        {
            _observers.Add(new Observer<T>(itemTypeName, dataAccess, this));
        }

        /// <summary>
        /// Добавить информацию об изменени объекта
        /// </summary>
        /// <param name="change">
        /// Информация об изменениях
        /// </param>
        public virtual void AddChange(Change change)
        {
            lock (this)
            {
                foreach (var changeList in _safeTransferDictionary)
                    changeList.Value.ChangeList.Add(change);
            }
        }

        /// <summary>
        /// Удаляет устревшие списки гарантированной доставки
        /// </summary>
        /// <param name="timeout">Таймаут устаревания списка</param>
        protected void RemoveOldSafeTransferList(TimeSpan timeout)
        {
            List<KeyValuePair<Guid, SafeTransferChangeList>> lockedList;
            lock (this)
            {
                lockedList = this._safeTransferDictionary.ToList();
            }

            var currentDate = DateTime.Now;
            lock (this)
            {
                foreach (var pair in lockedList)
                {
                    if ((currentDate - pair.Value.LastActivityDate) > timeout)
                        this._safeTransferDictionary.Remove(pair.Key);
                }
            }
        }

        /// <summary>
        /// Возвращает список изменений по идентификатору или создает новый, если его еще небыло
        /// </summary>
        /// <param name="id">Идентификатор списка доставки</param>
        /// <returns>Объект который содержит список изменений принадлежащий одному клиенту</returns>
        public SafeTransferChangeList GetSafeTransferChangeList(Guid id)
        {
            lock (this)
            {
                if (!this._safeTransferDictionary.ContainsKey(id))
                {
                    var item = new SafeTransferChangeList();
                    this._safeTransferDictionary.Add(id, item);
                    return item;
                }

                return this._safeTransferDictionary[id];
            }
        }
    }
}