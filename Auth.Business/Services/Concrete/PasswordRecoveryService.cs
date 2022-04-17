using Auth.Business.Models.DTOs;
using Auth.Business.Services.Abstract;
using Auth.Domain.Entities;
using Auth.Infra.Repositories.Abstract;
using Auth.Infra.UnitOfWork.Abstract;
using Microsoft.Extensions.Configuration;

namespace Auth.Business.Services.Concrete
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSentRepository _emailSentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendEmailService _sendEmailService;
        private readonly IKeyCodeService _keyCodeService;
        private readonly IEncryptService _encryptService;

        private readonly string _senderEmail;
        private readonly string _senderEmailPassword;
        private readonly string _templateName;

        private readonly int sizeVerificationCode = 10;
        private readonly int verificationCodeMinuteExpiration = 60;

        public PasswordRecoveryService(IUserRepository userRepository, IEmailSentRepository emailSentRepository, IUnitOfWork unitOfWork, 
            ISendEmailService sendEmailService, IKeyCodeService keyCodeService, IEncryptService encryptService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _emailSentRepository = emailSentRepository;
            _unitOfWork = unitOfWork;
            _sendEmailService = sendEmailService;
            _keyCodeService = keyCodeService;
            _encryptService = encryptService;
            _senderEmail = configuration.GetSection("PasswordRecovery:SenderEmail").Value;
            _senderEmailPassword = configuration.GetSection("PasswordRecovery:SenderEmailPassword").Value;
            _templateName = configuration.GetSection("PasswordRecovery:TemplateName").Value;
        }

        public void SendEmailVerificationCode(PasswordRecoveryServiceDTO passwordRecoveryServiceDTO)
        {
            var verificationCode = _keyCodeService.GenerateKeyCode(sizeVerificationCode);

            _sendEmailService.SendEmail(new SendEmailServiceDTO
            {
                SenderEmail = _senderEmail,
                SenderEmailPassword = _senderEmailPassword,
                RecipientEmail = passwordRecoveryServiceDTO.Email,
                VerificationCode = verificationCode,
                ValidatedCode = false,
                TemplateName = _templateName
            });
        }

        public void NewPassword(PasswordRecoveryNewPasswordServiceDTO passwordRecoveryNewPasswordServiceDTO, User user) 
        {
            var emailSent = _emailSentRepository.GetWithWhereAndIncludes(e => e.RecipientEmail == passwordRecoveryNewPasswordServiceDTO.Email
            && e.EmailTemplate.TemplateName == _templateName && e.SendDate.AddMinutes(verificationCodeMinuteExpiration) > DateTime.Now, e => e.EmailTemplate)
                .OrderByDescending(e => e.SendDate).FirstOrDefault();

            if (emailSent == null)
                throw new ApplicationException("Solicite outro código de verificação");

            if (emailSent.VerificationCode != passwordRecoveryNewPasswordServiceDTO.VerificationCode || emailSent.ValidatedCode == true)
                throw new ApplicationException("Código de verificação inválido");

            emailSent.ValidatedCode = true;
            _emailSentRepository.Update(emailSent);

            var newPasswordEncrypted = _encryptService.EncryptPassword(passwordRecoveryNewPasswordServiceDTO.NewPassword);
            user.Password = newPasswordEncrypted;
            _userRepository.Update(user);

            _unitOfWork.Save();
        }
    }
}
