using Auth.Business.Services.Abstract;
using Auth.Infra.Repositories.Abstract;
using Auth.Infra.UnitOfWork.Abstract;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Business.Services.Concrete
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private readonly IEmailSentRepository _emailSentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendEmailService _sendEmailService;
        private readonly IKeyCodeService _keyCodeService;

        private readonly string _senderEmail;
        private readonly string _senderEmailPassword;
        private readonly string _templateName;

        private readonly int sizeVerificationCode = 10;
        private readonly int verificationCodeMinuteExpiration = 60;

        public PasswordRecoveryService(IEmailSentRepository emailSentRepository, IUnitOfWork unitOfWork, 
            ISendEmailService sendEmailService, IKeyCodeService keyCodeService, IConfiguration configuration)
        {
            _emailSentRepository = emailSentRepository;
            _unitOfWork = unitOfWork;
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
            var emailSent = _emailSentRepository.GetWithIncludeAndWhere(e => e.EmailTemplate, e => e.RecipientEmail == recipientEmail 
            && e.EmailTemplate.TemplateName == _templateName && e.SendDate.AddMinutes(verificationCodeMinuteExpiration) > DateTime.Now)
                .OrderByDescending(e => e.SendDate).FirstOrDefault();

            if (emailSent.VerificationCode == verificationCode && emailSent.ValidatedCode == false)
            {
                emailSent.ValidatedCode = true;
                _emailSentRepository.Update(emailSent);
                _unitOfWork.Save();
                return true;
            }

            return false;
        }
    }
}
