// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Exporter.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Реализует экспорт данных в определенный формат
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    using global::DevExpress.XtraGrid;
    using global::DevExpress.XtraPivotGrid;

    using Init.Tools.UI;

    /// <summary>
    /// Перечисление типов, в которые можно экспортировать данные 
    /// </summary>
    public enum ExportKind
    {
        /// <summary>
        /// Excel 2003
        /// </summary>
        // ReSharper disable once InconsistentNaming
        XLS,

        /// <summary>
        /// Excel 2007
        /// </summary>
        // ReSharper disable once InconsistentNaming
        XLSX,

        /// <summary>
        /// HTML формат
        /// </summary>
        // ReSharper disable once InconsistentNaming
        HTML,

        /// <summary>
        /// RTF формат
        /// </summary>
        // ReSharper disable once InconsistentNaming
        RTF,

        /// <summary>
        /// PDF формат
        /// </summary>
        // ReSharper disable once InconsistentNaming
        PDF,

        /// <summary>
        /// TXT формат
        /// </summary>
        // ReSharper disable once InconsistentNaming
        TXT,

        /// <summary>
        /// Заданный пользователем формат
        /// </summary>
        // ReSharper disable once InconsistentNaming
        CUSTOM
    }

    /// <summary>
    /// Реализует экспорт данных в определенный формат
    /// </summary>
    public class Exporter
    {
        /// <summary>
        /// Экспорт таблицы в указанный файл
        /// </summary>
        /// <param name="grid">
        /// Грид, данные которого будут экспортироваться
        /// </param>
        /// <param name="kind">
        /// Тип, в который экспортируется (по умолчанию CUSTOM)
        /// </param>
        public static void Export(GridControl grid, ExportKind kind = ExportKind.CUSTOM)
        {
            string tmpPath = Path.GetTempPath();
            string currentPath;
            if (kind == ExportKind.CUSTOM)
            {
                using (var sfd = new SaveFileDialog { Filter = @"Книга Excel 2003 | *.xls|Книга Excel 2007 | *.xlsx|PDF файл | *.pdf|HTML файл | *.html|RTF файл | *.rtf|Текстовый файл | *.txt;" })
                {
                    if (sfd.ShowDialog() != DialogResult.OK)
                        return;
                    currentPath = sfd.FileName;
                }
            }
            else
            {
                int counter = 0;
                currentPath = string.Format(@"{0}\Document.{1}", tmpPath, kind.ToString().ToLower());
                while (File.Exists(currentPath))
                {
                    counter++;
                    currentPath = string.Format(@"{0}\Document{1}.{2}", tmpPath, counter, kind.ToString().ToLower());
                }
            }

            if (kind == ExportKind.XLS) grid.ExportToXls(currentPath);
            if (kind == ExportKind.XLSX) grid.ExportToXlsx(currentPath);
            if (kind == ExportKind.HTML) grid.ExportToHtml(currentPath);
            if (kind == ExportKind.RTF) grid.ExportToRtf(currentPath);
            if (kind == ExportKind.PDF) grid.ExportToPdf(currentPath);
            if (kind == ExportKind.TXT) grid.ExportToText(currentPath);

            if (kind == ExportKind.CUSTOM)
            {
                string ext = new FileInfo(currentPath).Extension.ToLower();
                switch (ext)
                {
                    case ".xls":
                        grid.ExportToXls(currentPath);
                        break;
                    case ".xlsx":
                        grid.ExportToXlsx(currentPath);
                        break;
                    case ".html":
                        grid.ExportToHtml(currentPath);
                        break;
                    case ".rtf":
                        grid.ExportToRtf(currentPath);
                        break;
                    case ".pdf":
                        grid.ExportToPdf(currentPath);
                        break;
                    case ".txt":
                        grid.ExportToText(currentPath);
                        break;
                    default:
                        MessageBox.Show(@"Неизвестный тип файла: " + ext);
                        return;
                }
            }

            if (File.Exists(currentPath))
            {
                try
                {
                    if (kind != ExportKind.CUSTOM)
                        File.SetAttributes(currentPath, FileAttributes.ReadOnly);
                    using (var proc = new Process())
                    {
                        proc.StartInfo.FileName = currentPath;
                        proc.StartInfo.UseShellExecute = true;
                        proc.Start();
                    }
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry("Application ", ex.Message);
                }
            }
            else
            {
                xMsg.Msg(string.Format("Экспорт не может быть завершен,{0}обратитесь к разработчикам", Environment.NewLine));
            }
        }

        /// <summary>
        /// Экспорт сводной таблицы в указанный файл
        /// </summary>
        /// <param name="grid">
        /// Грид, данные которого будут экспортироваться
        /// </param>
        /// <param name="kind">
        /// Тип, в который экспортируется (по умолчанию CUSTOM)
        /// </param>
        public static void Export(PivotGridControl grid, ExportKind kind = ExportKind.CUSTOM)
        {
            string tmpPath = Path.GetTempPath();
            string currentPath;
            if (kind == ExportKind.CUSTOM)
            {
                using (var sfd = new SaveFileDialog { Filter = @"Книга Excel 2003 | *.xls|Книга Excel 2007 | *.xlsx|PDF файл | *.pdf|HTML файл | *.html|RTF файл | *.rtf|Текстовый файл | *.txt;" })
                {
                    if (sfd.ShowDialog() != DialogResult.OK)
                        return;
                    currentPath = sfd.FileName;
                }
            }
            else
            {
                int counter = 0;
                currentPath = string.Format(@"{0}\Document.{1}", tmpPath, kind.ToString().ToLower());
                while (File.Exists(currentPath))
                {
                    counter++;
                    currentPath = string.Format(@"{0}\Document{1}.{2}", tmpPath, counter, kind.ToString().ToLower());
                }
            }

            if (kind == ExportKind.XLS) grid.ExportToXls(currentPath);
            if (kind == ExportKind.XLSX) grid.ExportToXlsx(currentPath);
            if (kind == ExportKind.HTML) grid.ExportToHtml(currentPath);
            if (kind == ExportKind.RTF) grid.ExportToRtf(currentPath);
            if (kind == ExportKind.PDF) grid.ExportToPdf(currentPath);
            if (kind == ExportKind.TXT) grid.ExportToText(currentPath);

            if (kind == ExportKind.CUSTOM)
            {
                string ext = new FileInfo(currentPath).Extension.ToLower();
                switch (ext)
                {
                    case ".xls":
                        grid.ExportToXls(currentPath);
                        break;
                    case ".xlsx":
                        grid.ExportToXlsx(currentPath);
                        break;
                    case ".html":
                        grid.ExportToHtml(currentPath);
                        break;
                    case ".rtf":
                        grid.ExportToRtf(currentPath);
                        break;
                    case ".pdf":
                        grid.ExportToPdf(currentPath);
                        break;
                    case ".txt":
                        grid.ExportToText(currentPath);
                        break;
                    default:
                        MessageBox.Show(@"Неизвестный тип файла: " + ext);
                        return;
                }
            }

            if (File.Exists(currentPath))
            {
                try
                {
                    if (kind != ExportKind.CUSTOM)
                        File.SetAttributes(currentPath, FileAttributes.ReadOnly);
                    using (var proc = new Process())
                    {
                        proc.StartInfo.FileName = currentPath;
                        proc.StartInfo.UseShellExecute = true;
                        proc.Start();
                    }
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry("Application ", ex.Message);
                }
            }
            else
            {
                xMsg.Msg(string.Format("Экспорт не может быть завершен,{0}обратитесь к разработчикам", Environment.NewLine));
            }
        }
    }
}