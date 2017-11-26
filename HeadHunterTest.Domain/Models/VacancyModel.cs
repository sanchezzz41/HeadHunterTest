using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HeadHunterTest.Domain.Entities;

namespace HeadHunterTest.Domain.Models
{
    /// <summary>
    /// Модель для вакансии
    /// </summary>
    public class VacancyModel
    {
        /// <summary>
        /// Id работодателя
        /// </summary>
        public Guid EmployerId { get; set; }

        /// <summary>
        /// Id города, в котором размещено резюме
        /// </summary>
        public Guid CityGuid { get; set; }

        /// <summary>
        /// Id проф. области
        /// </summary>
        public Guid ProfAreaGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
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
        public string Position { get; set; }

        /// <summary>
        /// Опыт работы
        /// </summary>
        public double WorkExpirience { get; set; }

        /// <summary>
        /// Описание вакансии
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Phone { get; set; }
    }
}
