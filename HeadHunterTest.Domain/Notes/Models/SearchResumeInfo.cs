using System;
using System.ComponentModel.DataAnnotations;
using HeadHunterTest.Domain.Entities;

namespace HeadHunterTest.Domain.Notes.Models
{
    /// <summary>
    /// Модель для поиска резюме
    /// </summary>
   public  class SearchResumeInfo
    {
        [Required]
        public string SearchString { get; set; }

        public EmploymentOption? EmploymentOption { get; set; }

        public double? WorkExpirience { get; set; }

        public decimal? Salary { get; set; }

        public Guid? CityGuid { get; set; }

        public Guid? ProfAreaGuid { get; set; }

        public DateTimeOffset? CreatedTime { get; set; }
    }
}
