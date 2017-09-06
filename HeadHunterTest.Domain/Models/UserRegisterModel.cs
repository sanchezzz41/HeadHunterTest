using System;
using System.ComponentModel.DataAnnotations;
using HeadHunterTest.Domain.Entities;

namespace HeadHunterTest.Domain.Models
{
    /// <summary>
    /// Модель для регистрации пользователя
    /// </summary>
    public class UserRegisterModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [Required]
        public string SurName { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }


        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Роль, к которой принадлежит пользователь
        /// </summary>
        public RolesOptions RoleId { get; set; }

        /// <summary>
        /// Название города
        /// </summary>
        public string NameCity { get; set; }
    }
}
