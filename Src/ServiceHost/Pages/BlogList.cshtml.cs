using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Article;
using EdixleQuery.Contracts.ArticleCategory;

namespace ServiceHost.Pages
{
    public class BlogListModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _categoryQuery;

        public BlogListModel(IArticleCategoryQuery categoryQuery, IArticleQuery articleQuery)
        {
            _categoryQuery = categoryQuery;
            _articleQuery = articleQuery;
        }

        public ArticleCategoryQueryModel ArticleCategoryQueryModel { get; set; }
        public List<ArticleQueryModel> ArticlesQueryModel { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
        public List<ArticleCategoryQueryModel> Categories { get; set; }
        public async Task OnGet(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                ArticleCategoryQueryModel = await _categoryQuery.GetArticleCategoryAsync(id);
            }
            else
            {
                ArticlesQueryModel = await _articleQuery.GetArticlesAsync();
            }

            LatestArticles = await _articleQuery.GetLatestArticlesAsync();
            Categories = await _categoryQuery.GetArticleCategoriesAsync();
        }
    }
}
