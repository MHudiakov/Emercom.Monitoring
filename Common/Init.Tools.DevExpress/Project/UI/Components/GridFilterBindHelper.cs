// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridFilterBindHelper.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Helper, помогающий настроить связывание редакторы фильтра, грида и обработку событий
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.UI.Components
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using global::DevExpress.XtraEditors;
    using global::DevExpress.XtraGrid.Views.Grid;

    /// <summary>
    /// Helper, помогающий настроить связывание редакторы фильтра, грида и обработку событий
    /// </summary>
    public class GridFilterBindHelper
    {
        /// <summary>
        /// Кэшированная ссылка на фильтруемое поле редактирования 
        /// </summary>
        private readonly TextEdit _filterEdit;

        /// <summary>
        /// Кэшированная ссылка на фильтруемый GridView
        /// </summary>
        private readonly GridView _view;

        /// <summary>
        /// Вспомогательный объект для фильтрации
        /// </summary>
        public GridFilterPaintHelper GridFilterPaintHelper { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GridFilterBindHelper"/>.
        /// </summary>
        /// <param name="filterEdit">
        /// Фильтруемое поле редактирования 
        /// </param>
        /// <param name="view">
        /// Фильтруемый GridView
        /// </param>
        public GridFilterBindHelper(TextEdit filterEdit, GridView view)
        {
            this._filterEdit = filterEdit;
            this._view = view;
        }

        /// <summary>
        /// Установка обработчиков для грида и редактора фильтра
        /// </summary>
        public void Init()
        {
            this.GridFilterPaintHelper = new GridFilterPaintHelper(this._view);
            this._filterEdit.EditValueChanged += this.FilterTextEditEditValueChanged;
        }

        /// <summary>
        /// Меняем фильтра для грида по изменению текста в редакторе фильтра
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void FilterTextEditEditValueChanged(object sender, EventArgs e)
        {
            this.GridFilterPaintHelper.FilterString = this._filterEdit.Text;
        }
    }
}
