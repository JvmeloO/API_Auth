using System.ComponentModel.DataAnnotations;

namespace Auth.Business.Models.DTOs
{
    public class SendEmailServiceDTO
    {
        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [EmailAddress(ErrorMessage = "Informe um Email válido")]
        [MaxLength(50, ErrorMessage = "O campo não pode passar de 50 caracteres")]
        public string SenderEmail { get; set; } = null!;

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [MaxLength(100, ErrorMessage = "O campo não pode passar de 100 caracteres")]
        public string SenderEmailPassword { get; set; } = null!;

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [EmailAddress(ErrorMessage = "Informe um Email válido")]
        [MaxLength(50, ErrorMessage = "O campo não pode passar de 50 caracteres")]
        public string RecipientEmail { get; set; } = null!;

        [MaxLength(10, ErrorMessage = "O campo não pode passar de 10 caracteres")]
        public string? VerificationCode { get; set; }

        public bool? ValidatedCode { get; set; }

        [MaxLength(1000, ErrorMessage = "O campo não pode passar de 1000 caracteres")]
        public string? Link { get; set; }

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [MaxLength(20, ErrorMessage = "O campo não pode passar de 20 caracteres")]
        public string TemplateName { get; set; } = null!;
    }
}
