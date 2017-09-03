using System;
using System.Collections.Generic;
using System.Text;

namespace HeadHunterTest.Domain.Models
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
