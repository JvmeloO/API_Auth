using Auth.Business.Models.DTOs;
using Auth.Domain.Entities;

namespace Auth.Business.Services.Abstract
{
    public interface IPasswordRecoveryService
    {
        void SendEmailVerificationCode(PasswordRecoveryServiceDTO passwordRecoveryServiceDTO);
        void NewPassword(PasswordRecoveryNewPasswordServiceDTO passwordRecoveryNewPasswordServiceDTO, User user);
    }
}
