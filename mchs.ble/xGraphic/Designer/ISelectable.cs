
namespace xGraphic
{
    /// <summary>
    /// Основной интерфейс для графических элементов, которые могут выделяться Используется в DesignerItem
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Является ли элемент выделенным
        /// </summary>
        bool IsSelected { get; set; }
    }
}
