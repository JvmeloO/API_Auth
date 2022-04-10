using Auth.Domain.Entities;

namespace Auth.Infra.Repositories.Abstract
{
    public interface IEmailSentRepository
    {
        EmailSent GetLastEmailSentByRecipientEmailAndEmailTemplateId(string recipientEmail, int emailTemplateId);
        void InsertEmailSent(EmailSent emailSent);
        void Save();
    }
}
