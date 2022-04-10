using Auth.Business.Services.Abstract;
using Auth.Infra.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Business.Services.Concrete
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private readonly IEmailSentRepository _emailSentRepository;
        private readonly ISendEmailService _sendEmailService;
        private readonly IKeyCodeService _keyCodeService;

        private readonly string _senderEmail;
        private readonly string _senderEmailPassword;
        private readonly string _templateName;

        private readonly int sizeVerificationCode = 10;

        public PasswordRecoveryService(IEmailSentRepository emailSentRepository, ISendEmailService sendEmailService, 
            IKeyCodeService keyCodeService, IConfiguration configuration)
        {
            _emailSentRepository = emailSentRepository;
            _sendEmailService = sendEmailService;
            _keyCodeService = keyCodeService;
            _senderEmail = configuration.GetSection("PasswordRecovery:SenderEmail").Value;
            _senderEmailPassword = configuration.GetSection("PasswordRecovery:SenderEmailPassword").Value;
            _templateName = configuration.GetSection("PasswordRecovery:TemplateName").Value;
        }

        public void SendEmailVerificationCode(string recipientEmail)
        {
            var verificationCode = _keyCodeService.GenerateKeyCode(sizeVerificationCode);

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
    }
}
