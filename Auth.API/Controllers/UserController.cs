﻿using Auth.API.Models.DTOs;
using Auth.Business.Services.Abstract;
using Auth.Domain.Entities;
using Auth.Infra.Repositories.Abstract;
using Auth.Infra.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptService _encryptService;

        public UserController(IUserRepository userRepository, IUnitOfWork unitOfWork, IEncryptService encryptService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _encryptService = encryptService;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] UserCreateDTO userCreateDTO)
        {
            try
            {
                if (_userRepository.GetByUsername(userCreateDTO.Username) != null)
                    return BadRequest(new { message = "Nome de usuário já usado" });

                var passwordEncrypted = _encryptService.EncryptPassword(userCreateDTO.Password);

                var user = new User
                {
                    Username = userCreateDTO.Username,
                    Email = userCreateDTO.Email,
                    Password = passwordEncrypted
                };
                _userRepository.Insert(user);
                _unitOfWork.Save();

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
