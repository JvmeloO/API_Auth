using System.ComponentModel.DataAnnotations;

namespace API_Auth.Models.DTOs
{
    public class RoleCreateDTO
    {
        [MaxLength(20)]
        public string RoleName { get; set; }
    }
}
