using System.Net;
using System.Net.Mail;

using Auth.Business.Services.Abstract;

namespace Auth.Business.Services.Concrete
{
    public class SendEmailService : ISendEmailService
    {
        public void SendEmail(int emailType, string senderEmail, string senderEmailPassword, string recipientEmail,
            string subjectEmail, string content, bool contentIsHtml, string? verificationCode, bool? validatedCode)
        {
            var smtpClient = new SmtpClient(host: "smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(senderEmail, senderEmailPassword)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subjectEmail,
                Body = content,
                IsBodyHtml = contentIsHtml
            };
            mailMessage.To.Add(recipientEmail);

            smtpClient.Send(mailMessage);
        }
    }
}
