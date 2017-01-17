// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridFilterPaintHelper.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Helper для реализации гибкого фильтра с подсветкой в GridView
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.UI.Components
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;

    using global::DevExpress.Utils;
    using global::DevExpress.XtraEditors.Repository;
    using global::DevExpress.XtraGrid.Columns;
    using global::DevExpress.XtraGrid.Views.Grid;
    using global::DevExpress.XtraPrinting.Native;

    using Init.Tools;

    /// <summary>
    /// Helper для реализации гибкого фильтра с подсветкой в GridView
    /// </summary>
    public class GridFilterPaintHelper
    {
        /// <summary>
        /// Кэшированная ссылка на GridView
        /// </summary>
        private readonly GridView _view;

        /// <summary>
        /// Стиль рисования прямоугольников выделения
        /// </summary>
        public AppearanceObject FilterAppearence { get; set; }

        /// <summary>
        /// Фильтруемая строка
        /// </summary>
        private string _filterString;

        /// <summary>
        /// Массив слов
        /// </summary>
        private string[] _words;

        /// <summary>
        /// Установка и получение фильтруемой строки
        /// </summary>
        public string FilterString
        {
            get
            {
                return this._filterString;
            }

            set
            {
                this._filterString = value;

                if (this._filterString.Length > 0)
                    this.ActivateFilter();
                else
                {
                    this._view.FocusedRowHandle = -1;
                    this.DeactivateFilter();
                }
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GridFilterPaintHelper"/>.
        /// </summary>
        /// <param name="view">
        /// GridView
        /// </param>
        public GridFilterPaintHelper(GridView view)
        {
            this._view = view;

            // цвет отрисовки выделения по умолчанию
            this.FilterAppearence = AppearanceObject.EmptyAppearance;
            this.FilterAppearence.BackColor = Color.FromArgb(192, 255, 192);
            this.FilterAppearence.BackColor2 = Color.FromArgb(255, 192, 128);
            this.FilterAppearence.BorderColor = Color.SandyBrown;
            this.FilterAppearence.GradientMode = LinearGradientMode.Vertical;
            this.FilterAppearence.Options.UseBackColor = true;
            this.FilterAppearence.Options.UseBorderColor = true;

            // отрисовка ячеек в гриде
            this._view.CustomDrawCell += this.GvDataCustomDrawCell;
        }

        #region Фильтрация

        /// <summary>
        /// Деактивация фильтра
        /// </summary>
        private void DeactivateFilter()
        {
            try
            {
                this.SetFilter(string.Empty);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Активация фильтра
        /// </summary>
        private void ActivateFilter()
        {
            try
            {
                this.SetFilter(this._filterString);
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Установка 
        /// </summary>
        /// <param name="filterString">
        /// Строка фильтрации
        /// </param>
        private void SetFilter(string filterString)
        {
            if (filterString.IsEmpty())
            {
                this._view.ClearColumnsFilter();
                return;
            }

            this._words = filterString.Split(' ');
            var fullFilterText = string.Empty;

            foreach (var word in this._words)
            {
                var filterText = string.Empty;
                foreach (var c in from GridColumn c in this._view.Columns where c.Visible select c)
                {
                    if (filterText == string.Empty)
                        filterText = c.FieldName + " Like '%" + word + "%'";
                    else
                        filterText += " OR " + c.FieldName + " Like '%" + word + "%'";
                }

                if (fullFilterText == string.Empty)
                    fullFilterText = "(" + filterText + ")";
                else
                    fullFilterText += " AND " + "(" + filterText + ")";
            }

            this._view.ActiveFilterString = fullFilterText;
            this._view.ApplyColumnsFilter();
        }

        #endregion

        /// <summary>
        /// Отрисовка фильтов в ячейках грида
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void GvDataCustomDrawCell(object sender, global::DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.DisplayText == null)
                    return;
                var text = e.DisplayText.ToString();
                if (this._words == null)
                    return;
                var needDrawAppearance = false;
                foreach (var word in this._words)
                {
                    int ind = text.ToLower().IndexOf(word.ToLower(), StringComparison.Ordinal);
                    if (ind < 0)
                        continue;
                    needDrawAppearance = true;

                    if (e.Column.RealColumnEdit is RepositoryItemMemoEdit && this._view.OptionsView.RowAutoHeight)
                        continue;

                    var textBefore = text.Substring(0, ind);
                    var textBeforeSize = e.Appearance.CalcTextSize(e.Graphics, textBefore, e.Bounds.Width);

                    var selectedTextSize = e.Appearance.CalcTextSize(e.Graphics, word, e.Bounds.Width);

                    var selectionRectangle = new RectangleF(
                        e.Bounds.Left + textBeforeSize.Width,
                        e.Bounds.Top + (e.Bounds.Height - selectedTextSize.Height) / 2,
                        selectedTextSize.Width,
                        selectedTextSize.Height);

                    var rect =
                        new Rectangle(
                            new Point((int)selectionRectangle.Location.X, (int)selectionRectangle.Location.Y),
                            new Size((int)selectionRectangle.Size.Width, (int)selectionRectangle.Size.Height));

                    this.FilterAppearence.DrawBackground(e.Cache, rect);
                    if (this.FilterAppearence.Options.UseBorderColor)
                        e.Graphics.DrawRectangle(this.FilterAppearence.GetBorderPen(e.Cache), rect);
                }

                if (!needDrawAppearance)
                    return;
                if (e.Column.RealColumnEdit is RepositoryItemMemoEdit && this._view.OptionsView.RowAutoHeight)
                    e.Graphics.DrawRectangle(this.FilterAppearence.GetBorderPen(e.Cache), e.Bounds);
                else
                {
                    e.Appearance.DrawString(e.Cache, text, e.Bounds);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Log.AddException(ex);
            }
        }
    }
}