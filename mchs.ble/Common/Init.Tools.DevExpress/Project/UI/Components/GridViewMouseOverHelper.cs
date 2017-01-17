// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridViewMouseOverHelper.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Helper. следит за перемещением мыши и делает строку под курсосром активной
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.UI.Components
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Windows.Forms;

    using global::DevExpress.XtraGrid.Views.Grid;
    using global::DevExpress.XtraGrid.Views.Grid.ViewInfo;

    /// <summary>
    /// Helper. следит за перемещением мыши и делает строку под курсосром активной
    /// </summary>
    public class GridViewMouseOverHelper
    {
        /// <summary>
        /// Ссылка на GridView, за которым следим
        /// </summary>
        private readonly GridView _view;

        /// <summary>
        /// Helper. следит за перемещением мыши и делает строку под курсосром активной
        /// </summary>
        /// <param name="view">GridView, за которым следим</param>
        public GridViewMouseOverHelper(GridView view)
        {
            this._view = view;
            view.MouseMove += this.ViewMouseMove;
            view.MouseLeave += this.ViewMouseLeave;
            view.FocusedRowChanged += this.ViewFocusedRowChanged;
        }

        /// <summary>
        /// Изменение активной строки
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void ViewFocusedRowChanged(object sender, global::DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.ActiveRowHandle = e.FocusedRowHandle;
        }

        /// <summary>
        /// Сброс выделения строки грида
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void ViewMouseLeave(object sender, EventArgs e)
        {
            this.ActiveRowHandle = -1;
        }

        #region ActiveRowHandle

        /// <summary>
        /// Указатель на активную строку
        /// </summary>
        private int _activeRowHandle;

        /// <summary>
        /// Активная строка
        /// </summary>
        public int ActiveRowHandle
        {
            get
            {
                return this._activeRowHandle;
            }

            set
            {
                var old = this._activeRowHandle;
                this._activeRowHandle = value;
                this._view.RefreshRow(this._activeRowHandle);
                this._view.RefreshRow(old);
                this._view.FocusedRowHandle = this._activeRowHandle;
            }
        }
        #endregion

        /// <summary>
        /// Установка активной строкой строку, в которой расположена ячейка, имеющая заданные координаты
        /// </summary>
        /// <param name="location">
        /// Координаты ячейки
        /// </param>
        private void UpdateHotTrackedCell(Point location)
        {
            GridHitInfo hi = this._view.CalcHitInfo(location);
            this.ActiveRowHandle = hi.RowHandle;
        }

        /// <summary>
        /// Установка активной строкой строку, которая находится под курсором мыши
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private void ViewMouseMove(object sender, MouseEventArgs e)
        {
            this.UpdateHotTrackedCell(e.Location);
        }
    }
}