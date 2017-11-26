using System;
using System.ComponentModel.DataAnnotations;

namespace HeadHunterTest.Domain.Users.Models
{
    /// <summary>
    /// Модель для регистрации соискателя
    /// </summary>
    public class JobSeekerRegisterModel : UserRegisterModel
    {
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Гражданство
        /// </summary>
        [Required]
        public string Citizenship { get; set; }

        /// <summary>
        /// True=мальчик
        /// </summary>
        public bool Sex { get; set; }
    }
}
