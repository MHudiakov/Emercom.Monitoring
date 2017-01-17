// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridHelperWF.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Объект обработки пользовательского ввода от таблицы
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Threading;
    using System.Windows.Forms;

    using global::DevExpress.XtraEditors;
    using global::DevExpress.XtraGrid;
    using global::DevExpress.XtraGrid.Views.Grid;

    using Init.Tools;
    using Init.Tools.UI;

    /// <summary>
    /// Объект обработки пользовательского ввода от таблицы
    /// </summary>
    /// <typeparam name="T">Тип объекта таблицы</typeparam>
    public class GridHelperWF<T>
            where T : class
    {
        /// <summary>
        /// Контрол, отображающий данные
        /// </summary>
        private readonly GridControl _grid;

        /// <summary>
        /// Стратегия взаимодействия с БД
        /// </summary>
        private readonly GhStrategy<T> _strategy;

        /// <summary>
        /// View, используемый контролом для отображения данных
        /// </summary>
        private readonly GridView _view;

        /// <summary>
        /// Кнопка добавления
        /// </summary>
        private readonly SimpleButton _addButton;

        /// <summary>
        /// Кнопка редактирования
        /// </summary>
        private readonly SimpleButton _editButton;

        /// <summary>
        /// Кнопка удаления
        /// </summary>
        private readonly SimpleButton _deleteButton;

        /// <summary>
        /// Метод вызова формы редактирования
        /// </summary>
        private readonly Func<T, bool> _executerVoid;

        /// <summary>
        /// Возвращает выделенный элемент
        /// </summary>
        public T SelectedItem
        {
            get
            {
                return this._view.GetFocusedRow() as T;
            }

            set
            {
                if (value == null)
                    return;

                var items = this._grid.DataSource as IList<T>;
                if (items != null)
                {
                    var selItem = items.SingleOrDefault(value.Equals);
                    this._view.FocusedRowHandle = this._view.GetRowHandle(items.IndexOf(selItem));
                }
            }
        }

        /// <summary>
        /// Текущая коллекция элементов
        /// </summary>
        public IEnumerable<T> Items { get; private set; }

        /// <summary>
        /// Событие смены выделенного элемента
        /// </summary>
        public event Action<T> SelectedItemChannged;

        /// <summary>
        /// Генерирует событие смены выделенного элемента
        /// </summary>
        /// <param name="item">Новое значение выделенного элемента</param>
        protected virtual void OnSelectedItemChannged(T item)
        {
            var handler = this.SelectedItemChannged;
            if (handler != null)
                handler(item);
        }

        /// <summary>
        /// Добавление к гриду функционала по добавлению, редактированию и удалению объектов
        /// </summary>
        /// <param name="grid">Грид</param>
        /// <param name="addButton">Кнопка "Добавить"</param>
        /// <param name="editButton">Кнопка "Изменить"</param>
        /// <param name="deleteButton">Кнопка "Удалить"</param>
        /// <param name="executerVoid">Метод вызова формы редактирования</param>
        /// <param name="strategy">Стратегия взаимодействия с БД</param>
        public GridHelperWF(
            GridControl grid,
            SimpleButton addButton,
            SimpleButton editButton,
            SimpleButton deleteButton,
            Func<T, bool> executerVoid,
            GhStrategy<T> strategy)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");

            this._view = grid.MainView as GridView;
            if (this._view == null)
                throw new ArgumentException(@"GridHelperWF работает только с MainView типа GridView", "grid");

            if (executerVoid == null)
                throw new ArgumentNullException("executerVoid");

            if (strategy == null)
                throw new ArgumentNullException("strategy");

            this._grid = grid;
            this._addButton = addButton;
            this._editButton = editButton;
            this._deleteButton = deleteButton;
            this._executerVoid = executerVoid;
            this._strategy = strategy;
            this.Items = new List<T>();
            this._view.KeyDown += this.ViewKeyDown;
            this._view.FocusedRowChanged +=
                (sender, args) => this.OnSelectedItemChannged(this._view.GetRow(args.FocusedRowHandle) as T);

            // закомментировано ибо по двойному клику выдаётся список движения
            // if (editButton != null)
            //     this._view.DoubleClick += (sender, args) => this.EditAction();

            if (addButton != null)
                addButton.Click += (sender, args) => this.AddAction();
            if (editButton != null)
                editButton.Click += (sender, args) => this.EditAction();
            if (deleteButton != null)
                deleteButton.Click += (sender, args) => this.DeleteAction();
        }

        /// <summary>
        /// Обработка нажатий клавиш в гриде
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="args">Параметры события</param>
        private void ViewKeyDown(object sender, KeyEventArgs args)
        {
            // не выполняем действия не продублированные кнопками
            if (args.KeyCode == Keys.Delete && this._deleteButton != null)
                this.DeleteAction();
            if (args.KeyCode == Keys.F2 && this._editButton != null)
                this.EditAction();
            if (args.KeyCode == Keys.Insert && this._addButton != null)
                this.AddAction();
        }

        /// <summary>
        /// Обновление данных
        /// </summary>
        public void AsyncUpdateData()
        {
            this._grid.BeginWait();
            this._grid.BeginUpdate();
            if (this._addButton != null)
                this._addButton.Enabled = false;
            if (this._editButton != null)
                this._editButton.Enabled = false;
            if (this._deleteButton != null)
                this._deleteButton.Enabled = false;

            try
            {
                var worker = new Thread(this.UpdateData) { IsBackground = true };
                worker.Start();
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception(string.Format("Ошибка загрузки данных в GridHelperWPF<{0}>", typeof(T).Name), ex));
                this.UnlockInterface();
            }
        }

        /// <summary>
        /// Событие по завершении обновления данных
        /// </summary>
        public event Action<GridHelperWF<T>> UpdateDataCompleted;

        /// <summary>
        /// Генерирует событие по завершении обновления данных
        /// </summary>
        /// <param name="helperWf">Объект в котором произошла загрузка</param>
        protected virtual void OnUpdateDataCompleted(GridHelperWF<T> helperWf)
        {
            Action<GridHelperWF<T>> handler = this.UpdateDataCompleted;
            if (handler != null)
                handler(helperWf);
        }

        /// <summary>
        /// Метод обновления данных
        /// </summary>
        private void UpdateData()
        {
            try
            {
                T selItem = null;
                this._grid.Invoke(new Action(() => selItem = this.SelectedItem));

                this.Items = this._strategy.GetList();
                this._grid.Invoke(new Action(() =>
                    {
                        this._grid.DataSource = this.Items;
                        this.SelectedItem = selItem;

                        this.OnUpdateDataCompleted(this);
                    }));
                //this._grid.CustomBestFitColumns(); - нельзя делать в таблицах с чеками?
            }
            catch (Exception ex)
            {
                Log.AddException(new Exception(string.Format("Ошибка асинхронной загрузки данных в GridHelperWF<{0}>", typeof(T).Name), ex));
            }
            finally
            {
                this._grid.Invoke(new Action(this.UnlockInterface));
            }
        }

        /// <summary>
        /// Разблокировка интерфейса
        /// </summary>
        private void UnlockInterface()
        {
            this._grid.EndWait();
            this._grid.EndUpdate();

            if (this._addButton != null)
                this._addButton.Enabled = true;
            if (this._editButton != null)
                this._editButton.Enabled = true;
            if (this._deleteButton != null)
                this._deleteButton.Enabled = true;
        }

        /// <summary>
        /// Выполняет действие удаления выделенного объекта
        /// </summary>
        public void DeleteAction()
        {
            var selectedItem = this.SelectedItem;
            if (selectedItem == null)
            {
                xMsg.MsgEmptyDeleteData();
                return;
            }

            if (!xMsg.MsgWithConfirmDelete())
                return;

            try
            {
                this._strategy.Delete(selectedItem);

                var items = this._grid.DataSource as IList<T>;
                if (items != null)
                {
                    var index = items.IndexOf(selectedItem);
                    items.Remove(selectedItem);

                    index--;
                    if (index > items.Count)
                        index = items.Count - 1;
                    if (index < 0)
                        index = 0;

                    this._grid.RefreshDataSource();

                    this._view.FocusedRowHandle = this._view.GetRowHandle(index);
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
                var err = new Exception(string.Format(@"Ошибка удаления из таблицы {0}", typeof(T).Name), ex);
                Log.AddException(err);
                xMsg.Error(err);
            }
        }

        /// <summary>
        /// Выполняет действе редактирования объекта
        /// </summary>
        public void EditAction()
        {
            var selectedItem = this.SelectedItem;
            if (selectedItem == null)
            {
                xMsg.MsgEmptyEditData();
                return;
            }

            try
            {
                if (this._executerVoid(selectedItem))
                {
                    this._strategy.Edit(selectedItem);
                    this._grid.RefreshDataSource();

                    var items = this._grid.DataSource as IList<T>;
                    if (items != null)
                        this._view.FocusedRowHandle = this._view.GetRowHandle(items.IndexOf(selectedItem));
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
                var err = new Exception(string.Format(@"Ошибка редактирования таблицы {0}", typeof(T).Name), ex);
                Log.AddException(err);
                xMsg.Error(err);
            }
        }

        /// <summary>
        /// Выполняет действие добавления объекта
        /// </summary>
        public void AddAction()
        {
            var item = this._strategy.CreateItem();

            try
            {
                if (this._executerVoid(item))
                {
                    this._strategy.Add(item);
                    var items = this._grid.DataSource as IList<T>;
                    if (items != null)
                    {
                        // добавление записи к гриду
                        if (!items.Contains(item))
                            items.Add(item);
                        this._grid.RefreshDataSource();
                        this._view.FocusedRowHandle = this._view.GetRowHandle(items.IndexOf(item));
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
                var err = new Exception(string.Format(@"Ошибка добавления в  таблицу {0}", typeof(T).FullName), ex);
                Log.AddException(err);
                xMsg.Error(err);
            }
        }
    }
}
