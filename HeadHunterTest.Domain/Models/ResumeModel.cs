using System;
using System.ComponentModel.DataAnnotations;
using HeadHunterTest.Domain.Entities;

namespace HeadHunterTest.Domain.Models
{
    /// <summary>
    /// Модель для резюме
    /// </summary>
    public class ResumeModel
    {
        /// <summary>
        /// Id соискателя
        /// </summary>
        public Guid JobSeekerGuid { get; set; }

        /// <summary>
        /// Id города, в котором размещено резюме
        /// </summary>
        public Guid CityGuid { get; set; }

        /// <summary>
        /// Id проф. области
        /// </summary>
        public Guid ProfAreaGuid { get; set; }

        /// <summary>
        /// Занятость
        /// </summary>
        public EmploymentOption EmploymentId { get; set; }

        /// <summary>
        /// Зарплата
        /// </summary>
        [Required]
        public decimal Salary { get; set; }

        /// <summary>
        /// Желаемая должность
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Position { get; set; }

        /// <summary>
        /// Опыт работы
        /// </summary>
        public double WorkExpirience { get; set; }

        /// <summary>
        /// Описание резюме
        /// </summary>
        [Required]
        public string Description { get; set; }
    }
}
