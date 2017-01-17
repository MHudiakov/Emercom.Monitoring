// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XtraGrid.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Предоставляет методы для работы с XtraGrid компонентами
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Forms;

    using global::DevExpress.Utils;
    using global::DevExpress.XtraGrid;
    using global::DevExpress.XtraGrid.Views.Base;
    using global::DevExpress.XtraGrid.Views.Grid;
    using global::DevExpress.XtraPivotGrid;

    /// <summary>
    /// Предоставляет методы для работы с XtraGrid компонентами
    /// </summary>
    public static class XtraGrid
    {
        /// <summary>
        /// Начало загрузки данных
        /// </summary>
        /// <param name="grid">
        /// Компонент класа GridControl
        /// </param>
        public static void BeginLoad(this GridControl grid)
        {
            var view = grid.MainView as GridView;
            if (view == null)
                throw new InvalidOperationException(@"GridHelperWF работает только с MainView типа GridView");
            view.OptionsView.ShowViewCaption = true;
            view.Appearance.ViewCaption.TextOptions.HAlignment = HorzAlignment.Near;
            view.ViewCaption = @"     Загрузка данных...";
        }

        /// <summary>
        /// Окончание загрузки
        /// </summary>
        /// <param name="grid">
        /// Компонент класа GridControl
        /// </param>
        public static void EndLoad(this GridControl grid)
        {
            var view = grid.MainView as GridView;
            if (view == null)
                throw new InvalidOperationException(@"GridHelperWF работает только с MainView типа GridView");
            view.Appearance.ViewCaption.TextOptions.HAlignment = HorzAlignment.Near;
            view.ViewCaption = @"     Загрузка данных завершена";
            view.OptionsView.ShowViewCaption = false;
        }

        /// <summary>
        /// Расширение функциональности GridControl: параметры по-умолчанию
        /// </summary>
        /// <param name="grid">
        /// Компонент класа GridControl
        /// </param>
        public static void GridExtend(this GridControl grid)
        {
            GridExtend(grid, GridExtendProperties.Default);
        }

        /// <summary>
        /// Расширение функциональности GridControl: параметризованное
        /// </summary>
        /// <param name="grid">
        /// Экземпляр таблицы для настройки
        /// </param>
        /// <param name="prop">
        /// Настройки таблицц
        /// </param>
        public static void GridExtend(this GridControl grid, GridExtendProperties prop)
        {
            CreateExportContextMenu(grid, prop);

            // Настройка внешнего вида грида
            var view = grid.MainView as GridView;
            if (view == null)
                throw new InvalidOperationException("GridExtend работает только с View типа GridView");

            if (prop.ShowAutoFilterRow != null) view.OptionsView.ShowAutoFilterRow = (bool)prop.ShowAutoFilterRow;
            if (prop.ShowFooter != null) view.OptionsView.ShowFooter = (bool)prop.ShowFooter;
            if (prop.ShowGroupPanel != null) view.OptionsView.ShowGroupPanel = (bool)prop.ShowGroupPanel;
            if (prop.ShowIndicator != null) view.OptionsView.ShowIndicator = (bool)prop.ShowIndicator;
            if (prop.ColumnAutoWidth != null) view.OptionsView.ColumnAutoWidth = (bool)prop.ColumnAutoWidth;
            if (prop.ColumnPanelRowHeight != null) view.ColumnPanelRowHeight = (int)prop.ColumnPanelRowHeight;
            if (prop.BestFitColumns) grid.DataSourceChanged += GridDataSourceChanged;
            if (prop.IsHeaderTextPositionCenter)
                view.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            view.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;
        }

        /// <summary>
        /// Создание констекстного меню экспорта данных
        /// </summary>
        /// <param name="grid">
        /// Экземпляр таблицы
        /// </param>
        /// <param name="prop">
        /// Свойства
        /// </param>
        public static void CreateExportContextMenu(GridControl grid, GridExtendProperties prop)
        {
            var cmExport = new ContextMenuStrip();
            var export = new ToolStripMenuItem("Экспорт..") { Image = Resource.export };
            var optimize = new ToolStripMenuItem("Оптимизировать ширину столбцов..") { Image = Resource.optimize };
            export.DropDownItems.Add("Экспорт в указанный файл", Resource.custom, (sender, e) => Exporter.Export((GridControl)((ToolStripItem)sender).Tag, ExportKind.CUSTOM)).Tag = grid;
            export.DropDownItems.Add("Экспорт в Excel 2003", Resource.xls, (sender, e) => Exporter.Export((GridControl)((ToolStripItem)sender).Tag, ExportKind.XLS)).Tag = grid;
            export.DropDownItems.Add("Экспорт в Excel 2007", Resource.xlsx, (sender, e) => Exporter.Export((GridControl)((ToolStripItem)sender).Tag, ExportKind.XLSX)).Tag = grid;
            export.DropDownItems.Add("Экспорт в PDF", Resource.pdf, (sender, e) => Exporter.Export((GridControl)((ToolStripItem)sender).Tag, ExportKind.PDF)).Tag = grid;
            export.DropDownItems.Add("Экспорт в HTML", Resource.html, (sender, e) => Exporter.Export((GridControl)((ToolStripItem)sender).Tag, ExportKind.HTML)).Tag = grid;
            export.DropDownItems.Add("Экспорт в RTF", Resource.rtf, (sender, e) => Exporter.Export((GridControl)((ToolStripItem)sender).Tag, ExportKind.RTF)).Tag = grid;
            export.DropDownItems.Add("Экспорт в TXT", Resource.txt, (sender, e) => Exporter.Export((GridControl)((ToolStripItem)sender).Tag, ExportKind.TXT)).Tag = grid;
            cmExport.Items.Add(export);
            cmExport.Items.Add(optimize);
            grid.ContextMenuStrip = cmExport;
            export.Enabled = prop.HasExport != null && (bool)prop.HasExport;
            optimize.Tag = grid;
            optimize.Click += DoBestFitColumns;
        }

        /// <summary>
        /// Создание констекстного меню экспорта данных
        /// </summary>
        /// <param name="grid">
        /// Экземпляр таблицы
        /// </param>
        public static void CreateExportContextMenu(PivotGridControl grid)
        {
            var cmExport = new ContextMenuStrip();
            var export = new ToolStripMenuItem("Экспорт..") { Image = Resource.export };
            export.DropDownItems.Add("Экспорт в указанный файл", Resource.custom, (sender, e) => Exporter.Export((PivotGridControl)((ToolStripItem)sender).Tag)).Tag = grid;
            export.DropDownItems.Add("Экспорт в Excel 2003", Resource.xls, (sender, e) => Exporter.Export((PivotGridControl)((ToolStripItem)sender).Tag)).Tag = grid;
            export.DropDownItems.Add("Экспорт в Excel 2007", Resource.xlsx, (sender, e) => Exporter.Export((PivotGridControl)((ToolStripItem)sender).Tag)).Tag = grid;
            export.DropDownItems.Add("Экспорт в PDF", Resource.pdf, (sender, e) => Exporter.Export((PivotGridControl)((ToolStripItem)sender).Tag)).Tag = grid;
            export.DropDownItems.Add("Экспорт в HTML", Resource.html, (sender, e) => Exporter.Export((PivotGridControl)((ToolStripItem)sender).Tag)).Tag = grid;
            export.DropDownItems.Add("Экспорт в RTF", Resource.rtf, (sender, e) => Exporter.Export((PivotGridControl)((ToolStripItem)sender).Tag)).Tag = grid;
            export.DropDownItems.Add("Экспорт в TXT", Resource.txt, (sender, e) => Exporter.Export((PivotGridControl)((ToolStripItem)sender).Tag)).Tag = grid;
            cmExport.Items.Add(export);
            grid.ContextMenuStrip = cmExport;
            export.Enabled = true;
        }

        /// <summary>
        /// Оптимизация размера колонок таблицы
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private static void GridDataSourceChanged(object sender, EventArgs e)
        {
            if (!(sender is GridControl))
                throw new ArgumentException(@"Ожидался тип GridControl", "sender");
            (sender as GridControl).CustomBestFitColumns();
        }

        /// <summary>
        /// Оптимизация ширины столбцов
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        private static void DoBestFitColumns(object sender, EventArgs e)
        {
            if (!(sender is ToolStripItem))
                throw new ArgumentException(@"Ожидался тип ToolStripItem", "sender");
            ((GridView)((GridControl)((ToolStripItem)sender).Tag).MainView).BestFitColumns();
        }

        /// <summary>
        /// Добавление объекта в связанный с гридом список, обновление и позиционирование в гриде на добавленном объекте
        /// </summary>
        /// <param name="grid">
        /// Таблица
        /// </param>
        /// <param name="obj">
        /// Добавляемый объект
        /// </param>
        public static void Add(this GridControl grid, object obj)
        {
            if (obj == null)
                return;
            if (!(grid.MainView is GridView))
                throw new ArgumentException(@"Ожидался тип GridView", "grid.MainView");

            var view = grid.MainView as GridView;
            ((IList)grid.DataSource).Add(obj);
            view.RefreshData();
            view.FocusedRowHandle = view.GetRowHandle(((IList)grid.DataSource).IndexOf(obj));
        }

        /// <summary>
        /// Обновление грида при редактировании объекта в связанном с гридом списке
        /// </summary>
        /// <param name="grid">
        /// Обновляемый грид
        /// </param>
        public static void Edit(this GridControl grid)
        {
            grid.MainView.RefreshData();
        }

        /// <summary>
        /// Удаление объекта из связанного с гридом списка, обновление и позиционирование в гриде рядом с удаленным объектом
        /// </summary>
        /// <param name="grid">
        /// Обновляемый грид
        /// </param>
        public static void Delete(this GridControl grid)
        {
            var view = grid.MainView as GridView;
            if (view == null)
                return;
            object focusedObj = view.GetFocusedRow();
            int currentRowHandle;
            if (view.IsLastRow && !view.IsFirstRow)
                currentRowHandle = view.GetPrevVisibleRow(((IList)grid.DataSource).IndexOf(focusedObj));
            else
                currentRowHandle = view.GetFocusedDataSourceRowIndex();

            ((IList)grid.DataSource).Remove(focusedObj);
            view.FocusedRowHandle = currentRowHandle;

            view.RefreshData();
        }

        /// <summary>
        /// Изменяет размер всех видимых столбцов, чтобы они оптимально соответствовали их содержанию.
        /// </summary>
        /// <param name="grid">
        /// Грид, в котором нужно оптимизировать размер стобцов
        /// </param>
        public static void CustomBestFitColumns(this GridControl grid)
        {
            if (grid.DataSource == null)
                return;

            if (((GridView)grid.MainView).IsEmpty)
                return;
            const int ROWS_FOR_CUSTOM_BEST_FIT_COLUMNS_COUNT = 10;
            var obj = grid.DataSource as IList;
            if (obj == null)
                return;
            if (!(grid.MainView is GridView))
                throw new ArgumentException(@"Ожидался тип GridView", "grid.MainView");
            var view = grid.MainView as GridView;
            object selectedElement = view.GetFocusedRow();

            if (obj.Count < ROWS_FOR_CUSTOM_BEST_FIT_COLUMNS_COUNT)
            {
                view.BestFitColumns();
                return;
            }

            view.BeginDataUpdate();
            var tempObj = new List<object>();
            for (int i = 0; i < ROWS_FOR_CUSTOM_BEST_FIT_COLUMNS_COUNT; i++)
                tempObj.Add(obj[i]);
            grid.DataSourceChanged -= GridDataSourceChanged;
            grid.DataSource = tempObj;
            view.BestFitColumns();
            grid.DataSource = obj;
            grid.DataSourceChanged += GridDataSourceChanged;
            view.EndDataUpdate();
            view.FocusedRowHandle = view.GetRowHandle(obj.IndexOf(selectedElement));
        }
    }
}