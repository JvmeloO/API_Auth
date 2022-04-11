using Auth.API.Models.DTOs;
using Auth.Domain.Entities;

namespace Auth.API.Models.Mappings
{
    public static class RoleMap
    {
        public static RoleResultDTO EntityToDTO(Role role)
        {
            return new RoleResultDTO
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName
            };
        }
    }
}
