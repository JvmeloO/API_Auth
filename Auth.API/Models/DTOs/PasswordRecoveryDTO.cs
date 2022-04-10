using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.DTOs
{
    public class PasswordRecoveryDTO
    {
        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [EmailAddress(ErrorMessage = "Informe um Email válido")]
        [MaxLength(50, ErrorMessage = "O campo não pode passar de 50 caracteres")]
        public string Email { get; set; } = null!;
    }

    public class PasswordRecoveryVerificationCodeDTO : PasswordRecoveryDTO
    {
        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [MaxLength(10, ErrorMessage = "O campo não pode passar de 10 caracteres")]
        public string VerificationCode { get; set; } = null!;
    }
}
