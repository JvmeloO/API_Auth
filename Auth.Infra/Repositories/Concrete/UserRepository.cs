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

        public int GetUserIdByUsername(string username)
        {
            return GetUserByUsername(username).UserId;
        }

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        public void InsertRolesToUser(int userId, List<int> RolesIds) 
        {
            var userRoles = new User { UserId = userId };
            _context.Users.Add(userRoles);
            _context.Users.Attach(userRoles);

            var roleList = new List<Role>();
            foreach (var roleId in RolesIds)
            {
                var role = new Role { RoleId = roleId };
                _context.Roles.Add(role);
                _context.Roles.Attach(role);
                roleList.Add(role);
            }

            userRoles.Roles = roleList;
            _context.Users.Add(userRoles);
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
