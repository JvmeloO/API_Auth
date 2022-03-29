using System.ComponentModel.DataAnnotations;

namespace API_Auth.API.Models.DTOs
{
    public class UserRolesDTO
    {
        [MaxLength(20)]
        public string Username { get; set; }

        public List<int> RoleId { get; set; }
    }
}
