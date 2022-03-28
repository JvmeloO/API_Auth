using System.Text.Json.Serialization;

namespace API_Auth.DTO
{
    public class UserRoleDTO
    {
        public int User_Id { get; set; }

        public int Role_Id { get; set; }

        [JsonIgnore]
        public virtual UserDTO? User { get; set; }

        [JsonIgnore]
        public virtual RoleDTO? Role { get; set; }
    }
}
