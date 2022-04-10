namespace Auth.Business.Services.Abstract
{
    public interface ISendEmailService
    {
        void SendEmail(string senderEmail, string senderEmailPassword, string recipientEmail,
            string? verificationCode, bool? validatedCode, string templateName);
    }
}
