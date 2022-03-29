using API_Auth.Models.Entities;

namespace API_Auth.Repositories.Abstract
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
