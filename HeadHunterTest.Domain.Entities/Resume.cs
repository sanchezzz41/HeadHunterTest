using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Класс предоставляющий резюме для соискателя
    /// </summary>
    public class Resume
    {
        /// <summary>
        /// Id резюме
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ResumeGuid { get; set; }

        /// <summary>
        /// Id соискателя
        /// </summary>
        [ForeignKey(nameof(JobSeeker))]
        public Guid JobSeekerGuid { get; set; }

        /// <summary>
        /// Id города, в котором размещено резюме
        /// </summary>
        [ForeignKey(nameof(ResumeInCity))]
        public Guid CityGuid { get; set; }

        /// <summary>
        /// Id проф. области
        /// </summary>
        [ForeignKey(nameof(ProfessionalArea))]
        public Guid ProfAreaGuid { get; set; }

        [ForeignKey(nameof(Employment))]
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
        /// Время создания резюме
        /// </summary>
        public DateTimeOffset DateResume { get; set; }

        /// <summary>
        /// Описание резюме
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        
        /// <summary>
        /// Владелец резюме(соискатель)
        /// </summary>
        public virtual JobSeeker JobSeeker { get; set; }

        /// <summary>
        /// Проф. область
        /// </summary>
        public virtual ProfessionalArea ProfessionalArea { get; set; }


        /// <summary>
        /// Город, в котором размещено резюме
        /// </summary>
        public virtual City ResumeInCity { get; set; }

        public virtual Employment Employment { get; set; }
        
        /// <summary>
        /// Связывыает вакансию и резюме
        /// </summary>
        public virtual List<Note> ResumeVacancies { get; set; }

        public Resume()
        {
        }

        public Resume(Guid jobSeekerGuid, Guid cityGuid, Guid profAreaGuid, EmploymentOption employmentId, decimal salary,
            string position, double workExpirience, string description)
        {
            ResumeGuid = Guid.NewGuid();
            JobSeekerGuid = jobSeekerGuid;
            CityGuid = cityGuid;
            ProfAreaGuid = profAreaGuid;
            EmploymentId = employmentId;
            Salary = salary;
            Position = position;
            WorkExpirience = workExpirience;
            DateResume = DateTimeOffset.Now;
            Description = description;
        }
    }
}
