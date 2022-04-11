using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infra.Repositories.Concrete
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DbSet<User> _dbSet;
        public UserRepository(authdbContext context) : base(context) 
        { 
            _dbSet = context.Set<User>(); 
        }

        public User GetByUsername(string username)
        {
            return _dbSet.SingleOrDefault(u => u.Username == username);
        }
    }
}
