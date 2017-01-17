// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditValueStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Стратегия применения значений parent редактору
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.DevExpress.UI.Components
{
    /// <summary>
    /// Стратегия применения значений parent редактору
    /// </summary>
    public enum EditValueStrategy
    {
        /// <summary>
        /// Зависти от того, в каким образовм закрывается контрол
        /// </summary>
        Hybrid = 0,

        /// <summary>
        /// Значение в поле фильтра
        /// </summary>
        FilterEditValue = 1,

        /// <summary>
        /// Выбрыннй объект из строки таблицы
        /// </summary>
        GridRowObject = 2,

        /// <summary>
        /// Текстовое представление объекта из строки таблицы
        /// </summary>
        GridRowText = 3
    }
}