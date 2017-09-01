using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
        public int Salary { get; set; }

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
    }
}
