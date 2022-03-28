using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API_Auth.DTO
{
    public class UserDTO
    {

        public int User_Id { get; set; }

        [MaxLength(20)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRoleDTO>? UserRoles { get; set; }
    }
}
