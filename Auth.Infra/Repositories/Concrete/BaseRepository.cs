using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Auth.Infra.Repositories.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private readonly DbSet<T> _dbSet;

        public BaseRepository(authdbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Insert(T obj)
        {
            return _dbSet.Add(obj).Entity;
        }

        public void Delete(T obj)
        {
            _dbSet.Remove(obj);
        }

        public T Update(T obj)
        {
            return _dbSet.Update(obj).Entity;
        }

        public IQueryable<T> GetAllWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            return includes.Aggregate(_dbSet.AsQueryable(), (current, include) => current.Include(include));
        }

        public T GetWithSingleOrDefault(Func<T, bool> singleOrDefault)
        {
            return _dbSet.SingleOrDefault(singleOrDefault);
        }

        public IQueryable<T> GetWithWhere(Func<T, bool> where)
        {
            return _dbSet.Where(where).AsQueryable();
        }

        // Example Parameters: (w => w.Where == where, i => i.Navigation1, i => i.Navigation2, i => i.Navigation3)
        public IQueryable<T> GetWithWhereAndIncludes(Func<T, bool> where, params Expression<Func<T, object>>[] includes)
        {
            var result = includes.Aggregate(_dbSet.AsQueryable(), (current, include) => current.Include(include));

            return result.Where(where).AsQueryable();
        }

        public T GetWithIncludeAndSingleOrDefault(Expression<Func<T, object>> include, Func<T, bool> singleOrDefault)
        {
            return _dbSet.Include(include).SingleOrDefault(singleOrDefault);
        }
    }
}
