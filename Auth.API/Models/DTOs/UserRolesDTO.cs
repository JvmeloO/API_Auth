using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.DTOs
{
    public class UserRolesDTO
    {
        [MaxLength(20)]
        public string Username { get; set; }

        public List<int> RolesIds { get; set; }
    }
}
