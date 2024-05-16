using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.EmployerPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalletManagement.Application.Contracts.Wallet;

namespace ServiceHost.Pages
{
    public class AddEmployerPageModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;
        private readonly IEmployerPageApplication _employerPageApplication;

        [BindProperty] public AddEmployerPage EmployerPage { get; set; }

        public AddEmployerPageModel(IAuthHelper authHelper, IAccountApplication accountApplication, IEmployerPageApplication employerPageApplication, IWalletApplication walletApplication)
        {
            _authHelper = authHelper;
            _accountApplication = accountApplication;
            _employerPageApplication = employerPageApplication;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetValidate()
        {
            var id = _authHelper.CurrentAccountId();
            return await _accountApplication.IsEmployerAsync(id) ? RedirectToPage("./EmployerPage") : RedirectToPage();
        }

        public async Task<IActionResult> OnPost()
        {
            var accountId = _authHelper.CurrentAccountId();
            
            if (!ModelState.IsValid)
                return RedirectToPage();
            if(await _accountApplication.IsEditorAsync(accountId))
                return RedirectToPage("./Index");
            
            EmployerPage.AccountId = accountId;
            await _employerPageApplication.AddAsync(EmployerPage);

            return RedirectToPage("./EmployerPage");
        }
    }
}
