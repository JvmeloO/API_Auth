using Auth.API.Models.DTOs;
using Auth.Business.Services.Abstract;
using Auth.Infra.Repositories.Abstract;
using Auth.Infra.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IEncryptService _encryptService;
        private readonly IPasswordRecoveryService _passwordRecoveryService;

        public AuthController(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService,
            IEncryptService encryptService, IPasswordRecoveryService passwordRecoveryService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _encryptService = encryptService;
            _passwordRecoveryService = passwordRecoveryService;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserAuthDTO userAuthDTO)
        {
            try
            {
                var user = _userRepository.GetByUsername(userAuthDTO.Username);

                if (user == null)
                    return NotFound(new { message = "Usuário não cadastrado" });

                if (!_encryptService.VerifyPassword(userAuthDTO.Password, user.Password))
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
        [Route("PasswordRecovery")]
        public IActionResult SendEmailPasswordRecovery([FromBody] PasswordRecoveryDTO passwordRecoveryDTO)
        {
            try
            {
                if (_userRepository.GetWithSingleOrDefault(u => u.Email == passwordRecoveryDTO.Email) == null)
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
        [Route("PasswordRecovery/ValidateCode")]
        public IActionResult ValidateCodePasswordRecovery([FromBody] PasswordRecoveryVerificationCodeDTO passwordRecoveryVerificationCodeDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(passwordRecoveryVerificationCodeDTO.VerificationCode))
                    return BadRequest(new { message = "Informe o código de verificação recebido por email" });

                if (!_passwordRecoveryService.ValidateCode(passwordRecoveryVerificationCodeDTO.Email, passwordRecoveryVerificationCodeDTO.VerificationCode))
                    return Unauthorized(new { message = "Código de verificação inválido ou expirado" });

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
