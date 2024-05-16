using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalletManagement.Application.Contracts.Wallet;

namespace ServiceHost.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IWalletApplication _walletApplication;
        public RegisterModel(IAccountApplication accountApplication, IWalletApplication walletApplication)
        {
            _accountApplication = accountApplication;
            _walletApplication = walletApplication;
        }

        [BindProperty] public RegisterAccount RegisterAccount { get; set; }
        //[BindProperty] public string PhoneNumber { get; set; }
        [TempData] public string Message { get; set; }

        public void OnGet(string? phone)
        {
            if (!string.IsNullOrWhiteSpace(phone))
            {
                RegisterAccount = new()
                {
                    PhoneNumber = phone
                };
            }
        }
        public async Task<IActionResult> OnPost()
        {
            if (RegisterAccount.Password == null || RegisterAccount.UserName == null || RegisterAccount.Email == null)
            {
                return RedirectToPage("./Register");
            }

            if (RegisterAccount.Password != RegisterAccount.RepeatPassword)
            {
                Message = "رمز عبور و تکرار آن یکسان نیست.";
                return RedirectToPage();
            }

            const string regex = @"^[A-Za-z\d_-]+$";
            if (!Regex.Match(RegisterAccount.UserName, regex).Success)
            {
                Message = "برای نام کاربری فقط حروف انگلیسی و اعداد مجاز است.";
                return RedirectToPage();
            }
            if (!await _accountApplication.VerifyCode(RegisterAccount.PhoneNumber, RegisterAccount.VerificationCode))
            {
                return RedirectToPage();
            }

            //RegisterAccount.PhoneNumber = PhoneNumber;
            var result = await _accountApplication.RegisterAsync(RegisterAccount);
    
            if (!result.IsSucceeded)
            {
                Message = result.Message;
                return RedirectToPage();
            }

            var login = new Login()
            {
                Password = RegisterAccount.Password,
                UserName = RegisterAccount.UserName,
            };
            
            var accountId = await _accountApplication.GetIdBy(RegisterAccount.UserName);
            var createWallet = new CreateWallet()
            {
                AccountId = accountId
            };
          
            await _walletApplication.CreateAsync(createWallet);

            await _accountApplication.LoginAsync(login);
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostSendVerificationCode()
        {
            if (!RegisterAccount.PhoneNumber.StartsWith("09"))
            {
                Message = "فقط شماره ایران قابل قبول است.";
                return RedirectToPage();
            }

            var result = await _accountApplication.SendVerificationCode(RegisterAccount.PhoneNumber);
            if (!result.IsSucceeded)
            {
                Message = result.Message;
                return RedirectToPage();
            }
            return RedirectToPage(routeValues: new { phone = RegisterAccount.PhoneNumber });
        }
    }
}
