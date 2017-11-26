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
        public DateTimeOffset DateOfBirth { get; set; }

        /// <summary>
        /// Гражданство
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Citizenship { get; set; }

        /// <summary>
        /// True = мужчина, flase = женщина
        /// </summary>
        public bool Sex { get; set; }

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
        /// <param name="email">Email</param>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <param name="passwordSalt">Солья для пароля</param>
        /// <param name="passwordHash">Хэш пароля</param>
        /// <param name="idCity">Id города</param>
        /// <param name="dateOfBirth">День рождения</param>
        /// <param name="citizenship">Гражданство</param>
        /// <param name="isBoy"></param>
        public JobSeeker(string name, string email, string phoneNumber, string passwordSalt,
            string passwordHash, Guid idCity, DateTimeOffset dateOfBirth, string citizenship,bool isBoy)
            : base(name, email, phoneNumber,
                passwordSalt, passwordHash, RolesOptions.JobSeeker, idCity)
        {
            DateOfBirth = dateOfBirth;
            Citizenship = citizenship;
            Sex = isBoy;
        }
    }
}
