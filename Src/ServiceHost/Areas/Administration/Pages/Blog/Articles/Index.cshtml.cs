using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.Configuration.Permissions;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class IndexModel : PageModel
    {

        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        public SelectList ArticleCategories { get; set; }
        public ArticleSearchModel SearchModel { get; set; }
        public List<ArticleViewModel> Articles { get; set; }

        public async Task OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategories = new SelectList(await _articleCategoryApplication.GetArticleCategoriesAsync(), "Id", "Name");
            Articles = await _articleApplication.SearchAsync(searchModel);
        }

        [NeedsPermission(BlogPermissions.CreateAndEditArticle)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticle());
        }

        [NeedsPermission(BlogPermissions.CreateAndEditArticle)]
        public async Task<JsonResult> OnPostCreate(CreateArticle command)
        {
            var result = await _articleApplication.CreateAsync(command);
            return new JsonResult(result);
        }

        [NeedsPermission(BlogPermissions.CreateAndEditArticle)]
        public async Task<IActionResult> OnGetEdit(long id)
        {
            var articleCategory = await _articleApplication.GetDetailsAsync(id);
            return Partial("./Edit", articleCategory);
        }

        [NeedsPermission(BlogPermissions.CreateAndEditArticle)]
        public async  Task<JsonResult> OnPostEdit(EditArticle command)
        {
            var result = await _articleApplication.EditAsync(command);
            return new JsonResult(result);
        }
    }
}
