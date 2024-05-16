using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class EditPersonalInformationModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IAccountApplication _accountApplication;
        private readonly IPersonalPageApplication _personalPageApplication;

        public EditPersonalInformationModel(IAuthHelper authHelper, IAccountApplication accountApplication, IPersonalPageApplication personalPageApplication)
        {
            _authHelper = authHelper;
            _accountApplication = accountApplication;
            _personalPageApplication = personalPageApplication;
        }


        public EditAccount Account { get; set; }
        [BindProperty] public EditPersonalPage PersonalPage { get; set; }
        [TempData] public string Message { get; set; }
        public async Task OnGet()
        {
            var id = _authHelper.CurrentAccountId();
            PersonalPage = await _personalPageApplication.GetDetailsAsync(await _personalPageApplication.GetPersonalPageIdByAsync(id));
            Account = await _accountApplication.GetDetailsAsync(id);
        }

        public async Task<IActionResult> OnGetValidate()
        {
            var id = _authHelper.CurrentAccountId();
            return !await _accountApplication.IsEditorAsync(id) ? RedirectToPage("./AddPersonalInformation") : RedirectToPage();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();
            PersonalPage.Slug = PersonalPage.FullName.Slugify();
            PersonalPage.Keywords = $"ادیتور لند, ادیتور, ادیت,EditorLand , {PersonalPage.FullName}";
            PersonalPage.MetaDescription =
                $"سایت ادیتور لند EditorLand سایت ارائه نمونه کار و استخدام ادیتور برای تولید کنندگان محتوا. {PersonalPage.FullName}";
            
            var result = await _personalPageApplication.EditAsync(PersonalPage);

            if (result.IsSucceeded) return RedirectToPage("./AddPortfolio");

            Message = result.Message;
            return RedirectToPage();
        }
    }
}
