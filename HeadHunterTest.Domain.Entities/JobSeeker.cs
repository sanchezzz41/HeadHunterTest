using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Соискатель
    /// </summary>
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
