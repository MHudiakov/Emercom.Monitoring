using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class LoginModel
    {
        [DisplayName("Имя")]
        [Required]
        public string UserLogin { get; set; }
        
        [Required]
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DisplayName("Локальный сервер")]
        public bool IsLocalServer { get; set; }
    }
}