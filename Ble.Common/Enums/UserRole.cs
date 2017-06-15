namespace Ble.Common.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Роли пользователй
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Обычный пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        User,

        /// <summary>
        /// Администратор
        /// </summary>
        [Display(Name = "Администратор")]
        Administrator
    }
}