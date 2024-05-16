using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalletManagement.Application.Contracts.Wallet;

namespace ServiceHost.Pages
{
    public class AddPersonalInformationModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;
        private readonly IPersonalPageApplication _personalPageApplication;

        public EditAccount Account { get; set; }
        [BindProperty] public CreatePersonalPage PersonalPage { get; set; }
        [TempData] public string Message { get; set; }

        public AddPersonalInformationModel(IAuthHelper authHelper, IAccountApplication accountApplication, IPersonalPageApplication personalPageApplication, IWalletApplication walletApplication)
        {
            _authHelper = authHelper;
            _accountApplication = accountApplication;
            _personalPageApplication = personalPageApplication;
        }

        public async Task OnGet()
        {
            var id = _authHelper.CurrentAccountId();
            Account = await _accountApplication.GetDetailsAsync(id);
        }
        public async Task<IActionResult> OnGetValidate()
        {
            var id = _authHelper.CurrentAccountId();
            return await _accountApplication.IsEditorAsync(id) ? RedirectToPage("./EditPersonalInformation") : RedirectToPage();
        }
        public async Task<IActionResult> OnPost()
        {
            var accountId = _authHelper.CurrentAccountId();

            if (!ModelState.IsValid)
                return RedirectToPage();
            if (await _accountApplication.IsEmployerAsync(accountId))
                return RedirectToPage("./Index");
            
            PersonalPage.Slug = PersonalPage.FullName.Slugify();
            PersonalPage.Keywords = $"ادیتور لند, ادیتور, ادیت,EditorLand , {PersonalPage.FullName}";
            PersonalPage.MetaDescription =
                $"سایت ادیتور لند EditorLand سایت ارائه نمونه کار و استخدام ادیتور برای تولید کنندگان محتوا. {PersonalPage.FullName}";

            var result = await _personalPageApplication.CreateAsync(PersonalPage);

            if (result.IsSucceeded) return RedirectToPage("./AddPortfolio");
            
            Message = result.Message;
            return RedirectToPage();
        }
    }
}
