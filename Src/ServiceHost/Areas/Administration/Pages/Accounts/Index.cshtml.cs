using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts
{
    public class IndexModel : PageModel
    {

        private readonly IRoleApplication _roleApplication;
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public List<AccountViewModel> Accounts { get; set; }
        public SelectList Roles { get; set; }

        public async Task OnGet(AccountSearchModel searchModel)
        {
            Accounts = await _accountApplication.SearchAsync(searchModel);
            Roles = new SelectList(await _roleApplication.GetListAsync(), "Id", "Name");
        }

        [NeedsPermission(AccountPermissions.CreateAndEditAccount)]
        public async Task<IActionResult> OnGetCreate()
        {
            var command = new RegisterAccount
            {
                Roles = await _roleApplication.GetListAsync()
            };
            return Partial("./Create", command);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditAccount)]
        public async Task<IActionResult> OnPostCreate(RegisterAccount command)
        {
            var result = await _accountApplication.RegisterAsync(command);
            return new JsonResult(result);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditAccount)]
        public async Task<IActionResult> OnGetEdit(long id)
        {
            var account = await _accountApplication.GetDetailsAsync(id);
            account.Roles = await _roleApplication.GetListAsync();
            return Partial("./Edit", account);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditAccount)]
        public async Task<IActionResult> OnPostEdit(EditAccount command)
        {
            var result = await _accountApplication.EditAsync(command);
            return new JsonResult(result);

        }

        [NeedsPermission(AccountPermissions.CreateAndEditAccount)]
        public IActionResult OnGetChangePassword(long id)
        {
            var command = new ChangePassword { Id = id };
            return Partial("./ChangePassword", command);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditAccount)]
        public async Task<IActionResult> OnPostChangePassword(ChangePassword command)
        {
            var result = await _accountApplication.ChangePasswordAsync(command);
            return new JsonResult(result);
        }
    }
}
