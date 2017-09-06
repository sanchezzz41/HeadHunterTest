using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий вакансию от работодателя
    /// </summary>
    public class Vacancy
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

        /// <summary>
        /// Связывыает вакансию и резюме
        /// </summary>
        public virtual List<ResumeVacancy> ResumeVacancies { get; set; }

        public Vacancy()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Иницилизирует новую вакансию
        /// </summary>
        /// <param name="employerId">Id работодателя, который размещаюет вакансию</param>
        /// <param name="cityId">Id города, в котором размещается вакансия </param>
        /// <param name="description">Описание вакансии</param>
        public Vacancy(Guid employerId, Guid cityId, string description)
        {
            Id = Guid.NewGuid();
            EmployerId = employerId;
            CityId = cityId;
            Description = description;
        }
    }
}
