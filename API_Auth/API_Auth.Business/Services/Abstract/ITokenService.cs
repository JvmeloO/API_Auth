using API_Auth.Domain.Entities;

namespace API_Auth.Business.Services.Abstract
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
