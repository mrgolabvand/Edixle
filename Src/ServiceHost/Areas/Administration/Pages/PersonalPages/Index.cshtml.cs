using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.PersonalPages
{
    public class IndexModel : PageModel
    {
        private readonly IPersonalPageApplication _personalPageApplication;

        public IndexModel(IPersonalPageApplication accountApplication)
        {
            _personalPageApplication = accountApplication;
        }

        public List<PersonalPageViewModel> Pages { get; set; }

        public PersonalPageSearchModel SearchModel { get; set; }

        public async Task OnGet()
        {
            Pages = await _personalPageApplication.SearchAsync(SearchModel);
        }


        [NeedsPermission(AccountPermissions.ConfirmAndCancelPage)]
        public async Task<IActionResult> OnGetConfirm(long id)
        {
            await _personalPageApplication.ConfirmAsync(id);
            return RedirectToPage("./Index");
        }

        [NeedsPermission(AccountPermissions.ConfirmAndCancelPage)]
        public async Task<IActionResult> OnGetCancel(long id)
        {
            await _personalPageApplication.CancelAsync(id);
            return RedirectToPage("./Index");
        }

    }
}
