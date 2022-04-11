using Auth.API.Models.DTOs;
using Auth.Domain.Entities;

namespace Auth.API.Models.Mappings
{
    public static class RoleMap
    {
        public static RoleResultDTO EntityToResultDTO(Role role) => new()
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName
        };
    }
}
