using System.ComponentModel.DataAnnotations;

namespace HeadHunterTest.Domain.Users.Models
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

        [Required]
        public string Address { get; set; }
    }
}
