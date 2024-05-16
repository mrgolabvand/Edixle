using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.JobOffer;
using AccountManagement.Application.Contracts.Project;
using AccountManagement.Application.Contracts.ProjectOffer;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.JobOffers
{
    public class IndexModel : PageModel
    {
        private readonly IJobOfferApplication _jobOfferApplication;

        public IndexModel(IJobOfferApplication reviewApplication)
        {
            _jobOfferApplication = reviewApplication;
        }

        [TempData]
        public string Message { get; set; }

        public List<JobOfferViewModel> Projects { get; set; }


        public async Task OnGet(string title)
        {
            Projects = await _jobOfferApplication.SearchAsync(title);
        }


        [NeedsPermission(CommentPermissions.ConfirmAndCancelComment)]
        public async Task<IActionResult> OnGetConfirm(long id)
        {
           await _jobOfferApplication.AcceptAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
