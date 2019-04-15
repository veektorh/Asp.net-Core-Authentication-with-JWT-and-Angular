using System;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Repository
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        IQueryable<T> GetAll();

        T GetById(object id);

        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    }
}