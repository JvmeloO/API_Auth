using System.Net;
using System.Net.Mail;
using Auth.Business.Services.Abstract;
using Auth.Domain.Entities;
using Auth.Infra.Repositories.Abstract;
using Auth.Infra.UnitOfWork.Abstract;

namespace Auth.Business.Services.Concrete
{
    public class SendEmailService : ISendEmailService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IEmailSentRepository _emailSentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SendEmailService(IEmailTemplateRepository emailTemplateRepository, IUnitOfWork unitOfWork,
            IEmailSentRepository emailSentRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _unitOfWork = unitOfWork;
            _emailSentRepository = emailSentRepository;
        }

        public void SendEmail(string senderEmail, string senderEmailPassword, string recipientEmail,
            string? verificationCode, bool? validatedCode, string templateName)
        {
            var template = _emailTemplateRepository.GetByTemplateName(templateName);

            if (template == null)
                throw new ApplicationException("Template não existe");

            var templateContentCode = string.Empty;
            if (verificationCode != null && validatedCode != null)
                templateContentCode = string.Format(template.Content, verificationCode);

            var smtpClient = new SmtpClient(host: "smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Timeout = 30000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderEmailPassword)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = template.EmailSubject,
                Body = templateContentCode,
                IsBodyHtml = template.ContentIsHtml
            };
            mailMessage.To.Add(recipientEmail);

            smtpClient.Send(mailMessage);

            var emailSent = new EmailSent
            {
                SenderEmail = senderEmail,
                RecipientEmail = recipientEmail,
                SendDate = DateTime.Now,
                VerificationCode = verificationCode,
                ValidatedCode = validatedCode,
                EmailTemplateId = template.EmailTemplateId
            };
            _emailSentRepository.Insert(emailSent);
            _unitOfWork.Save();
        }
    }
}
