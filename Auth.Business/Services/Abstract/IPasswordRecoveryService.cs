namespace Auth.Business.Services.Abstract
{
    public interface IPasswordRecoveryService
    {
        void SendEmailVerificationCode(string recipientEmail);
        bool ValidateCode(string recipientEmail, string verificationCode);
    }
}
