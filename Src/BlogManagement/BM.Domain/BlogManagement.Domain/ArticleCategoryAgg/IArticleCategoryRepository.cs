using _0_Framework.Domain;
using BlogManagement.Application.Contracts.ArticleCategory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IBaseRepository<long, ArticleCategory>
    {
        ValueTask<List<ArticleCategoryViewModel>> GetArticleCategoriesAsync();
        ValueTask<List<ArticleCategoryViewModel>> SearchAsync(ArticleCategorySearchModel searchModel);
        ValueTask<EditArticleCategory> GetDetailsAsync(long id);
        ValueTask<string> GetCategorySlugByAsync(long id);
    }
}
