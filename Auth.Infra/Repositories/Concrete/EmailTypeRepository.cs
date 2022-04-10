using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;

namespace Auth.Infra.Repositories.Concrete
{
    public class EmailTypeRepository : IEmailTypeRepository, IDisposable
    {
        private readonly authdbContext _context;

        public EmailTypeRepository(authdbContext context)
        {
            _context = context;
        }

        public int GetEmailTypeIdByTypeName(string typeName)
        {
            return _context.EmailTypes.SingleOrDefault(u => u.TypeName == typeName).EmailTypeId;
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
