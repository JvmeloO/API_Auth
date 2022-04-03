using Auth.Infra.Context;
using Auth.Infra.Repositories.Abstract;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infra.Repositories.Concrete
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserByUsername(string username)
        { 
            return _context.Users.SingleOrDefault(x => x.Username == username);
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            _context.Users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
