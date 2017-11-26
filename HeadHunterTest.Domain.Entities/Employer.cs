using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Работодатель
    /// </summary>
    public class Employer : User
    {
        /// <summary>
        /// Название компании
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string NameOfCompany { get; set; }

        /// <summary>
        /// Сайт компании
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Site { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        /// <summary>
        /// Список вакансий
        /// </summary>
        public virtual List<Vacancy> Vacancieses { get; set; }

        public Employer()
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
        /// <param name="nameCompany">Название компании</param>
        /// <param name="webSite">Веб сайт</param>
        /// <param name="address"></param>
        public Employer(string name, string email, string phoneNumber, string passwordSalt,
            string passwordHash, Guid idCity, string nameCompany, string webSite,string address)
            : base(name, email, phoneNumber,
                passwordSalt, passwordHash, RolesOptions.Employer, idCity)
        {
            NameOfCompany = nameCompany;
            Site = webSite;
            Address = address;
        }
    }
}
