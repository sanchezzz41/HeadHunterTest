using System;
using System.ComponentModel.DataAnnotations;

namespace HeadHunterTest.Domain.Models
{
    /// <summary>
    /// Модель для резюме
    /// </summary>
    public class ResumeModel
    {
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
        /// Id проф. области
        /// </summary>
        [Required]
        public Guid ProfAreaId { get; set; }

        /// <summary>
        /// Id города, в котором размещено резюме
        /// </summary>
        [Required]
        public Guid CityId { get; set; }
    }
}
