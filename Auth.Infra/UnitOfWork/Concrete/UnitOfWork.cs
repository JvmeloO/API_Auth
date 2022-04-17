using Auth.Infra.DbContexts;
using Auth.Infra.UnitOfWork.Abstract;

namespace Auth.Infra.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly authdbContext _context;

        public UnitOfWork(authdbContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
