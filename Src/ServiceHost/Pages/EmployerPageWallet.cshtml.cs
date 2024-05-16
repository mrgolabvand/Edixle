using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using AccountManagement.Application.Contracts.EmployerPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalletManagement.Application.Contracts.Transaction;
using WalletManagement.Application.Contracts.Wallet;

namespace ServiceHost.Pages
{
    public class EmployerPageWalletModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IWalletApplication _walletApplication;
        private readonly ITransactionApplication _transactionApplication;
        private readonly IEmployerPageApplication _employerPageApplication;
        private readonly IZarinPalFactory _zarinPalFactory;


        public EmployerPageViewModel EmployerPageViewModel { get; set; }
        public WalletViewModel Wallet { get; set; }
        [BindProperty] public AddTransaction Transaction { get; set; }

        public EmployerPageWalletModel(IAuthHelper authHelper, IEmployerPageApplication employerPageApplication, IWalletApplication walletApplication, ITransactionApplication transactionApplication, IZarinPalFactory zarinPalFactory)
        {
            _authHelper = authHelper;
            _employerPageApplication = employerPageApplication;
            _walletApplication = walletApplication;
            _transactionApplication = transactionApplication;
            _zarinPalFactory = zarinPalFactory;
        }

        public async Task OnGet()
        {
            var accountId = _authHelper.CurrentAccountId();
            var employerPageId = await _employerPageApplication.GetEmployerPageIdByAccountIdAsync(accountId);
            EmployerPageViewModel = await _employerPageApplication.GetViewModelAsync(employerPageId);
            Wallet = await _walletApplication.GetWalletByAccountIdAsync(accountId);
        }

        public async Task<IActionResult> OnGetAskForSettlement()
        {
            var accountId = _authHelper.CurrentAccountId();
            var wallet = await _walletApplication.GetWalletByAccountIdAsync(accountId);
            var addTransaction = new AddTransaction()
            {
                Amount = wallet.CurrentCredit,
                Description = "واریز موجودی به حساب بانکی",
                ReceiverWalletId = Guid.Empty,
                WalletId = wallet.Id
            };

            await _walletApplication.AskForSettlementAsync(wallet.Id);
            var (_, transactionId) =await _transactionApplication.AddAsync(addTransaction);
            await _transactionApplication.SuccessAsync(transactionId);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostPay()
        {
            var walletId = await _walletApplication.GetWalletIdByAccountIdAsync(_authHelper.CurrentAccountId());
            Transaction.Description = "افزایش موجودی";
            Transaction.WalletId = walletId;
            Transaction.ReceiverWalletId = walletId;
            var (_, orderId) = await _transactionApplication.AddAsync(Transaction);


            var paymentResponse = _zarinPalFactory.CreatePaymentRequest(Transaction.Amount.ToString(), "", "", $"افزایش موجودی کیف پول", orderId, "EmployerPageWallet");
            return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }


        public async Task<IActionResult> OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] Guid oId)
        {
            var orderAmount = await _transactionApplication.GetAmountByIdAsync(oId);
            var stringOrderAmount = orderAmount.ToString("F0");
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, stringOrderAmount);

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status == 100)
            {
                await _transactionApplication.SuccessAsync(oId);
                result.Succeeded("افزایش موجودی با موفقیت انجام شد.", "0");
                return RedirectToPage("./PaymentResult", result);
            }
            result = result.Failed("پرداخت ناموفق بود، در صورت کسر وجه تا 24 ساعت دیگر بازگشت داده میشود.");
            return RedirectToPage("./PaymentResult", result);
        }
    }
}
