using Auth.Domain.Entities;

namespace Auth.Infra.Repositories.Abstract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByUsername(string username);
        User GetByEmail(string email);
    }
}
