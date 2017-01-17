// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucPopUpContainerWithFilter.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Всплывающая панель с фильтром
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.UI.Components
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Forms;

    using global::DevExpress.Utils;
    using global::DevExpress.XtraEditors;
    using global::DevExpress.XtraEditors.Controls;
    using global::DevExpress.XtraGrid.Views.Grid;

    /// <summary>
    /// Всплывающая панель с фильтром
    /// </summary>
    public partial class ucPopUpContainerWithFilter : PopupContainerControl
    {
        /// <summary>
        /// Редактируемое значение
        /// </summary>
        private object _editValue;

        /// <summary>
        /// Кэшированная ссылка на helper, помогающий настроить связывание редакторы фильтра, грида и обработку событий
        /// </summary>
        private GridFilterBindHelper _gridFilterBindHelper;

        /// <summary>
        /// Стратегия применения значения Parent редактору
        /// </summary>
        [Category("Binding")]
        [Description("Стратегия установки значения в родительсокм PopupContainerEdit")]
        [DefaultValue(EditValueStrategy.Hybrid)]
        public EditValueStrategy EditValueStrategy { get; set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ucPopUpContainerWithFilter"/>.
        /// </summary>
        public ucPopUpContainerWithFilter()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Значение редактируемого объекта
        /// </summary>
        public object EditValue
        {
            get
            {
                return this._editValue;
            }

            set
            {
                if (this._editValue != value)
                {
                    this._editValue = value;
                    if (this.EditValueChanged != null)
                        this.EditValueChanged(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Значение в гриде
        /// </summary>
        [Category("Data")]
        [Description("Значение строки грида")]
        public object GridEditValue { get; private set; }

        /// <summary>
        /// Значение в фильтре
        /// </summary>
        [Category("Data")]
        [Description("Значение строки фильтра")]
        public object FilterEditValue { get; private set; }

        /// <summary>
        /// Стиль отрисовки подсветки фильтра
        /// </summary>
        [Category("Appearance")]
        [Description("Стиль отрисовки подсветки фильтра")]
        public AppearanceObject FilterAppearance { get; set; }

        /// <summary>
        /// TextEdit, значения которого будут использоваться как фильтр
        /// </summary>
        [Category("Binding")]
        [Description("TextEdit значения которого будут использоваться как фильтр")]
        [DefaultValue(EditValueStrategy.Hybrid)]
        public TextEdit FilterTextEdit { get; set; }

        /// <summary>
        /// Фильтруемый GridViev
        /// </summary>
        [Category("Binding")]
        [Description("GridViev к  которму применяется фильтр")]
        [DefaultValue(EditValueStrategy.Hybrid)]
        public GridView FilteredGridView { get; set; }

        /// <summary>
        /// Событие изменения редактируемого значения
        /// </summary>
        [Category("Data")]
        public event EventHandler EditValueChanged;

        /// <summary>
        /// Выполняется при завершшении изменения родительского объекта
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.OwnerEdit != null)
            {
                this.OwnerEdit.CloseUp -= this.OnCloseUpEventHandler;
                this.OwnerEdit.CloseUp += this.OnCloseUpEventHandler;
                this.FilterEditValue = this.OwnerEdit.EditValue;
                this.GridEditValue = this.OwnerEdit.EditValue;
            }
        }

        /// <summary>
        /// Обрабатывает закрытие всплывающей панели
        /// </summary>
        /// <param name="o">
        /// Инициатор события
        /// </param>
        /// <param name="args">
        /// Предоставляет данные для события закрытия всплывающей панеоли
        /// </param>
        private void OnCloseUpEventHandler(object o, CloseUpEventArgs args)
        {
            switch (this.EditValueStrategy)
            {
                case EditValueStrategy.FilterEditValue:
                    this.OwnerEdit.EditValue = this.FilterEditValue;
                    break;
                case EditValueStrategy.GridRowObject:
                    this.OwnerEdit.EditValue = this.GridEditValue;
                    break;
                case EditValueStrategy.GridRowText:
                    this.OwnerEdit.EditValue = this.GridEditValue != null ? this.GridEditValue.ToString() : null;
                    break;
                case EditValueStrategy.Hybrid:
                    this.OwnerEdit.EditValue = this.EditValue;
                    break;
                default:
                    this.OwnerEdit.EditValue = this.EditValue;
                    break;
            }

            args.AcceptValue = true;
            args.Value = this.OwnerEdit.EditValue;
        }

        /// <summary>
        /// Инициализация грида, выполняемая при загрузки 
        /// </summary>
        protected override void OnLoaded()
        {
            if (this.FilteredGridView != null && this.FilterTextEdit != null)
            {
                // Устанавливаем привязку грида с редактором фильтра
                this._gridFilterBindHelper = new GridFilterBindHelper(this.FilterTextEdit, this.FilteredGridView);

                // обрабатываем нажатия кнопок в гриде
                this.FilteredGridView.KeyDown += this.FilteredGridViewKeyDown;
                this.FilteredGridView.DoubleClick += (sender, args) =>
                    {
                        this.FilterEditValue = this.FilterTextEdit.Text;
                        this.GridEditValue = this.FilteredGridView.GetFocusedRow();
                        this.EditValue = this.FilterEditValue;

                        if (this.OwnerEdit != null)
                            this.OwnerEdit.ClosePopup();
                    };

                this.FilteredGridView.RowClick += (sender, args) =>
                    {
                        if (this.OwnerEdit != null)
                            this.SetEditValueFromGrid();
                    };

                this.FilterTextEdit.KeyDown += this.FilterTextEditKeyDown;

                this._gridFilterBindHelper.Init();
                if (this.FilterAppearance != null)
                    this._gridFilterBindHelper.GridFilterPaintHelper.FilterAppearence = this.FilterAppearance;
                else
                    this.FilterAppearance = this._gridFilterBindHelper.GridFilterPaintHelper.FilterAppearence;
            }

            base.OnLoaded();
        }

        /// <summary>
        /// Нажатие на кнопку фильтрации TextEdit
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void FilterTextEditKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.FilterEditValue = this.FilterTextEdit.Text;
                this.GridEditValue = this.FilteredGridView.GetFocusedRow();
                this.EditValue = this.FilterEditValue;

                if (this.OwnerEdit != null)
                    this.OwnerEdit.ClosePopup();
            }

            if (e.KeyCode == Keys.Down)
            {
                this.FilteredGridView.Focus();
            }
        }

        /// <summary>
        /// Устанвка редакируемого значения из грида
        /// </summary>
        private void SetEditValueFromGrid()
        {
            this.FilterEditValue = this.FilterTextEdit.Text;
            this.GridEditValue = this.FilteredGridView.GetFocusedRow();
            this.EditValue = this.GridEditValue;
        }

        /// <summary>
        /// Обработка нажатия клавиш 'Enter' b 'Up' в фильтруемом гриде
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void FilteredGridViewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.OwnerEdit != null)
            {
                this.SetEditValueFromGrid();
                if (this.OwnerEdit != null) this.OwnerEdit.ClosePopup();
            }

            if (e.KeyCode == Keys.Up)
            {
                var index = this.FilteredGridView.GetVisibleIndex(this.FilteredGridView.GetFocusedDataSourceRowIndex());
                if (index <= 0)
                    this.FilterTextEdit.Focus();
            }
        }
    }
}