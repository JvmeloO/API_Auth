using Auth.API.Models.DTOs;
using Auth.Business.Services.Abstract;
using Auth.Infra.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IEncryptService _encryptService;

        public AuthController(IUserRepository userRepository, ITokenService tokenService, IEncryptService encryptService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _encryptService = encryptService;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserAuthDTO userAuthDTO)
        {
            try
            {
                var user = _userRepository.GetUserByUsername(userAuthDTO.Username);

                if (user == null)
                    return NotFound(new { message = "Usuário não cadastrado" });

                if (!_encryptService.VerifyPassword(userAuthDTO.Password, user.Password))
                    return BadRequest(new { message = "Senha inválida" });

                var token = _tokenService.GenerateToken(user);

                var userCredentialDTO = new UserCredentialDTO
                {
                    Username = user.Username,
                    Email = user.Email,
                    Token = token
                };

                return Ok(userCredentialDTO);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
