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
        public virtual List<Vacancies> Vacancieses { get; set; }
    }
}
