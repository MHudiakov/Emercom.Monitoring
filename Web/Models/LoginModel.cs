using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    /// <summary>
    /// Модель авторизации пользователя
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [DisplayName("Имя")]
        [Required]
        public string UserLogin { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}