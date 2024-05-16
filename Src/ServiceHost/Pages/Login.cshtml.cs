using System.Threading.Tasks;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty] public Login Login { get; set; }
        [TempData] public string Message { get; set; }

        private readonly IAccountApplication _accountApplication;

        public LoginModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            if (Login.Password == null || Login.UserName == null)
            {
                Message = "مقادیر خواسته شده را وارد کنید.";
                return RedirectToPage("./Login");
            }

            var result = await _accountApplication.LoginAsync(Login);
            if (!result.IsSucceeded)
                Message = result.Message;
            return RedirectToPage(result.IsSucceeded ? "./Index" : "./Login");
        }
        public async Task<IActionResult> OnPostLogin(Login login)
        {
            if (login.Password == null || login.UserName == null)
            {
                Message = "مقادیر خواسته شده را وارد کنید.";
                return RedirectToPage("./Login");
            }

            var result = await _accountApplication.LoginAsync(login);
            if (!result.IsSucceeded)
                Message = result.Message;
            return RedirectToPage(result.IsSucceeded ? "./Index" : "./Login");
        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("./Index");
        }
    }
}
