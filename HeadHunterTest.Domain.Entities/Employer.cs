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
        public string NameCompany { get; set; }

        /// <summary>
        /// Сайт компании
        /// </summary>
        [Required]
        public string WebSite { get; set; }

        /// <summary>
        /// Список вакансий
        /// </summary>
        public virtual List<Vacancies> Vacancieses { get; set; }
    }
}
