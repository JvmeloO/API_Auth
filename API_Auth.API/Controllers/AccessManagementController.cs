using API_Auth.API.Models.DTOs;
using API_Auth.Domain.Entities;
using API_Auth.Infra.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccessManagementController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;

        public AccessManagementController(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
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
        [Route("Role/Grant-User")]
        [Authorize(Roles = "Administrador")]
        public IActionResult GrantRoleUser([FromBody] UserRolesDTO userRolesDTO)
        {
            var user = _userRepository.GetUserByUsername(userRolesDTO.Username);

            if (user == null)
                return NotFound(new { message = "Usuário não cadastrado" });

            try
            {
                foreach (var roleId in userRolesDTO.RoleId)
                {
                    var userRole = new UserRole
                    {
                        UserId = user.UserId,
                        RoleId = roleId
                    };

                    _userRoleRepository.InsertUserRole(userRole);
                }

                _userRoleRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
