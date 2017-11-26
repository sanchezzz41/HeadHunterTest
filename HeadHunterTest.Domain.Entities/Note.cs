using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Заметка
    /// </summary>
    public class Note
    {

        /// <summary>
        /// Id резюме
        /// </summary>
        [ForeignKey(nameof(Resume))]
        public Guid ResumeGuid { get; set; }

        /// <summary>
        /// Id вакансии
        /// </summary>
        [ForeignKey(nameof(Vacancy))]
        public Guid VacancyGuid { get; set; }

        /// <summary>
        /// Добавил ли эту заметку работодатель
        /// </summary>
        public bool IsEmployer { get; set; }

        /// <summary>
        /// Время создания заметки
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }

        public Resume Resume { get; set; }

        public Vacancy Vacancy { get; set; }

        public Note()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idResume">Id резюме</param>
        /// <param name="idVacancy">Id вакансии</param>
        /// <param name="isEmployer"></param>
        public Note(Guid idResume,Guid idVacancy,bool isEmployer)
        {
            ResumeGuid = idResume;
            VacancyGuid = idVacancy;
            IsEmployer = isEmployer;
            CreatedTime=DateTimeOffset.Now;
        }
    }
}
