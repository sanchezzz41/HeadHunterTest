﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Класс представляющий пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

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
        /// Cоль для пароля
        /// </summary>
        [Required]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Роль, к которой принадлежит пользователь
        /// </summary>
        [ForeignKey(nameof(Role))]
        public RolesOption RoleId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Id города
        /// </summary>
        [ForeignKey(nameof(City))]
        public Guid IdCity { get; set; }

        /// <summary>
        /// Город,в котором совершаются действия
        /// </summary>
        public City City { get; set; }

        public User()
        {
            Id=Guid.NewGuid();
        }

        /// <summary>
        /// Иницилизация пользователя
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surName">Фамилия</param>
        /// <param name="email">Email</param>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <param name="passwordSalt">Солья для пароля</param>
        /// <param name="passwordHash">Хэш пароля</param>
        /// <param name="role">Роль</param>
        /// <param name="idCity">Id города</param>
        public User(string name, string surName, string email, string phoneNumber, string passwordSalt,
            string passwordHash,RolesOption role,Guid idCity)
        {
            Id=Guid.NewGuid();
            Name = name;
            SurName = surName;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            RoleId = role;
            IdCity = idCity;
        }
    }
}
