using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Role
{
    public class CreateModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;

        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        [BindProperty]
        public CreateRole Command { get; set; }

        [NeedsPermission(AccountPermissions.CreateAndEditRole)]
        public void OnGet()
        {
        }

        [NeedsPermission(AccountPermissions.CreateAndEditRole)]
        public IActionResult OnPost()
        {
            _roleApplication.CreateAsync(Command);
            return RedirectToPage("Index");
        }
    }
}
