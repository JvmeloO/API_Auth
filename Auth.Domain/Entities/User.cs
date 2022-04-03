using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(20)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
