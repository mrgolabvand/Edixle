using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReviewManagement.Application.Contracts.Review;

namespace ServiceHost.Areas.Administration.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly IReviewApplication _reviewApplication;

        public IndexModel(IReviewApplication reviewApplication)
        {
            _reviewApplication = reviewApplication;
        }

        [TempData]
        public string Message { get; set; }

        public List<ReviewViewModel> Reviews { get; set; }

        public ReviewSearchModel SearchModel { get; set; }

        public async Task OnGet(ReviewSearchModel searchModel)
        {
            Reviews = await _reviewApplication.SearchAsync(searchModel);
        }


        [NeedsPermission(CommentPermissions.ConfirmAndCancelComment)]
        public async Task<IActionResult> OnGetCancel(long id)
        {
            await _reviewApplication.CancelAsync(id);
            return RedirectToPage("./Index");
        }

        [NeedsPermission(CommentPermissions.ConfirmAndCancelComment)]
        public async Task<IActionResult> OnGetConfirm(long id)
        {
           await _reviewApplication.ConfirmAsync(id);

            return RedirectToPage("./Index");
        }
    }
}
