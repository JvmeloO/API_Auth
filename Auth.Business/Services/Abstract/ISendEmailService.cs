using Auth.Business.Models.DTOs;

namespace Auth.Business.Services.Abstract
{
    public interface ISendEmailService
    {
        void SendEmail(SendEmailServiceDTO sendEmailServiceDTO);
    }
}
