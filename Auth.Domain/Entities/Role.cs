using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }

        [MaxLength(20)]
        public string RoleName { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
