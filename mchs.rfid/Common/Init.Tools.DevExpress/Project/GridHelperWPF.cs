// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridHelperWPF.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Класс помощник для GridControlWPF
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.ServiceModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using global::DevExpress.Xpf.Grid;

    using Init.Tools;
    using Init.Tools.UI;

    /// <summary>
    /// Класс помощник для GridControlWPF
    /// </summary>
    /// <typeparam name="TEntity">Объект с публичным конструктором</typeparam>
    public class GridHelperWPF<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Закэшировнная ссылка на грид
        /// </summary>
        private readonly GridControl _grid;

        /// <summary>
        /// Закэшировнная ссылка на кнопку добавления
        /// </summary>
        private readonly Button _addButton;

        /// <summary>
        /// Закэшировнная ссылка на кнопку редактирования
        /// </summary>
        private readonly Button _editButton;

        /// <summary>
        /// Закэшировнная ссылка на кнопку удаления
        /// </summary>
        private readonly Button _deleteButton;

        /// <summary>
        /// Стратегия выполнения операции для GridHelper
        /// </summary>
        private readonly GhStrategy<TEntity> _strategy;

        /// <summary>
        /// Закешированная ссылка на метод формы редактирования с ссылкой на редактируемый объект
        /// </summary>
        private readonly Func<TEntity, bool> _executerVoid;

        /// <summary>
        /// Ссылка на графическое предтавление грида
        /// </summary>
        private readonly GridDataViewBase _view;

        /// <summary>
        /// Событие смены выделенного элемента.
        /// Указывает с какого объекта на какой переключились
        /// </summary>
        public event Action<TEntity, TEntity> SelectedEventChanged;

        /// <summary>
        /// Генерирует событие <example>SelectedEventChanged</example>
        /// </summary>
        /// <param name="from">
        /// Предыдущий элемент
        /// </param>
        /// <param name="to">
        /// Следуюущий элемент
        /// </param>
        protected virtual void OnSelectedEventChanged(TEntity from, TEntity to)
        {
            Action<TEntity, TEntity> handler = this.SelectedEventChanged;
            if (handler != null)
                handler(from, to);
        }

        /// <summary>
        /// Выделенный элемент грида
        /// </summary>
        public TEntity SelectedItem
        {
            get
            {
                return this._view.Dispatcher.Invoke(new Func<TEntity>(() => this._view.FocusedRow as TEntity)) as TEntity;
            }

            set
            {
                if (value == null)
                    return;

                this._view.Dispatcher.Invoke(
                    new Action(() =>
                    {
                        var items = this._grid.ItemsSource as IList<TEntity>;
                        if (items != null)
                        {
                            var selItem = items.SingleOrDefault(value.Equals);
                            this._view.FocusedRow = selItem;
                        }
                    }));
            }
        }

        /// <summary>
        /// Добавление к гриду функционала по добавлению, редактированию и удалению объектов
        /// </summary>
        /// <param name="grid">Грид</param>
        /// <param name="addButton">Кнопка "Добавить"</param>
        /// <param name="editButton">Кнопка "Изменить"</param>
        /// <param name="deleteButton">Кнопка "Удалить"</param>
        /// <param name="executerVoid">Метод формы редактирования с ссылкой на редактируемый объект</param>
        /// <param name="strategy">Стратегия работы с объектом</param>
        public GridHelperWPF(
            GridControl grid,
            Button addButton,
            Button editButton,
            Button deleteButton,
            Func<TEntity, bool> executerVoid,
            GhStrategy<TEntity> strategy)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");
            this._view = grid.View as GridDataViewBase;

            if (this._view == null)
                throw new InvalidCastException("GridHelper работает только с treeList.View типа GridDataViewBase");
            if (strategy == null)
                throw new ArgumentNullException("strategy");
            if (executerVoid == null)
                throw new ArgumentNullException("executerVoid");

            this._grid = grid;
            this._addButton = addButton;
            this._editButton = editButton;
            this._deleteButton = deleteButton;
            this._strategy = strategy;
            this._executerVoid = executerVoid;

            // привязка к событиям
            this._view.PreviewKeyDown += this.ViewPreviewKeyDown;
            this._view.MouseDoubleClick += this.ViewMouseDoubleClick;
            this._view.FocusedRowChanged += (sender, args) => this.OnSelectedEventChanged(args.OldRow as TEntity, args.NewRow as TEntity);

            if (addButton != null)
                addButton.Click += this.AddButtonClick;
            if (editButton != null)
                editButton.Click += this.EditButtonClick;
            if (deleteButton != null)
                deleteButton.Click += this.DeleteButtonClick;
        }

        #region Action
        /// <summary>
        /// Обновление данных
        /// </summary>
        public void AsyncUpdateData()
        {
            this._grid.IsEnabled = false;

            if (this._addButton != null)
                this._addButton.IsEnabled = false;
            if (this._editButton != null)
                this._editButton.IsEnabled = false;
            if (this._deleteButton != null)
                this._deleteButton.IsEnabled = false;

            try
            {
                var worker = new Thread(this.UpdateData) { IsBackground = true };
                worker.Start();
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception(string.Format("Ошибка загрузки данных в GridHelperWPF<{0}>", typeof(TEntity).Name), ex));
                this.UnlockInterface();
            }
        }

        /// <summary>
        /// Метод обновления данных
        /// </summary>
        private void UpdateData()
        {
            try
            {
                var items = this._strategy.GetList();
                this._grid.Dispatcher.Invoke(new Action(() => this._grid.ItemsSource = items));
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception(string.Format("Ошибка асинхронной загрузки данных в GridHelperWPF<{0}>", typeof(TEntity).Name), ex));
            }
            finally
            {
                this._grid.Dispatcher.Invoke(new Action(this.UnlockInterface));
            }
        }

        /// <summary>
        /// Разблокировка интерфейса
        /// </summary>
        private void UnlockInterface()
        {
            this._grid.IsEnabled = true;

            if (this._addButton != null)
                this._addButton.IsEnabled = true;
            if (this._editButton != null)
                this._editButton.IsEnabled = true;
            if (this._deleteButton != null)
                this._deleteButton.IsEnabled = true;
        }

        /// <summary>
        /// Добавление записи
        /// </summary>
        private void Add()
        {
            TEntity item;
            try
            {
                item = this._strategy.CreateItem();
            }
            catch (Exception ex)
            {
                xMsg.Error(ex);
                return;
            }

            try
            {
                if (this._executerVoid(item))
                {
                    // добавление записи в репозиторий
                    this._strategy.Add(item);

                    var list = this._grid.ItemsSource as List<TEntity>;
                    if (list != null)
                    {
                        // добавление записи к гриду
                        if (!list.Contains(item))
                            list.Add(item);
                        this.UpdateData();
                        this._grid.RefreshData();

                        var items = this._grid.ItemsSource as IList<TEntity>;
                        if (items == null)
                            return;

                        // фокусировка на добавленной записи
                        this._view.FocusedRow = items.SingleOrDefault(item.Equals);
                    }
                }
            }
            catch (FaultException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                var err = new Exception(string.Format(@"Ошибка добавления в  таблицу {0}", typeof(TEntity).FullName), ex);
                Log.AddException(err);
                xMsg.Error(err);
            }
        }

        /// <summary>
        /// Изменение записи
        /// </summary>
        private void Edit()
        {
            var item = this.SelectedItem;
            if (item == null)
            {
                xMsg.MsgEmptyEditData();
                return;
            }

            try
            {
                if (this._executerVoid(item))
                {
                    // изменение записи в репозитории
                    this._strategy.Edit(item);

                    this.UpdateData();

                    this._grid.RefreshData();

                    var items = this._grid.ItemsSource as IList<TEntity>;
                    if (items == null)
                        return;

                    // фокусировка на добавленной записи
                    this._view.FocusedRow = items.SingleOrDefault(item.Equals);
                }
            }
            catch (FaultException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                var err = new Exception(string.Format(@"Ошибка редактирования таблицы {0}", typeof(TEntity).Name), ex);
                Log.AddException(err);
                xMsg.Error(err);
            }
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        private void Delete()
        {
            var item = this.SelectedItem;
            if (item == null)
            {
                xMsg.MsgEmptyDeleteData();
                return;
            }

            if (!xMsg.MsgWithConfirmDelete())
                return;

            try
            {
                // удаление записи из репозитория
                this._strategy.Delete(item);
                var list = this._grid.ItemsSource as List<TEntity>;
                if (list != null)
                {
                    var index = list.IndexOf(item);

                    // удаление записи из грида
                    list.Remove(item);
                    if (index >= list.Count)
                        index = list.Count - 1;

                    this.UpdateData();
                    this._grid.RefreshData();

                    this._view.FocusedRowHandle = this._grid.GetRowHandleByListIndex(index);
                }
            }
            catch (FaultException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Log.AddException(ex);
                xMsg.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                var err = new Exception(string.Format(@"Ошибка удаления из таблицы {0}", typeof(TEntity).Name), ex);
                Log.AddException(err);
                xMsg.Error(err);
            }
        }
        #endregion

        #region Events

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Стандартный обработчик.")]
        private void ViewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Edit();
        }

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Стандартный обработчик.")]
        private void ViewPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && this._deleteButton != null)
                this.Delete();
            if (e.Key == Key.F2 && this._editButton != null)
                this.Edit();
            if (e.Key == Key.Insert && this._addButton != null)
                this.Add();
        }

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Стандартный обработчик.")]
        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            this.Delete();
        }

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Стандартный обработчик.")]
        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            this.Edit();
        }

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Стандартный обработчик.")]
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            this.Add();
        }

        #endregion
    }
}
