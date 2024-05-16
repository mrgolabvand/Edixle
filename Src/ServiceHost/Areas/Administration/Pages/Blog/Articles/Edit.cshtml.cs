using System.Threading.Tasks;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class EditModel : PageModel
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        [BindProperty]
        public EditArticle Command { get; set; }
        public SelectList ArticleCategories { get; set; }

        public EditModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        public async Task OnGet(long id)
        {
            Command = await _articleApplication.GetDetailsAsync(id);
            ArticleCategories = new SelectList(await _articleCategoryApplication.GetArticleCategoriesAsync(), "Id", "Name");
        }

        public async Task<IActionResult> OnPost()
        {
            await _articleApplication.EditAsync(Command);

            return RedirectToPage("./Index");
        }
    }
}
