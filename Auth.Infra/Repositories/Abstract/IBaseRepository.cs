using System.Linq.Expressions;

namespace Auth.Infra.Repositories.Abstract
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        T Insert(T obj);
        void Delete(T obj);
        T Update(T obj);
        IQueryable<T> GetAllWithIncludes(params Expression<Func<T, object>>[] includes);
        T GetWithSingleOrDefault(Func<T, bool> singleOrDefault);
        IQueryable<T> GetWithWhere(Func<T, bool> where);
        IQueryable<T> GetWithIncludeAndWhere(Expression<Func<T, object>> include, Func<T, bool> where);
        T GetWithIncludeAndSingleOrDefault(Expression<Func<T, object>> include, Func<T, bool> singleOrDefault);
    }
}
