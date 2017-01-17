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
    public class ClientChangeManager
    {
        /// <summary>
        /// Список изменений
        /// </summary>
        private readonly Dictionary<string, SyncStrategy> _behaviors;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClientChangeManager"/>. 
        /// Создать объект описания изменений
        /// </summary>
        public ClientChangeManager()
        {
            _behaviors = new Dictionary<string, SyncStrategy>();
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
                if (!_behaviors.ContainsKey(change.TypeName))
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
                _behaviors[change.TypeName].AddItem(change);

            // Применяем операции редактирования
            var editList = changes.Where(n => n.ChangeType == ChangeTypeEnum.Edit).OrderBy(n => n.DateTime);
            foreach (var change in editList)
                _behaviors[change.TypeName].EditItem(change);

            // Применяем операции удаления
            var remove = changes.Where(n => n.ChangeType == ChangeTypeEnum.Delete).OrderBy(n => n.DateTime);
            foreach (var change in remove)
                _behaviors[change.TypeName].RemoveItem(change);
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

            if (_behaviors.ContainsKey(typeName))
                throw new Exception(string.Format("Невозможно зарегистрировать SyncStrategy, т.к. SyncStrategy с таким TypeName({0}) уже зарегистрирована ранее", typeName));

            _behaviors.Add(typeName, syncStrategy);
        }
    }
}