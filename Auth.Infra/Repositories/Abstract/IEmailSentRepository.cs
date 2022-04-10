using Auth.Domain.Entities;

namespace Auth.Infra.Repositories.Abstract
{
    public interface IEmailSentRepository
    {
        EmailSent GetLastEmailSentByRecipientEmailAndTemplateName(string recipientEmail, string templateName);
        void InsertEmailSent(EmailSent emailSent);
        void Save();
    }
}
