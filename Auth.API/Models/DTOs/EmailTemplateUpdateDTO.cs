using System.ComponentModel.DataAnnotations;

namespace Auth.API.Models.DTOs
{
    public class EmailTemplateUpdateDTO
    {
        [MaxLength(20, ErrorMessage = "O campo não pode passar de 20 caracteres")]
        public string? TemplateName { get; set; }

        [MaxLength(1000, ErrorMessage = "O campo não pode passar de 1000 caracteres")]
        public string? EmailSubject { get; set; }

        [MaxLength(400000, ErrorMessage = "O campo não pode passar de 400.000 caracteres")]
        public string? Content { get; set; }

        public bool? ContentIsHtml { get; set; }

        public string? EmailTypeId { get; set; }
    }
}
