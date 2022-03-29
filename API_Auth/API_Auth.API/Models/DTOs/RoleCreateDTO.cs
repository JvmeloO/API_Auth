using System.ComponentModel.DataAnnotations;

namespace API_Auth.API.Models.DTOs
{
    public class RoleCreateDTO
    {
        [MaxLength(20)]
        public string RoleName { get; set; }
    }
}
