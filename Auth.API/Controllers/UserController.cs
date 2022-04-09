using Auth.API.Models.DTOs;
using Auth.Business.Services.Abstract;
using Auth.Domain.Entities;
using Auth.Infra.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptService _encryptService;

        public UserController(IUserRepository userRepository, IEncryptService encryptService)
        {
            _userRepository = userRepository;
            _encryptService = encryptService;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] User User)
        {
            if (_userRepository.GetUserByUsername(User.Username) != null)
                return BadRequest(new { message = "Nome de usuário já usado" });

            try
            {
                var passwordEncrypted = _encryptService.EncryptPassword(User.Password);

                var user = new User
                {
                    Username = User.Username,
                    Email = User.Email,
                    Password = passwordEncrypted
                };

                _userRepository.InsertUser(user);
                _userRepository.Save();

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
