using Auth.Infra.DbContexts;
using Auth.Domain.Entities;
using Auth.Infra.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infra.Repositories.Concrete
{
    public class RoleRepository : IRoleRepository, IDisposable
    {
        private readonly authdbContext _context;

        public RoleRepository(authdbContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public IEnumerable<Role> GetRolesByUserId(int userId)
        {
            return _context.Roles.Where(r => r.Users.Any(u => u.UserId == userId));
        }

        public Role GetRoleByRoleName(string roleName)
        {
            return _context.Roles.SingleOrDefault(r => r.RoleName == roleName);
        }

        public Role GetRoleByRoleId(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public void InsertRole(Role role)
        {
            _context.Roles.Add(role);
        }

        public void DeleteRole(int roleId)
        {
            var role = _context.Roles.Find(roleId);
            _context.Roles.Remove(role);
        }

        public void UpdateRole(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
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
