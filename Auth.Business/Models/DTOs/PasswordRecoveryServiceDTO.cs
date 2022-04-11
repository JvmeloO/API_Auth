using Auth.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Auth.Business.Models.DTOs
{
    public class PasswordRecoveryServiceDTO
    {
        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [EmailAddress(ErrorMessage = "Informe um Email válido")]
        [MaxLength(50, ErrorMessage = "O campo não pode passar de 50 caracteres")]
        public string Email { get; set; } = null!;
    }

    public class PasswordRecoveryNewPasswordServiceDTO : PasswordRecoveryServiceDTO
    {
        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [MaxLength(10, ErrorMessage = "O campo não pode passar de 10 caracteres")]
        public string VerificationCode { get; set; } = null!;

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [MaxLength(30, ErrorMessage = "O campo não pode passar de 30 caracteres")]
        public string NewPassword { get; set; } = null!;
    }
}
