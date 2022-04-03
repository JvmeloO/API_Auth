using Auth.Domain.Entities;

namespace Auth.Business.Services.Abstract
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
