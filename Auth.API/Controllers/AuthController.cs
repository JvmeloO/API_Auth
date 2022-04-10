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
        private readonly IPasswordRecoveryService _passwordRecoveryService;

        public AuthController(IUserRepository userRepository, ITokenService tokenService, IEncryptService encryptService,
            IPasswordRecoveryService passwordRecoveryService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _encryptService = encryptService;
            _passwordRecoveryService = passwordRecoveryService;
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

        [HttpPost]
        [Route("Recovery")]
        public IActionResult SendEmailPasswordRecovery([FromBody] PasswordRecoveryDTO passwordRecoveryDTO)
        {
            try
            {
                if (_userRepository.GetUserByEmail(passwordRecoveryDTO.Email) == null)
                    return NotFound(new { message = "Email não cadastrado" });

                _passwordRecoveryService.SendEmailVerificationCode(passwordRecoveryDTO.Email);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Recovery/ValidateCode")]
        public IActionResult ValidateCodePasswordRecovery([FromBody] PasswordRecoveryDTO passwordRecoveryDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(passwordRecoveryDTO.ValidateCode))
                    return BadRequest(new { message = "Informe o código de verificação recebido por email" });

                if (!_passwordRecoveryService.ValidateCode(passwordRecoveryDTO.Email, passwordRecoveryDTO.ValidateCode))
                    return Unauthorized(new { message = "Código de verificação inválido" });

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
