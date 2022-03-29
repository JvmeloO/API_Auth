using API_Auth.Context;
using API_Auth.Models.DTOs;
using API_Auth.Models.Entities;
using API_Auth.Repositories.Abstract;
using API_Auth.Repositories.Concrete;
using API_Auth.Services.Abstract;
using API_Auth.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptService _encryptService;

        [ActivatorUtilitiesConstructor]
        public UserController()
        {
            _userRepository = new UserRepository(new AppDbContext());
            _encryptService = new EncryptService();
        }

        public UserController(IUserRepository userRepository, IEncryptService encryptService)
        {
            _userRepository = userRepository;
            _encryptService = encryptService;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (_userRepository.GetUserByUsername(userRegisterDTO.Username) != null)
                return BadRequest(new { message = "Nome de usuário já usado" });

            try
            {
                var passwordEncrypted = _encryptService.EncryptPassword(userRegisterDTO.Password);

                var user = new User 
                {
                    Username = userRegisterDTO.Username,
                    Email = userRegisterDTO.Email,
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
