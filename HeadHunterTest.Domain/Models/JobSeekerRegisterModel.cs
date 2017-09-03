using System;
using System.ComponentModel.DataAnnotations;

namespace HeadHunterTest.Domain.Models
{
    /// <summary>
    /// Модель для регистрации соискателя
    /// </summary>
    public class JobSeekerRegisterModel : UserRegisterModel
    {
        /// <summary>
        /// Дата рождения
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Гражданство
        /// </summary>
        [Required]
        public string Citizenship { get; set; }
    }
}
