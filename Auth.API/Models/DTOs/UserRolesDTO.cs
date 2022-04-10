using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.DTOs
{
    public class UserRolesDTO
    {
        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [MaxLength(20, ErrorMessage = "O campo não pode passar de 20 caracteres")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        public List<int> RolesIds { get; set; } = null!;
    }
}
