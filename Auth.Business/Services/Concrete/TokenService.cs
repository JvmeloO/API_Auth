using Auth.Business.Services.Abstract;
using Auth.Domain.Configurations;
using Auth.Domain.Entities;
using Auth.Infra.Repositories.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Business.Services.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly BaseConfigurations _config;

        public TokenService(IRoleRepository roleRepository, BaseConfigurations config)
        {
            _roleRepository = roleRepository;
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GetClaimsIdentity(user),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            var userRoles = _roleRepository.GetWithIncludeAndWhere(r => r.Users, r => r.Users.Any(u => u.UserId == user.UserId));
            if (userRoles != null)
                claims.AddRange(from role in userRoles
                                select (new Claim(ClaimTypes.Role, role.RoleName)));

            return new ClaimsIdentity(claims.ToArray());
        }
    }
}
