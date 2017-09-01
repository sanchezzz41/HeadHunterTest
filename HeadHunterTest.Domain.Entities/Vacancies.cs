using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий вакансию от работодателя
    /// </summary>
    public class Vacancies
    {
        /// <summary>
        /// Id вакансии
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Описание вакансии
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Id города, в котором размещена вакансия
        /// </summary>
        [ForeignKey(nameof(VacanciesInCity))]
        public Guid CityId { get; set; }
                            
        /// <summary>
        /// Город, в котором размещена вакансия
        /// </summary>
        public City VacanciesInCity { get; set; }

        /// <summary>
        /// Id работодателя
        /// </summary>
        [ForeignKey(nameof(Employer))]
        public Guid EmployerId { get; set; }

        /// <summary>
        /// Владелец вакансии(работодатель)
        /// </summary>
        public Employer Employer { get; set; }
    }
}
