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
        public Guid Id { get; set; }

        /// <summary>
        /// Зарплата
        /// </summary>
        [Required]
        public uint Salary { get; set; }

        /// <summary>
        /// Желаемая должность
        /// </summary>
        [Required]
        public string DesiredPosition { get; set; }

        /// <summary>
        /// Id соискателя
        /// </summary>
        [ForeignKey(nameof(JobSeeker))]
        public Guid JobSeekerId { get; set; }

        /// <summary>
        /// Владелец резюме(соискатель)
        /// </summary>
        public JobSeeker JobSeeker { get; set; }

        /// <summary>
        /// Id проф. области
        /// </summary>
        [ForeignKey(nameof(ProfessionalArea))]
        public Guid ProfAreaId { get; set; }

        /// <summary>
        /// Проф. область
        /// </summary>
        public ProfessionalArea ProfessionalArea { get; set; }

        /// <summary>
        /// Id города, в котором размещено резюме
        /// </summary>
        [ForeignKey(nameof(ResumeInCity))]
        public Guid CityId { get; set; }

        /// <summary>
        /// Город, в котором размещено резюме
        /// </summary>
        public City ResumeInCity { get; set; }

        /// <summary>
        /// Связывыает вакансию и резюме
        /// </summary>
        public virtual List<ResumeVacancy> ResumeVacancies { get; set; }

        public Resume()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idJobSeeker">Id соискателя</param>
        /// <param name="idCity">Id города</param>
        /// <param name="idProfArea">Id проф. области</param>
        /// <param name="salary">Желаемая зарплата</param>
        /// <param name="desiredPosition">Желаемая должность</param>
        public Resume(Guid idJobSeeker, Guid idCity, Guid idProfArea, uint salary, string desiredPosition)
        {
            Id = Guid.NewGuid();
            JobSeekerId = idJobSeeker;
            CityId = idCity;
            ProfAreaId = idProfArea;
            Salary = salary;
            DesiredPosition = desiredPosition;
        }
    }
}
