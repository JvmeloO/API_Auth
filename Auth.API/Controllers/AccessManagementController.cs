using Auth.API.Models.DTOs;
using Auth.Domain.Entities;
using Auth.Infra.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccessManagementController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AccessManagementController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [Route("Roles")]
        [Authorize(Roles = "Administrador")]
        public IActionResult GetRoles() 
        {
            return Ok(_roleRepository.GetRoles());
        }

        [HttpGet]
        [Route("Roles/User/{username}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult GetRolesByUsername(string username)
        {
            return Ok(_roleRepository.GetRolesByUserId(_userRepository.GetUserIdByUsername(username)));
        }

        [HttpPost]
        [Route("Role/Create")]
        [Authorize(Roles = "Administrador")]
        public IActionResult RoleCreate(RoleCreateDTO roleCreateDTO)
        {
            if (_roleRepository.GetRoleByRoleName(roleCreateDTO.RoleName) != null)
                return BadRequest(new { message = "Função já existe" });

            try
            {
                var role = new Role
                {
                    RoleName = roleCreateDTO.RoleName,
                };

                _roleRepository.InsertRole(role);
                _roleRepository.Save();

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Roles/Grant-User")]
        [Authorize(Roles = "Administrador")]
        public IActionResult GrantRolesUser([FromBody] UserRolesDTO userRolesDTO)
        {
            var user = _userRepository.GetUserByUsername(userRolesDTO.Username);

            if (user == null)
                return NotFound(new { message = "Usuário não cadastrado" });

            try
            {
                _userRepository.InsertRolesToUser(user.UserId, userRolesDTO.RolesIds);
                _userRepository.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[HttpPost]
        //[Route("Roles/Dismiss-User")]
        //[Authorize(Roles = "Administrador")]
        //public IActionResult DismissRolesUser([FromBody] UserRolesDTO userRolesDTO)
        //{
        //    if (_userRepository.GetUserByUsername(userRolesDTO.Username) == null)
        //        return NotFound(new { message = "Usuário não cadastrado" });

        //    try
        //    {
        //        _userRepository.InsertRolesToUser(userRolesDTO.Username, userRolesDTO.RolesIds);
        //        _userRepository.Save();

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}
    }
}
