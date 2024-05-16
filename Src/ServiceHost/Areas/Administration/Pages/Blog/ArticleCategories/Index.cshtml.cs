using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories
{
    public class IndexModel : PageModel
    {

        private readonly IArticleCategoryApplication _articleCatgoryApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCatgoryApplication = articleCategoryApplication;
        }

        public ArticleCategorySearchModel SearchModel { get; set; }
        public List<ArticleCategoryViewModel> ArticleCategories { get; set; }

        public async Task OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = await _articleCatgoryApplication.SearchAsync(searchModel);
        }

        [NeedsPermission(BlogPermissions.CreateAndEditArticleCategory)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateArticelCategory());
        }

        [NeedsPermission(BlogPermissions.CreateAndEditArticleCategory)]
        public async  Task<JsonResult> OnPostCreate(CreateArticelCategory command)
        {
            var result = await _articleCatgoryApplication.CreateAsync(command);
            return new JsonResult(result);
        }

        [NeedsPermission(BlogPermissions.CreateAndEditArticleCategory)]
        public async Task<IActionResult> OnGetEdit(long id)
        {
            var articleCategory = await _articleCatgoryApplication.GetDetailsAsync(id);
            return Partial("./Edit", articleCategory);
        }

        [NeedsPermission(BlogPermissions.CreateAndEditArticleCategory)]
        public async Task<JsonResult> OnPostEdit(EditArticleCategory command)
        {
            var result = await _articleCatgoryApplication.EditAsync(command);
            return new JsonResult(result);
        }
    }
}
