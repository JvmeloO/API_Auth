using API_Auth.Context;
using API_Auth.DTO;
using API_Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] UserDTO user)
        {
            var userLogin = _context.Users.FirstOrDefault(u => u.Username == user.Username);

            if (userLogin == null)
                return NotFound(new { message = "Usuário não cadastrado" });

            if (EncryptService.EncryptPassword(user.Password) != userLogin.Password)
                return BadRequest(new { message = "Senha inválida" });

            var token = TokenService.GenerateToken(userLogin);

            return new
            {
                Username = userLogin.Username,
                Email = userLogin.Email,
                token = token
            };
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<dynamic>> Register([FromBody] UserDTO user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
                return BadRequest(new { message = "Nome de usuário já usado" });

            var passwordEncrypted = EncryptService.EncryptPassword(user.Password);
            user.Password = passwordEncrypted;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }
    }
}
