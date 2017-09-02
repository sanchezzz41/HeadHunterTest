using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeadHunterTest.Domain.Entities
{
    /// <summary>
    /// Класс предостовляющий роль
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Id роли
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public RolesOption Id { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Список пользователей
        /// </summary>
        public virtual List<User> Users { get; set; }

        public Role()
        {

        }

        public Role(RolesOption id, string roleName)
        {
            Id = id;
            RoleName = roleName;
        }
    }
}
