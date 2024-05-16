using System.Collections.Generic;
using System.Threading.Tasks;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Article;
using EdixleQuery.Contracts.ArticleCategory;

namespace ServiceHost.Pages
{
    public class BlogDetailsModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly ICommentApplication _commentApplication;
        private readonly IArticleCategoryQuery _categoryQuery;
        public BlogDetailsModel(IArticleQuery articleQuery, IArticleCategoryQuery categoryQuery, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _categoryQuery = categoryQuery;
            _commentApplication = commentApplication;
        }

        public ArticleQueryModel Article { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
        public List<ArticleCategoryQueryModel> Categories { get; set; }
        [BindProperty]
        public AddComment AddComment { get; set; }
        public async Task OnGet(string id)
        {
            Article = await _articleQuery.GetArticleAsync(id);
            LatestArticles =await _articleQuery.GetLatestArticlesAsync();
            Categories = await _categoryQuery.GetArticleCategoriesAsync();
        }

        public async Task<IActionResult> OnPostComment()
        {
            AddComment.Type = CommentType.Article;
            await _commentApplication.AddAsync(AddComment);
            return RedirectToPage("./BlogDetails");
        }
    }
}
