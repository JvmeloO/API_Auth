using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infra.Repositories.Concrete
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly authdbContext _context;

        public UserRepository(authdbContext context)
        {
            _context = context;
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public int GetUserIdByUsername(string username)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username).UserId;
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        public void InsertRolesToUser(int userId, List<int> rolesIds)
        {
            var userRoles = _context.Users.Single(u => u.UserId == userId);
            foreach (var roleId in rolesIds)
                userRoles.Roles.Add(_context.Roles.Single(r => r.RoleId == roleId));
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            _context.Users.Remove(user);
        }

        public void DeleteRolesToUser(int userId, List<int> rolesIds)
        {
            var userRoles = _context.Users.Include(r => r.Roles).Single(u => u.UserId == userId);

            foreach (var roleId in rolesIds)
                userRoles.Roles.Remove(userRoles.Roles.SingleOrDefault(r => r.RoleId == roleId));
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
            if (!disposed)
                if (disposing)
                    _context.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
