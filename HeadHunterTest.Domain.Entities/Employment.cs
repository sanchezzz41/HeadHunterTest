using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Занятость
    /// </summary>
    public class Employment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public EmploymentOption EmploymentId { get; set; }


        [Required]
        public string Name { get; set; }

        public Employment()
        {
            
        }

        public Employment(EmploymentOption emploumentId, string name)
        {
            EmploymentId = emploumentId;
            Name = name;
        }
    }
}
