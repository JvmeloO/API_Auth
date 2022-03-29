using API_Auth.Context;
using API_Auth.Models.Entities;
using API_Auth.Repositories.Abstract;
using API_Auth.Repositories.Concrete;
using API_Auth.Services.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Auth.Services.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly IRoleRepository _roleRepository;

        public TokenService()
        {
            _roleRepository = new RoleRepository(new AppDbContext());
        }

        public TokenService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public string GenerateToken(User user) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
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
            var userRoles = _roleRepository.GetRolesByUserId(user.UserId);
            if (userRoles != null)
                claims.AddRange(from role in userRoles
                                select (new Claim(ClaimTypes.Role, role.RoleName)));

            return new ClaimsIdentity(claims.ToArray());
        }
    }
}
