// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridExtendProperties.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Предоставляет расширенные свойства грида
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    /// <summary>
    /// Предоставляет расширенные свойства грида
    /// </summary>
    public class GridExtendProperties
    {    
        /// <summary>
        /// Возвращает или задает значение, определяющее свойства по умолчанию
        /// </summary>
        public static GridExtendProperties Default { get; set; }
        
        /// <summary>
        /// Указывает, возможен ли экспорт в файл 
        /// </summary>
        public bool? HasExport { get; set; }

        /// <summary>
        /// Возвращает или задает значение, определяющее, отображается ли автофильтр строк
        /// </summary>
        public bool? ShowAutoFilterRow { get; set; }

        /// <summary>
        /// Возвращает или задает значение, определяющее, отображается ли нижний колонтитул
        /// </summary>
        public bool? ShowFooter { get; set; }

        /// <summary>
        /// Возвращает или задает значение, определяющее, отображается ли групповая панель
        /// </summary>
        public bool? ShowGroupPanel { get; set; }

        /// <summary>
        /// Возвращает или задает значение, определяющее, отображается ли панель индикаторов строк
        /// </summary>
        public bool? ShowIndicator { get; set; }

        /// <summary>
        /// Возвращает или задает значение, определяющее, установлена ли авторегулировка ширины столбцов
        /// </summary>
        public bool? ColumnAutoWidth { get; set; }

        /// <summary>
        /// Возвращает или задает значение, определяющее высоту панели заголовка строк
        /// </summary>
        public int? ColumnPanelRowHeight { get; set; }

        /// <summary>
        /// Возвращает или задает значение, определяющее, задана ли автоматическая оптимизация ширины столбцов
        /// </summary>
        public bool BestFitColumns { get; set; }

        /// <summary>
        /// Возвращает или задает значение, определяющее, выранивается ли текст заголовка по центру
        /// </summary>
        public bool IsHeaderTextPositionCenter { get; set; }

        /// <summary>
        /// Только доступности экспорта
        /// </summary>
        public static GridExtendProperties OnlyExport = new GridExtendProperties
        {
            HasExport = true
        };

        /// <summary>
        /// Значения по умолчанию без автоматической оптимизация ширины столбцов
        /// </summary>
        public static GridExtendProperties DefaultWithoutBestFitColumns = new GridExtendProperties
        {
            HasExport = true,
            ShowAutoFilterRow = true,
            ShowFooter = true,
            ShowGroupPanel = false,
            ShowIndicator = false,
            ColumnAutoWidth = false,
            ColumnPanelRowHeight = 32,
            BestFitColumns = false,
            IsHeaderTextPositionCenter = true
        };

        /// <summary>
        /// Значения по умолчанию с экспортом и нижним колонтитулом
        /// </summary>
        public static GridExtendProperties DefaultWithExportAndFooter = new GridExtendProperties
        {
            HasExport = true,
            ShowAutoFilterRow = true,
            ShowFooter = true,
            ShowGroupPanel = false,
            ShowIndicator = false
        };

        /// <summary>
        /// Инициализирует статические поля класса <see cref="GridExtendProperties"/>.
        /// </summary>
        static GridExtendProperties()
        {
            Default = new GridExtendProperties
             {
                 HasExport = true,
                 ShowAutoFilterRow = true,
                 ShowFooter = true,
                 ShowGroupPanel = false,
                 ShowIndicator = false,
                 ColumnAutoWidth = false,
                 ColumnPanelRowHeight = 32,
                 BestFitColumns = true,
                 IsHeaderTextPositionCenter = true
             };
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GridExtendProperties"/>.
        /// </summary>
        public GridExtendProperties()
        {
            if (Default == null)
                return;
            this.HasExport = Default.HasExport;
            this.ShowAutoFilterRow = Default.ShowAutoFilterRow;
            this.ShowFooter = Default.ShowFooter;
            this.ShowGroupPanel = Default.ShowGroupPanel;
            this.ShowIndicator = Default.ShowIndicator;
            this.ColumnAutoWidth = Default.ColumnAutoWidth;
            this.ColumnPanelRowHeight = Default.ColumnPanelRowHeight;
            this.BestFitColumns = Default.BestFitColumns;
            this.IsHeaderTextPositionCenter = Default.IsHeaderTextPositionCenter;
        }
    }
}