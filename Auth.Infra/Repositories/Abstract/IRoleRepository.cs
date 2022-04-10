using Auth.Domain.Entities;

namespace Auth.Infra.Repositories.Abstract
{
    public interface IRoleRepository : IDisposable
    {
        IEnumerable<Role> GetRoles();
        IEnumerable<Role> GetRolesByUserId(int userId);
        Role GetRoleByRoleName(string roleName);
        void InsertRole(Role role);
        void DeleteRole(int roleId);
        void UpdateRole(Role role);
        void Save();
    }
}
