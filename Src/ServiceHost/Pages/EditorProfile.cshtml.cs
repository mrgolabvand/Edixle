using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Account;

namespace ServiceHost.Pages
{
    public class EditorProfileModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;
        private readonly IAccountQuery _accountQuery;
        private readonly IPersonalPageApplication _personalPageApplication;

        public PersonalPageViewModel PersonalPageViewModel { get; set; }
        public AccountQueryModel AccountQueryModel { get; set; }
        [BindProperty] public ChangePassword Password { get; set; }
        [BindProperty] public EditAccount Account { get; set; }
        [BindProperty] public EditPersonalPage PersonalPage { get; set; }  
        public EditorProfileModel(IPersonalPageApplication personalPageApplication, IAuthHelper authHelper, IAccountApplication accountApplication, IAccountQuery accountQuery)
        {
            _personalPageApplication = personalPageApplication;
            _authHelper = authHelper;
            _accountApplication = accountApplication;
            _accountQuery = accountQuery;
        }

        public async Task OnGet()
        {
            var accountId = _authHelper.CurrentAccountId();
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(accountId);
            PersonalPage =  await _personalPageApplication.GetDetailsAsync(pageId);
            Account = await _accountApplication.GetDetailsAsync(accountId);
            PersonalPageViewModel = await _personalPageApplication.GetPageInfoByAsync(pageId);
            AccountQueryModel = await _accountQuery.GetAccountAsync(accountId);
        }

        public async Task<IActionResult> OnPostChangePassword()
        {
            Password.Id = _authHelper.CurrentAccountId();
            var result = await _accountApplication.ChangePasswordAsync(Password);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditPersonalPage()
        {
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            PersonalPage.Slug = PersonalPage.FullName.Slugify();
            PersonalPage.Id = pageId;
            PersonalPage.AccountId = _authHelper.CurrentAccountId();
            PersonalPage.Keywords = $"ادیتور لند, ادیتور, ادیت,EditorLand , {PersonalPage.FullName}";
            PersonalPage.MetaDescription =
                $"سایت ادیتور لند EditorLand سایت ارائه نمونه کار و استخدام ادیتور برای تولید کنندگان محتوا. {PersonalPage.FullName}";
            await _personalPageApplication.EditAsync(PersonalPage);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAccount()
        {
            var accountId = _authHelper.CurrentAccountId();
            var account = await _accountApplication.GetDetailsAsync(accountId);
            Account.RoleId = account.RoleId;
            Account.Id = accountId;
            await _accountApplication.EditAsync(Account);
            return RedirectToPage();
        }
    }
}
