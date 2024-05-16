using _0_Framework.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace _0_Framework.Infrastructure
{
    public class BaseRepositoryV2<T, TKey, TEditModel, TViewModel> : IBaseRepositorV2<T, TKey, TEditModel, TViewModel> where T : class where TEditModel : class where TViewModel : class
    {
        private readonly DbContext dbContext;
        public IMapper Mapper { get; set; }
        public BaseRepositoryV2(DbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            Mapper = mapper;
        }

        public TEditModel GetDetails(long id)
        {
            var model = dbContext.Find<BaseModel<T>>(id);
            return Mapper.Map<EditModel<TEditModel>>(model).Model;
        }
        public void Create(T entity)
        {
            dbContext.Add(entity);
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Any(expression);
        }

        public T Get(TKey id)
        {
            return dbContext.Find<T>(id);
        }

        public List<T> Get()
        {
            return dbContext.Set<T>().ToList();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
