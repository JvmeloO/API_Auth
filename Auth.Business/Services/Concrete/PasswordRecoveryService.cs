using Auth.Business.Services.Abstract;
using Auth.Infra.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Business.Services.Concrete
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private readonly ISendEmailService _sendEmailService;
        private readonly IEmailSentRepository _emailSentRepository;

        private readonly string _senderEmail;
        private readonly string _senderEmailPassword;
        private readonly string _templateName;

        private readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        private readonly int sizeVerificationCode = 10;

        public PasswordRecoveryService(ISendEmailService sendEmailService, IEmailSentRepository emailSentRepository, 
            IConfiguration configuration)
        {
            _sendEmailService = sendEmailService;
            _emailSentRepository = emailSentRepository;
            _senderEmail = configuration.GetSection("PasswordRecovery:SenderEmail").Value;
            _senderEmailPassword = configuration.GetSection("PasswordRecovery:SenderEmailPassword").Value;
            _templateName = configuration.GetSection("PasswordRecovery:TemplateName").Value;
        }

        public void SendEmailVerificationCode(string recipientEmail)
        {
            var verificationCode = GenerateVerificationCode(sizeVerificationCode);

            _sendEmailService.SendEmail(_senderEmail, _senderEmailPassword, recipientEmail,
                verificationCode, false, _templateName);
        }

        public bool ValidateCode(string recipientEmail, string verificationCode) 
        {
            var emailSent = _emailSentRepository.GetLastEmailSentByRecipientEmailAndTemplateName(recipientEmail, _templateName);

            if (verificationCode == emailSent.VerificationCode && emailSent.ValidatedCode == false
                && emailSent.SendDate.AddHours(1) > DateTime.Now)
            {
                _emailSentRepository.UpdateVerificationCodeValidatedEmailSentByEmailSendId(emailSent.EmailSentId);
                _emailSentRepository.Save();
                return true;
            }

            return false;
        }

        private string GenerateVerificationCode(int size) 
        {
            var data = new byte[4 * size];

            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }

            var result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();

        }
    }
}
