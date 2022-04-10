using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.DTOs
{
    public class EmailTemplateCreateDTO
    {
        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [MaxLength(20, ErrorMessage = "O campo não pode passar de 20 caracteres")]
        public string TemplateName { get; set; } = null!;

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [MaxLength(1000, ErrorMessage = "O campo não pode passar de 1000 caracteres")]
        public string EmailSubject { get; set; } = null!;

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        [MaxLength(50000, ErrorMessage = "O campo não pode passar de 50000 caracteres")]
        public string Content { get; set; } = null!;

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        public bool ContentIsHtml { get; set; }

        [Required(ErrorMessage = "O campo não pode ser nulo")]
        public int EmailTypeId { get; set; }
    }
}
