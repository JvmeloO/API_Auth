using Auth.Infra.Context;
using Auth.Infra.Repositories.Abstract;
using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infra.Repositories.Concrete
{
    public class UserRoleRepository : IUserRoleRepository, IDisposable
    {
        private readonly AppDbContext _context;

        public UserRoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserRole> GetUserRoles()
        {
            return _context.UserRoles.ToList();
        }

        public IEnumerable<UserRole> GetUserRolesByUserId(int userId)
        {
            return _context.UserRoles.ToList().Where(ur => ur.UserId == userId);
        }

        public void InsertUserRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
        }

        public void DeleteUserRole(UserRole userRole)
        {
            var userRoleDelete = _context.UserRoles.SingleOrDefault(ur => ur.UserId == userRole.UserId && ur.RoleId == userRole.RoleId);
            _context.UserRoles.Remove(userRoleDelete);
        }

        public void UpdateUser(UserRole userRole)
        {
            _context.Entry(userRole).State = EntityState.Modified;
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
