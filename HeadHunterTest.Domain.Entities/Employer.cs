using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Работодатель
    /// </summary>
    //[Table("Employers")]
    public class Employer : User
    {
        /// <summary>
        /// Название компании
        /// </summary>
        [Required]
        public string NameCompany { get; set; }

        /// <summary>
        /// Сайт компании
        /// </summary>
        [Required]
        public string WebSite { get; set; }

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
        /// <param name="surName">Фамилия</param>
        /// <param name="email">Email</param>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <param name="passwordSalt">Солья для пароля</param>
        /// <param name="passwordHash">Хэш пароля</param>
        /// <param name="idCity">Id города</param>
        /// <param name="nameCompany">Название компании</param>
        /// <param name="webSite">Веб сайт</param>
        public Employer(string name, string surName, string email, string phoneNumber, string passwordSalt,
            string passwordHash, Guid idCity, string nameCompany, string webSite)
            : base(name, surName, email, phoneNumber,
                passwordSalt, passwordHash, RolesOptions.Employer, idCity)
        {
            NameCompany = nameCompany;
            WebSite = webSite;
        }
    }
}
