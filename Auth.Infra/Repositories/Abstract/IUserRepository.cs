using Auth.Domain.Entities;

namespace Auth.Infra.Repositories.Abstract
{
    public interface IUserRepository : IDisposable
    {
        User GetUserByUsername(string username);
        User GetUserByEmail(string email);
        int GetUserIdByUsername(string username);
        void InsertUser(User user);
        void InsertRolesToUser(int userId, List<int> rolesIds);
        void DeleteUser(int userId);
        void DeleteRolesToUser(int userId, List<int> rolesIds);
        void UpdateUser(User user);
        void Save();
    }
}
