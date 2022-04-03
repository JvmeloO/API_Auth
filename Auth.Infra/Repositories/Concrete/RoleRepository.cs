using Auth.Infra.Context;
using Auth.Domain.Entities;
using Auth.Infra.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infra.Repositories.Concrete
{
    public class RoleRepository : IRoleRepository, IDisposable
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context) 
        {
            _context = context;
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public IEnumerable<Role> GetRolesByUserId(int userId) 
        {
            var userRoles = _context.UserRoles.ToList().Where(ur => ur.UserId == userId);
            var roles = (from role in userRoles
                         select GetRoleById(role.RoleId));

            return roles;
        }

        public Role GetRoleByRoleName(string roleName) 
        {
            return _context.Roles.SingleOrDefault(x => x.RoleName == roleName);
        }

        public Role GetRoleById(int roleId)
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
