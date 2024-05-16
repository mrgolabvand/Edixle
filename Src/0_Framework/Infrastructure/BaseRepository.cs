using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace _0_Framework.Infrastructure
{
    public class BaseRepository<TKey, T> : IBaseRepository<TKey, T> where T : class
    {
        private readonly DbContext dbContext;

        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async ValueTask CreateAsync(T entity) => await dbContext.AddAsync(entity);

        public async ValueTask<bool> ExistsAsync(Expression<Func<T, bool>> expression) => await dbContext.Set<T>().AnyAsync(expression);

        public async ValueTask<T> GetAsync(TKey id) => await dbContext.FindAsync<T>(id);

        public async ValueTask<List<T>> GetAsync() => await dbContext.Set<T>().ToListAsync();

        public async ValueTask SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
