using System.ComponentModel.DataAnnotations;

namespace HeadHunterTest.Domain.Models
{
    public class EmployerRegisterModel : UserRegisterModel
    {
        /// <summary>
        /// Название компании
        /// </summary>
        [Required]
        public string NameCompany { get; set; }

        /// <summary>
        /// Сайт компании
        /// </summary>
        [Required]
        public string WebSite { get; set; }
    }
}
