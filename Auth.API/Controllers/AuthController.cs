using Auth.API.Models.DTOs;
using Auth.Business.Models.DTOs;
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
        private readonly IPasswordRecoveryService _passwordRecoveryService;

        public AuthController(IUserRepository userRepository, ITokenService tokenService,
            IEncryptService encryptService, IPasswordRecoveryService passwordRecoveryService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _encryptService = encryptService;
            _passwordRecoveryService = passwordRecoveryService;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserAuthCreateDTO userAuthCreateDTO)
        {
            try
            {
                var user = _userRepository.GetByUsername(userAuthCreateDTO.Username);

                if (user == null)
                    return NotFound(new { message = "Usuário não cadastrado" });

                if (!_encryptService.VerifyPassword(userAuthCreateDTO.Password, user.Password))
                    return BadRequest(new { message = "Senha inválida" });

                var token = _tokenService.GenerateToken(user);

                return Ok(new UserCredentialResultDTO 
                {
                    Username = user.Username,
                    Email = user.Email,
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("PasswordRecovery/Send-Email")]
        public IActionResult SendEmailPasswordRecovery([FromBody] PasswordRecoveryServiceDTO passwordRecoveryServiceDTO)
        {
            try
            {
                if (_userRepository.GetWithSingleOrDefault(u => u.Email == passwordRecoveryServiceDTO.Email) == null)
                    return NotFound(new { message = "Email não cadastrado" });

                _passwordRecoveryService.SendEmailVerificationCode(passwordRecoveryServiceDTO);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("PasswordRecovery/New-Password")]
        public IActionResult NewPasswordPasswordRecovery([FromBody] PasswordRecoveryNewPasswordServiceDTO passwordRecoveryNewPasswordServiceDTO)
        {
            try
            {
                var user = _userRepository.GetWithSingleOrDefault(u => u.Email == passwordRecoveryNewPasswordServiceDTO.Email);

                if (user == null)
                    return NotFound(new { message = "Email não cadastrado" });

                _passwordRecoveryService.NewPassword(passwordRecoveryNewPasswordServiceDTO, user);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
