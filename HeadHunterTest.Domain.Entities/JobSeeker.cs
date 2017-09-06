using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeadHunterTest.Domain.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// Соискатель
    /// </summary>
    public class JobSeeker : User
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

        /// <summary>
        /// Список резюме
        /// </summary>
        public virtual List<Resume> Resumes { get; set; }

        public JobSeeker()
        {

        }

        /// <inheritdoc />
        /// <summary>
        /// Иницилизация соискателя
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surName">Фамилия</param>
        /// <param name="email">Email</param>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <param name="passwordSalt">Солья для пароля</param>
        /// <param name="passwordHash">Хэш пароля</param>
        /// <param name="idCity">Id города</param>
        /// <param name="dateOfBirth">День рождения</param>
        /// <param name="citizenship">Гражданство</param>
        public JobSeeker(string name, string surName, string email, string phoneNumber, string passwordSalt,
            string passwordHash, Guid idCity, DateTime dateOfBirth, string citizenship)
            : base(name, surName, email, phoneNumber,
                passwordSalt, passwordHash, RolesOptions.JobSeeker, idCity)
        {
            DateOfBirth = dateOfBirth;
            Citizenship = citizenship;
        }
    }
}
