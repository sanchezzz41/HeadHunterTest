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
        public Guid VacancyGuid { get; set; }

        /// <summary>
        /// Id работодателя
        /// </summary>
        [ForeignKey(nameof(Employer))]
        public Guid EmployerId { get; set; }

        /// <summary>
        /// Id города, в котором размещено резюме
        /// </summary>
        [ForeignKey(nameof(VacanciesInCity))]
        public Guid CityGuid { get; set; }

        /// <summary>
        /// Id проф. области
        /// </summary>
        [ForeignKey(nameof(ProfessionalArea))]
        public Guid ProfAreaGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(Employment))]
        public EmploymentOption EmploymentId { get; set; }

        /// <summary>
        /// Зарплата
        /// </summary>
        [Required]
        public decimal Salary { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Position { get; set; }

        /// <summary>
        /// Опыт работы
        /// </summary>
        public double WorkExpirience { get; set; }

        /// <summary>
        /// Время создания резюме
        /// </summary>
        public DateTimeOffset DateVacancy { get; set; }

        /// <summary>
        /// Описание вакансии
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        /// <summary>
        /// Проф. область
        /// </summary>
        public virtual ProfessionalArea ProfessionalArea { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Employment Employment { get; set; }


        /// <summary>
        /// Город, в котором размещена вакансия
        /// </summary>
        public virtual City VacanciesInCity { get; set; }


        /// <summary>
        /// Владелец вакансии(работодатель)
        /// </summary>
        public virtual Employer Employer { get; set; }

        /// <summary>
        /// Связывыает вакансию и резюме
        /// </summary>
        public virtual List<Note> ResumeVacancies { get; set; }

        public Vacancy()
        {
            VacancyGuid = Guid.NewGuid();
        }

        public Vacancy(Guid employerId, Guid cityGuid, Guid profAreaGuid, EmploymentOption employmentId, decimal salary,
            string position, double workExpirience, string description, string phone)
        {
            VacancyGuid = Guid.NewGuid();
            EmployerId = employerId;
            CityGuid = cityGuid;
            ProfAreaGuid = profAreaGuid;
            EmploymentId = employmentId;
            Salary = salary;
            Position = position;
            WorkExpirience = workExpirience;
            DateVacancy = DateTimeOffset.Now;
            Description = description;
            Phone = phone;
        }
    }
}
