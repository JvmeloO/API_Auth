using API_Auth.Models.Entities;

namespace API_Auth.Services.Abstract
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
