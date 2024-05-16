using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _0_Framework.Domain
{
    public interface IBaseRepository<TKey, T> where T : class
    {
        ValueTask CreateAsync(T entity);
        ValueTask<T> GetAsync(TKey id);
        ValueTask<List<T>> GetAsync();
        ValueTask<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        ValueTask SaveChangesAsync();
    }
}
