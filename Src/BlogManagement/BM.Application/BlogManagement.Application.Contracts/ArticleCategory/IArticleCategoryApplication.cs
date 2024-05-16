using _0_Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public interface IArticleCategoryApplication
    {
        ValueTask<OperationResult> CreateAsync(CreateArticelCategory command);
        ValueTask<OperationResult> EditAsync(EditArticleCategory command);
        ValueTask<List<ArticleCategoryViewModel>> GetArticleCategoriesAsync();
        ValueTask<List<ArticleCategoryViewModel>> SearchAsync(ArticleCategorySearchModel searchModel);
        ValueTask<EditArticleCategory> GetDetailsAsync(long id);
    }
}
