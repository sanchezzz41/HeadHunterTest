using System;
using HeadHunterTest.Domain.Entities;

namespace HeadHunterTest.Domain.Resumes.Models
{
    /// <summary>
    /// Возвращаемая модель
    /// </summary>
    public class ResumeModel
    {
        /// <summary>
        /// Id резюме
        /// </summary>
        public Guid ResumeGuid { get; set; }

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

        public EmploymentOption EmploymentId { get; set; }

        /// <summary>
        /// Зарплата
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Желаемая должность
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Опыт работы
        /// </summary>
        public double WorkExpirience { get; set; }

        /// <summary>
        /// Время создания резюме
        /// </summary>
        public DateTimeOffset DateResume { get; set; }

        /// <summary>
        /// Описание резюме
        /// </summary>
        public string Description { get; set; }

        public ResumeModel(Resume resume)
        {
            ResumeGuid = resume.ResumeGuid;
            JobSeekerGuid = resume.JobSeekerGuid;
            CityGuid = resume.CityGuid;
            ProfAreaGuid = resume.ProfAreaGuid;
            EmploymentId = resume.EmploymentId;
            Salary = resume.Salary;
            Position = resume.Position;
            WorkExpirience = resume.WorkExpirience;
            DateResume = resume.DateResume;
            Description = resume.Description;
        }
    }
}
