using System.Text.Json.Serialization;

namespace API_Auth.DTO
{
    public class RoleDTO
    {
        public int Role_Id { get; set; }

        public string Role_Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRoleDTO>? UserRoles { get; set; }
    }
}
