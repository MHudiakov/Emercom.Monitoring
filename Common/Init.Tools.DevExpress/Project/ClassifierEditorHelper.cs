// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClassifierEditorHelper.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Расширение для элементов управления, отображающих справочники
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using global::DevExpress.Utils;
    using global::DevExpress.XtraEditors.Controls;

    /// <summary>
    /// Вспомогательный класс для загрузки данных в комбо бокс контролы
    /// </summary>
    /// <typeparam name="T">
    /// Тип классификатора
    /// </typeparam>
    public class ClassifierEditorHelper<T>
        where T : class, new()
    {
        /// <summary>
        /// Элемент управления
        /// </summary>
        private readonly global::DevExpress.XtraEditors.PopupBaseEdit _edit;

        /// <summary>
        /// Метод получения списка элементов
        /// </summary>
        private readonly Func<List<T>> _loadItems;

        /// <summary>
        /// Метод сохранения элемента
        /// </summary>
        private readonly Action<T> _saveItem;

        /// <summary>
        /// Метод редактирования элемента
        /// </summary>
        private readonly Func<T, bool> _editItem;

        /// <summary>
        /// Метод создания элемента
        /// </summary>
        private readonly Func<T> _createItem;

        /// <summary>
        /// Метод установки отображаемой коллекции элементов контролу
        /// </summary>
        private readonly Action<List<T>> _setItemsCollection;

        /// <summary>
        /// Добавляет элементу механизм Inline добавления элемента
        /// </summary>
        /// <typeparam name="T">Тип справочника</typeparam>
        /// <param name="edit">Элемент управления</param>
        /// <param name="loadItems">Метод получения списка элементов</param>
        /// <param name="saveItem">Метод сохранения элемента</param>
        /// <param name="editItem">Метод редактирования элемента</param>
        /// <param name="createItem">Метод создания элемента</param>
        /// <param name="setItemsCollection">Метод установки отображаемой коллекции элементов контролу</param>
        public ClassifierEditorHelper(
            global::DevExpress.XtraEditors.PopupBaseEdit edit,
            Func<List<T>> loadItems,
            Action<T> saveItem,
            Func<T, bool> editItem,
            Func<T> createItem,
            Action<List<T>> setItemsCollection)
        {
            if (edit == null)
                throw new ArgumentNullException("edit");
            if (loadItems == null)
                throw new ArgumentNullException("loadItems");
            if (saveItem == null)
                throw new ArgumentNullException("saveItem");
            if (editItem == null)
                throw new ArgumentNullException("editItem");
            if (createItem == null)
                throw new ArgumentNullException("createItem");
            if (setItemsCollection == null)
                throw new ArgumentNullException("setItemsCollection");

            _edit = edit;
            _loadItems = loadItems;
            _saveItem = saveItem;
            _editItem = editItem;
            _createItem = createItem;
            _setItemsCollection = setItemsCollection;

            var button = new EditorButton(ButtonPredefines.Plus, "Добавить элемен") { Shortcut = new KeyShortcut(Keys.Insert) };
            edit.Properties.Buttons.Add(button);
            edit.EditValueChanged += (sender, args) => this.OnEditValueChanged(new EventArgs());

            edit.ButtonClick += (sender, args) =>
            {
                if (args.Button == button)
                    this.Execute();
            };

            SetTextExitStyle();
        }

        /// <summary>
        /// Событие после обновления источника данных
        /// </summary>
        public event EventHandler<DataSourceUpdatedEventArgs<T>> DataSourceUpdated;

        /// <summary>
        /// Генерирует событие после обновления источника данных
        /// </summary>
        /// <param name="args">Параметры события</param>
        protected virtual void OnDataSourceUpdated(DataSourceUpdatedEventArgs<T> args)
        {
            var handler = this.DataSourceUpdated;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Событие смены редактируемого значения
        /// </summary>
        public event EventHandler<EventArgs> EditValueChanged;

        /// <summary>
        /// Генерирует событие смены редактирвумого значения
        /// </summary>
        /// <param name="args">Параметры события</param>
        protected virtual void OnEditValueChanged(EventArgs args)
        {
            var handler = this.EditValueChanged;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Устанавливает стиль отображения редактрора
        /// </summary>
        /// <param name="style">
        /// Стиль отображения <see cref="TextEditStyles"/> 
        /// <remarks>
        /// По умолчанию TextEditStyles.DisableTextEditor
        /// </remarks>
        /// </param>
        /// <returns>
        /// Helper
        /// </returns>
        public ClassifierEditorHelper<T> SetTextExitStyle(TextEditStyles style = TextEditStyles.DisableTextEditor)
        {
            _edit.Properties.TextEditStyle = style;
            return this;
        }

        /// <summary>
        /// Устанавливает обработчик события смены значения
        /// </summary>
        /// <param name="handler">Обработчик</param>
        /// <returns>Helper</returns>
        public ClassifierEditorHelper<T> SetEditValueChangedHandler(EventHandler<EventArgs> handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");
            EditValueChanged += handler;
            return this;
        }

        /// <summary>
        /// Устанавливает обработчик события обновления источника данных
        /// </summary>
        /// <param name="handler">Обработчик</param>
        /// <returns>Helper</returns>
        public ClassifierEditorHelper<T> SetDataSourceUpdatedEventHandler(
            EventHandler<DataSourceUpdatedEventArgs<T>> handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");
            DataSourceUpdated += handler;
            return this;
        }

        /// <summary>
        /// Открывает форму добавления элемента справочника
        /// </summary>
        private void Execute()
        {
            T item = _createItem();
            if (_editItem(item))
            {
                _saveItem(item);
                var items = this._loadItems();

                T currentItem = default(T);

                // ищем элемент компаратором
                if (item is IEquatable<T>)
                    currentItem = items.SingleOrDefault(e => ((IEquatable<T>)item).Equals(e));

                // ищем элемент по ссылке
                if (currentItem == null)
                    currentItem = items.SingleOrDefault(e => ReferenceEquals(e, item));

                // если после добавления не удалось найти элемент в коллекции, добавляем в коллекцию сами
                if (currentItem == null)
                {
                    currentItem = item;
                    items.Add(item);
                }

                this.SetDataSource(items);
                _edit.EditValue = currentItem;
            }
        }

        /// <summary>
        /// Выполняет загрузку данных
        /// </summary>
        public void LoadData()
        {
            var items = this._loadItems();
            this.SetDataSource(items);
        }

        /// <summary>
        /// Устанвливает источник данных
        /// </summary>
        /// <param name="items">Коллекция элементов, используемая в качестве нового источника даыннх</param>
        private void SetDataSource(List<T> items)
        {
            this._setItemsCollection(items);
            this._edit.Refresh();
            this.OnDataSourceUpdated(new DataSourceUpdatedEventArgs<T>(items));
        }
    }
}
