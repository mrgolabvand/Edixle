using _0_Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        ValueTask<OperationResult> CreateAsync(CreateArticle command);
        ValueTask<OperationResult> EditAsync(EditArticle command);
        ValueTask<EditArticle> GetDetailsAsync(long id);
        ValueTask<List<ArticleViewModel>> SearchAsync(ArticleSearchModel searchModel);
    }
}
