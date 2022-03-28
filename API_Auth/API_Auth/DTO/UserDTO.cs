using System.Text.Json.Serialization;

namespace API_Auth.DTO
{
    public class UserDTO
    {

        public int User_Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRoleDTO>? UserRoles { get; set; }
    }
}
