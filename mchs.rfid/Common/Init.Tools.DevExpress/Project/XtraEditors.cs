// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XtraEditors.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Helper класс для CheckedComboBoxEdit,
//   добавляет методы проверки и установки статуса IsSelected элементов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using global::DevExpress.XtraEditors;

    /// <summary>
    /// Helper класс для CheckedComboBoxEdit, 
    /// добавляет методы проверки и установки статуса IsSelected элементов
    /// </summary>
    public static class XtraEditors
    {
        /// <summary>
        /// Проверяет, что визуальный элемент, содержащий значение из DataSource, выделен
        /// </summary>
        /// <param name="combo">
        /// Редактор
        /// </param>
        /// <param name="value">
        /// Значение элемента
        /// </param>
        /// <returns>
        /// True
        /// </returns>
        public static bool IsCheckedItem(this CheckedComboBoxEdit combo, object value)
        {
            var items = combo.Properties.DataSource as IList;
            if (items == null)
                return false;

            var index = items.IndexOf(value);
            if (combo.Properties.Items.Count < index + 1)
                return false;
            return combo.Properties.Items[index].CheckState == CheckState.Checked;
        }

        /// <summary>
        /// Получает список элеметов из DataSource, которые выделены в редакторе
        /// </summary>
        /// <param name="combo">
        /// Редактор
        /// </param>
        /// <typeparam name="T">
        /// Тип элеметов в DataSource
        /// </typeparam>
        /// <returns>
        /// Списко выделенных элементов
        /// </returns>
        public static List<T> GetCheckedItems<T>(this CheckedComboBoxEdit combo)
        {
            var result = new List<T>();

            var items = combo.Properties.DataSource as IList;
            if (items == null)
                return result;
            result.AddRange(items.OfType<T>().Where(i => IsCheckedItem(combo, i)));
            return result;
        }

        /// <summary>
        /// Устанавливает элементы из DataSource в выделенные
        /// </summary>
        /// <param name="combo">
        /// Редактор
        /// </param>
        /// <param name="valueList">
        /// Список элементов, которые нужно выделить
        /// </param>
        public static void SetCheckedItems(this CheckedComboBoxEdit combo, IList valueList)
        {
            combo.Properties.GetItems();
            var items = combo.Properties.DataSource as IList;
            if (items == null)
                return;

            foreach (var item in from object item in items from j in (from object j in valueList where item.AreEqual(j) select j) select item)
                combo.Properties.Items[items.IndexOf(item)].CheckState = CheckState.Checked;
        }
    }
}
