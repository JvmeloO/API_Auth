using API_Auth.Domain.Entities;

namespace API_Auth.Infra.Repositories.Abstract
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserByUsername(string username);
        User GetUserById(int userId);
        void InsertUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        void Save();
    }
}
