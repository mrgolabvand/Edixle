using System.Threading.Tasks;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class CreateModel : PageModel
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        [BindProperty]
        public CreateArticle Command { get; set; }
        public SelectList ArticleCategories { get; set; }

        public CreateModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        public async Task OnGet()
        {
            ArticleCategories = new SelectList(await _articleCategoryApplication.GetArticleCategoriesAsync(), "Id", "Name");
        }

        public async Task<IActionResult> OnPost()
        {
            await _articleApplication.CreateAsync(Command);

            return RedirectToPage("./Index");
        }
    }
}
