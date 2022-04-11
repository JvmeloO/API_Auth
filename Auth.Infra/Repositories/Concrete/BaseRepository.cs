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
            var result = _dbSet.AsQueryable();

            foreach (var include in includes)
                result = result.Include(include);

            return result;
        }

        public IQueryable<T> GetWithWhere(Func<T, bool> where)
        {
            return _dbSet.Where(where).AsQueryable();
        }

        public IQueryable<T> GetWithIncludeAndWhere(Expression<Func<T, object>> include, Func<T, bool> where)
        {
            var result = _dbSet.Include(include).Where(where).AsQueryable();

            return result;
        }

        public T GetWithIncludeAndSingleOrDefault(Expression<Func<T, object>> include, Func<T, bool> singleOrDefault)
        {
            var result = _dbSet.Include(include).SingleOrDefault(singleOrDefault);

            return result;
        }
    }
}
