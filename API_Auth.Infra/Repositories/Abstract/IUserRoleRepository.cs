using API_Auth.Domain.Entities;

namespace API_Auth.Infra.Repositories.Abstract
{
    public interface IUserRoleRepository : IDisposable
    {
        IEnumerable<UserRole> GetUserRoles();
        IEnumerable<UserRole> GetUserRolesByUserId(int userId);
        void InsertUserRole(UserRole userRole);
        void DeleteUserRole(UserRole userRole);
        void UpdateUser(UserRole userRole);
        void Save();
    }
}
