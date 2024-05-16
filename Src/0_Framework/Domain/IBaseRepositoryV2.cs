using _0_Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _0_Framework.Domain
{
    public interface IBaseRepositorV2<T, TKey, TEditModel, TViewModel> where T : class where TEditModel : class where TViewModel : class
    {
        void Create(T entity);
        T Get(TKey id);
        List<T> Get();
        bool Exists(Expression<Func<T, bool>> expression);
        void SaveChanges();
        TEditModel GetDetails(long id);
        //List<T> Search(PortfolioCategorySearchModel searchModel);
    }
}
