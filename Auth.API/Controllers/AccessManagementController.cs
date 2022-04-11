using Auth.API.Models.DTOs;
using Auth.API.Models.Mappings;
using Auth.Domain.Entities;
using Auth.Infra.Repositories.Abstract;
using Auth.Infra.UnitOfWork.Abstract;
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
        private readonly IUnitOfWork _unitOfWork;

        public AccessManagementController(IUserRepository userRepository, IRoleRepository roleRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("Roles")]
        [Authorize(Roles = "Administrador")]
        public IActionResult GetRoles() 
        {
            try
            {
                var roles = _roleRepository.GetAll();
                return Ok(roles.Select(r => RoleMap.EntityToDTO(r)).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("Roles/User/{username}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult GetUserRoles(string username)
        {
            try
            {
                var roles = _roleRepository.GetWithIncludeAndWhere(r => r.Users, r => r.Users.Any(u => u.Username == username));
                return Ok(roles.Select(r => RoleMap.EntityToDTO(r)).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Roles/Grant-User")]
        [Authorize(Roles = "Administrador")]
        public IActionResult GrantUserRoles([FromBody] UserRolesDTO userRolesDTO)
        {
            try
            {
                var user = _userRepository.GetByUsername(userRolesDTO.Username);

                if (user == null)
                    return NotFound(new { message = "Usuário não cadastrado" });

                // Insert in table Many to Many
                foreach (var roleId in userRolesDTO.RolesIds)
                    user.Roles.Add(_roleRepository.GetById(roleId));
                _unitOfWork.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Roles/Dismiss-User")]
        [Authorize(Roles = "Administrador")]
        public IActionResult DismissUserRoles([FromBody] UserRolesDTO userRolesDTO)
        {
            try
            {
                var user = _userRepository.GetWithIncludeAndSingleOrDefault(u => u.Roles, u => u.Username == userRolesDTO.Username);

                if (user == null)
                    return NotFound(new { message = "Usuário não cadastrado" });

                // Delete in table Many to Many
                foreach (var roleId in userRolesDTO.RolesIds)
                    user.Roles.Remove(_roleRepository.GetById(roleId));
                _unitOfWork.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
