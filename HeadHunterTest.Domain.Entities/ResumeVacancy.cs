using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Класс связывающий заявку и вакансию
    /// </summary>
    public class ResumeVacancy
    {

        /// <summary>
        /// Id резюме
        /// </summary>
        [ForeignKey(nameof(Resume))]
        public Guid ResumeId { get; set; }

        public Resume Resume { get; set; }

        /// <summary>
        /// Id вакансии
        /// </summary>
        [ForeignKey(nameof(Vacancy))]
        public Guid VacancyId { get; set; }

        public Vacancy Vacancy { get; set; }

        public ResumeVacancy()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idResume">Id резюме</param>
        /// <param name="idVacancy">Id вакансии</param>
        public ResumeVacancy(Guid idResume,Guid idVacancy)
        {
            ResumeId = idResume;
            VacancyId = idVacancy; 
        }
    }
}
