using Auth.API.Models.DTOs;
using Auth.Domain.Entities;

namespace Auth.API.Models.Mappings
{
    public static class EmailTemplateMap
    {
        public static EmailTemplate CreateDTOToEntity(EmailTemplateCreateDTO emailTemplateCreateDTO) => new()
        {
            TemplateName = emailTemplateCreateDTO.TemplateName,
            Content = emailTemplateCreateDTO.Content,
            ContentIsHtml = emailTemplateCreateDTO.ContentIsHtml,
            EmailSubject = emailTemplateCreateDTO.EmailSubject,
            EmailTypeId = Convert.ToInt32(emailTemplateCreateDTO.EmailTypeId)
        };

        public static EmailTemplate UpdateDTOToEntity(EmailTemplateUpdateDTO emailTemplateUpdateDTO, EmailTemplate emailTemplate) => new()
        {
            TemplateName = emailTemplateUpdateDTO.TemplateName ?? emailTemplate.TemplateName,
            EmailSubject = emailTemplateUpdateDTO.EmailSubject ?? emailTemplate.EmailSubject,
            Content = emailTemplateUpdateDTO.Content ?? emailTemplate.Content,
            ContentIsHtml = emailTemplateUpdateDTO.ContentIsHtml ?? emailTemplate.ContentIsHtml,
            EmailTypeId = emailTemplateUpdateDTO.EmailTypeId != null ? Convert.ToInt32(emailTemplateUpdateDTO.EmailTypeId) : emailTemplate.EmailTypeId
        };
    }
}
