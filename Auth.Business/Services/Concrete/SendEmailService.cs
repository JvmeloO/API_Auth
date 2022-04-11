using System.Net;
using System.Net.Mail;
using Auth.Business.Models.DTOs;
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

        public SendEmailService(IEmailTemplateRepository emailTemplateRepository, IEmailSentRepository emailSentRepository,
            IUnitOfWork unitOfWork)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _emailSentRepository = emailSentRepository;
            _unitOfWork = unitOfWork;
        }

        public void SendEmail(SendEmailServiceDTO sendEmailServiceDTO)
        {
            var template = _emailTemplateRepository.GetWithSingleOrDefault(e => e.TemplateName == sendEmailServiceDTO.TemplateName);

            if (template == null)
                throw new ApplicationException("Template não existe");

            var templateContentWithVariables = template.Content;
            if (sendEmailServiceDTO.VerificationCode != null && sendEmailServiceDTO.ValidatedCode != null)
                templateContentWithVariables = templateContentWithVariables.Replace("@codigoVerificacao", sendEmailServiceDTO.VerificationCode);
            if (sendEmailServiceDTO.Link != null)
                templateContentWithVariables = templateContentWithVariables.Replace("@link", sendEmailServiceDTO.Link);

            var smtpClient = new SmtpClient(host: "smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Timeout = 30000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sendEmailServiceDTO.SenderEmail, sendEmailServiceDTO.SenderEmailPassword)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(sendEmailServiceDTO.SenderEmail),
                Subject = template.EmailSubject,
                Body = templateContentWithVariables,
                IsBodyHtml = template.ContentIsHtml
            };
            mailMessage.To.Add(sendEmailServiceDTO.RecipientEmail);

            smtpClient.Send(mailMessage);

            var emailSent = new EmailSent
            {
                SenderEmail = sendEmailServiceDTO.SenderEmail,
                RecipientEmail = sendEmailServiceDTO.RecipientEmail,
                SendDate = DateTime.Now,
                VerificationCode = sendEmailServiceDTO.VerificationCode,
                ValidatedCode = sendEmailServiceDTO.ValidatedCode,
                Link = sendEmailServiceDTO.Link,
                EmailTemplateId = template.EmailTemplateId
            };
            _emailSentRepository.Insert(emailSent);
            _unitOfWork.Save();
        }
    }
}
