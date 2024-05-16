using _0_Framework.Domain;
using BlogManagement.Application.Contracts.Article;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IBaseRepository<long, Article>
    {
        ValueTask<EditArticle> GetDetailsAsync(long id);
        ValueTask<Article> GetWithCategoryAsync(long id);
        ValueTask<List<ArticleViewModel>> SearchAsync(ArticleSearchModel searchModel);
    }
}
