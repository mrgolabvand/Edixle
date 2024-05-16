using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Project;
using AccountManagement.Application.Contracts.ProjectOffer;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.ProjectOffers
{
    public class IndexModel : PageModel
    {
        private readonly IProjectOfferApplication _projectOfferApplication;

        public IndexModel(IProjectOfferApplication reviewApplication)
        {
            _projectOfferApplication = reviewApplication;
        }

        [TempData]
        public string Message { get; set; }

        public List<ProjectOfferViewModel> Projects { get; set; }


        public async Task OnGet(string title)
        {
            Projects = await _projectOfferApplication.SearchAsync(title);
        }


        [NeedsPermission(CommentPermissions.ConfirmAndCancelComment)]
        public async Task<IActionResult> OnGetConfirm(long id)
        {
           await _projectOfferApplication.AcceptAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
