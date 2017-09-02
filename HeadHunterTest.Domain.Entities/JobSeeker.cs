using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeadHunterTest.Domain.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// Соискатель
    /// </summary>
    //[Table("JobSeekers")]
    public class JobSeeker : User
    {
        /// <summary>
        /// Дата рождения
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Гражданство
        /// </summary>
        [Required]
        public string Citizenship { get; set; }                                                                        

        /// <summary>
        /// Список резюме
        /// </summary>
        public virtual List<Resume> Resumes { get; set; }
    }
}
