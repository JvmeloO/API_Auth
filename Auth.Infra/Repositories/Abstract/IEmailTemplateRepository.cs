using Auth.Domain.Entities;

namespace Auth.Infra.Repositories.Abstract
{
    public interface IEmailTemplateRepository
    {
        EmailTemplate GetEmailTemplateByTemplateName(string templateName);
        void InsertEmailTemplate(EmailTemplate emailTemplate);
        void DeleteEmailTemplate(int emailTemplateId);
        void UpdateEmailTemplate(EmailTemplate emailTemplate);
        void Save();
    }
}
