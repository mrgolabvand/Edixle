using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Project;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly IProjectApplication _projectApplication;

        public IndexModel(IProjectApplication reviewApplication)
        {
            _projectApplication = reviewApplication;
        }

        [TempData]
        public string Message { get; set; }

        public List<ProjectViewModel> Projects { get; set; }


        public async Task OnGet(string title)
        {
            Projects = await _projectApplication.SearchAsync(title);
        }


        [NeedsPermission(CommentPermissions.ConfirmAndCancelComment)]
        public async Task<IActionResult> OnGetCancel(long id)
        {
            await _projectApplication.RejectAsync(id);
            return RedirectToPage("./Index");
        }

        [NeedsPermission(CommentPermissions.ConfirmAndCancelComment)]
        public async Task<IActionResult> OnGetConfirm(long id)
        {
            await _projectApplication.ConfirmAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
