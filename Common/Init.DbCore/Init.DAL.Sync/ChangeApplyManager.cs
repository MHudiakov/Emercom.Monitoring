// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientChangeManager.cs" company="ИНИТ-центр">
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

    using Init.DAL.Sync.Common;
    using Init.DAL.Sync.Strategy;

    /// <summary>
    /// Объект применения изменений к структуре навигационных свойств и кешей менеджера данных
    /// </summary>
    public class ChangeApplyManager
    {
        /// <summary>
        /// Список изменений
        /// </summary>
        private readonly Dictionary<string, SyncStrategy> _strategies;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ChangeApplyManager"/>. 
        /// Создать объект описания изменений
        /// </summary>
        public ChangeApplyManager()
        {
            this._strategies = new Dictionary<string, SyncStrategy>();
        }

        /// <summary>
        /// Применяет список изменений к кешу менеджера данных
        /// </summary>
        /// <param name="changes">
        /// Список изменений
        /// </param>
        public void Sync(List<Change> changes)
        {
            if (changes == null)
                throw new ArgumentNullException("changes");

            foreach (var change in changes)
            {
                if (!this._strategies.ContainsKey(change.TypeName))
                {
                    throw new Exception(
                        string.Format(
                            "Невозможно обработать операции для обектов: {0}, т.к. для него не зарегистрирован behavior в ClientChaingeManager-е",
                            change.TypeName));
                }
            }

            // Применяем операции добавления
            var addList = changes.Where(n => n.ChangeType == ChangeTypeEnum.Add).OrderBy(n => n.DateTime);
            foreach (var change in addList)
                this._strategies[change.TypeName].AddItem(change);

            // Применяем операции редактирования
            var editList = changes.Where(n => n.ChangeType == ChangeTypeEnum.Edit).OrderBy(n => n.DateTime);
            foreach (var change in editList)
                this._strategies[change.TypeName].EditItem(change);

            // Применяем операции удаления
            var remove = changes.Where(n => n.ChangeType == ChangeTypeEnum.Delete).OrderBy(n => n.DateTime);
            foreach (var change in remove)
                this._strategies[change.TypeName].RemoveItem(change);
        }

        /// <summary>
        /// Регистрирует стратегию исправления кеша. 
        /// Указанная стратегия будет использоваться для применения к кешу объктов <see cref="Change"/>
        /// </summary>
        /// <param name="syncStrategy">
        /// Обект применения изменений
        /// </param>
        /// <param name="typeName">
        /// Имя типа для которого будет применяться данная стратегия
        /// </param>
        public void RegisterSyncStrategy(SyncStrategy syncStrategy, string typeName)
        {
            if (syncStrategy == null)
                throw new ArgumentNullException("syncStrategy");

            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentNullException("typeName");

            if (this._strategies.ContainsKey(typeName))
                throw new Exception(string.Format("Невозможно зарегистрировать SyncStrategy, т.к. SyncStrategy с таким TypeName({0}) уже зарегистрирована ранее", typeName));

            this._strategies.Add(typeName, syncStrategy);
        }
    }
}