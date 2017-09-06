using System;

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
        /// Id города, в котором размещается вакансия
        /// </summary>
        public Guid CityId { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
    }
}
