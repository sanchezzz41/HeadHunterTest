using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Класс представляющий город
    /// </summary>
    public class City
    {
        /// <summary>
        /// Id города
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        /// <summary>
        /// Название города
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Список пользователей в текущем городе
        /// </summary>
        public virtual List<User> Users { get; set; }

        /// <summary>
        /// Список вакансий в текущем городе
        /// </summary>
        public virtual List<Vacancies> Vacancieses { get; set; }

        /// <summary>
        /// Список резюме в текущем городе
        /// </summary>
        public virtual List<Resume> Resumes { get; set; }
    }
}
