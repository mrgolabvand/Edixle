using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Comments
{
    public class IndexModel : PageModel
    {
        private readonly ICommentApplication _commentApplication;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        [TempData]
        public string Message { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public CommentSearchModel SearchModel { get; set; }

        public async Task OnGet(CommentSearchModel searchModel)
        {
            Comments = await _commentApplication.SearchAsync(searchModel);
        }


        [NeedsPermission(CommentPermissions.ConfirmAndCancelComment)]
        public async Task<IActionResult> OnGetCancel(long id)
        {
            var result = await _commentApplication.CancelAsync(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        [NeedsPermission(CommentPermissions.ConfirmAndCancelComment)]
        public async Task<IActionResult> OnGetConfirm(long id)
        {
            var result = await _commentApplication.ConfirmAsync(id);
            if (result.IsSucceeded)
            {
                return RedirectToPage("./Index");
            }

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
