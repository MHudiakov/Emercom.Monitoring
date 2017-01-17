// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SyncTask.cs" company="ИНИТ-центр">
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
    using System.Threading;

    using Init.Tools;

    /// <summary>
    /// Задача синхронизации изменений
    /// </summary>
    public class SyncTask
    {
        /// <summary>
        /// Интервал синхранизации
        /// </summary>
        private readonly TimeSpan _syncInterval;

        /// <summary>
        /// Объект для сохранения полученных изменений
        /// </summary>
        private readonly ChangeApplyManager _changeApplyManager;

        /// <summary>
        /// Метод для получения Change-й за период
        /// </summary>
        private readonly Func<List<Change>> _getChangeList;
        
        /// <summary>
        /// Поток синхронизации
        /// </summary>
        private Thread _synchroniser;

        /// <summary>
        /// Обертка системы логирования
        /// </summary>
        public Loger Logger { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SyncTask"/>. 
        /// Задача синхронизации изменений
        /// </summary>
        /// <param name="syncInterval">
        /// Интервал синхронизации
        /// </param>
        /// <param name="changeApplyManager">
        /// Объект сохраниния полученных изменений
        /// </param>
        /// <param name="getChangeList">
        /// Метод получения Change-й за интервал времени
        /// </param>
        public SyncTask(TimeSpan syncInterval, ChangeApplyManager changeApplyManager, Func<List<Change>> getChangeList)
        {
            if (syncInterval == null)
                throw new ArgumentNullException("syncInterval");

            if (changeApplyManager == null)
                throw new ArgumentNullException("changeApplyManager");

            if (getChangeList == null)
                throw new ArgumentNullException("getChangeList");

            _syncInterval = syncInterval;
            this._changeApplyManager = changeApplyManager;
            _getChangeList = getChangeList;
            Logger = new Loger();
        }

        /// <summary>
        /// Запускаем синхронизацию в другом потоке
        /// </summary>
        public void Start()
        {
            this.Stop();
            _synchroniser = new Thread(Sync) { IsBackground = true };
            _synchroniser.Start();
        }

        /// <summary>
        /// Остановить поток синхронизации
        /// </summary>
        public void Stop()
        {
            if (_synchroniser != null)
                _synchroniser.Abort();
        }

        /// <summary>
        /// Дата последней попытки синхронизации
        /// </summary>
        private DateTime _lastSyncDate = DateTime.MinValue;

        /// <summary>
        /// Метод синхронизации
        /// </summary>
        private void Sync()
        {
            while (true)
            {
                try
                {
                    var currentDate = DateTime.Now;
                    if (currentDate < _lastSyncDate + _syncInterval)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    _lastSyncDate = currentDate;

                    var changeList = _getChangeList();

                    if (changeList.Count == 0)
                        continue;

                    DoSync(changeList);

                    Logger.LogMsg("Cинхронизация выполнена.");
                }
                catch (ThreadAbortException abortException)
                {
                    Logger.LogException((Exception)abortException.ExceptionState);
                    throw;
                }
                catch (Exception ex)
                {
                    Logger.LogException(new Exception("Ошибка синхронизации.", ex));
                }
            }
        }

        /// <summary>
        /// Выполняет синхронизации с указыным списком изменений
        /// </summary>
        /// <param name="changeList">
        /// The change List.
        /// </param>
        protected virtual void DoSync(List<Change> changeList)
        {
            this._changeApplyManager.Sync(changeList);
        }
    }
}