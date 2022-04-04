using Auth.Domain.Entities;

namespace Auth.Infra.Repositories.Abstract
{
    public interface IUserRepository : IDisposable
    {
        User GetUserByUsername(string username);
        int GetUserIdByUsername(string username);
        void InsertUser(User user);
        void InsertRolesToUser(int userId, List<int> RolesIds);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void Save();
    }
}
