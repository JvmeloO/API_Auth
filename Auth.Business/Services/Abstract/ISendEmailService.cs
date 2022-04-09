namespace Auth.Business.Services.Abstract
{
    public interface ISendEmailService
    {
        void SendEmail(int emailType, string senderEmail, string senderEmailPassword, string recipientEmail,
            string subjectEmail, string content, bool contentIsHtml, string? verificationCode, bool? validatedCode);
    }
}
