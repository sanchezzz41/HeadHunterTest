using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Класс связывающий заявку и вакансию
    /// </summary>
    public class ResumeVacancy
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

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
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idResume">Id резюме</param>
        /// <param name="idVacancy">Id вакансии</param>
        public ResumeVacancy(Guid idResume,Guid idVacancy)
        {
            Id=Guid.NewGuid();
            ResumeId = idResume;
            VacancyId = idVacancy; 
        }
    }
}
