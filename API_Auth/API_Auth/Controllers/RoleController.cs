#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Auth.DTO;
using API_Auth.Data;
using Microsoft.AspNetCore.Authorization;

namespace API_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly DataContext _context;

        public RoleController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<RoleDTO>> GetRoleDTO(int id)
        {
            var roleDTO = await _context.Roles.FindAsync(id);

            if (roleDTO == null)
            {
                return NotFound();
            }

            return roleDTO;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutRoleDTO(int id, RoleDTO roleDTO)
        {
            if (id != roleDTO.Role_Id)
            {
                return BadRequest();
            }

            _context.Entry(roleDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<RoleDTO>> PostRoleDTO(RoleDTO roleDTO)
        {
            _context.Roles.Add(roleDTO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleDTO", new { id = roleDTO.Role_Id }, roleDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteRoleDTO(int id)
        {
            var roleDTO = await _context.Roles.FindAsync(id);
            if (roleDTO == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(roleDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleDTOExists(int id)
        {
            return _context.Roles.Any(e => e.Role_Id == id);
        }
    }
}
