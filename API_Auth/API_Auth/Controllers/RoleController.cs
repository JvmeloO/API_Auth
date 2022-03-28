#nullable disable
using API_Auth.Context;
using API_Auth.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoleController(AppDbContext context)
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
