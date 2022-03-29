using System.ComponentModel.DataAnnotations;

namespace API_Auth.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(20)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

        
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
