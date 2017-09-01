using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Класс предсталвяющий профессианальную область
    /// </summary>
    public class ProfessionalArea
    {
        /// <summary>
        /// Id проф. области
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Название проф. области
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Список резюме
        /// </summary>
        public virtual List<Resume> Resumes { get; set; }
    }
}
