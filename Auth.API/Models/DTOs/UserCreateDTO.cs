using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.DTOs
{
    public class UserCreateDTO
    {

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [MaxLength(20, ErrorMessage = "O campo não pode passar de 20 caracteres")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [EmailAddress(ErrorMessage = "Informe um Email válido")]
        [MaxLength(50, ErrorMessage = "O campo não pode passar de 50 caracteres")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [MaxLength(30, ErrorMessage = "O campo não pode passar de 30 caracteres")]
        public string Password { get; set; } = null!;
    }
}
