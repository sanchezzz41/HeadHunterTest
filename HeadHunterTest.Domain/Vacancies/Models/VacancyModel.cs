using System;
using HeadHunterTest.Domain.Entities;

namespace HeadHunterTest.Domain.Vacancies.Models
{
    public class VacancyModel
    {
        /// <summary>
        /// Id вакансии
        /// </summary>
        public Guid VacancyGuid { get; set; }

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
        public decimal Salary { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
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
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        public VacancyModel(Vacancy vacancy)
        {
            VacancyGuid = vacancy.VacancyGuid;
            EmployerId = vacancy.EmployerId;
            CityGuid = vacancy.CityGuid;
            ProfAreaGuid = vacancy.ProfAreaGuid;
            EmploymentId = vacancy.EmploymentId;
            Salary = vacancy.Salary;
            Position = vacancy.Position;
            WorkExpirience = vacancy.WorkExpirience;
            DateVacancy = vacancy.DateVacancy;
            Description = vacancy.Description;
            Phone = vacancy.Phone;
        }
    }
}
