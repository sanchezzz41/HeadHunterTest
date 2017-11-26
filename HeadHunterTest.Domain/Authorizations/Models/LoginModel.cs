
namespace HeadHunterTest.Domain.Authorizations.Models
{
    /// <summary>
    /// Модель для входа
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
