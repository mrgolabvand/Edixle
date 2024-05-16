using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using AccountManagement.Application.Contracts.PersonalPage;
using EdixleQuery.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalletManagement.Application.Contracts.Transaction;
using WalletManagement.Application.Contracts.Wallet;

namespace ServiceHost.Pages
{
    public class EditorProfileWalletModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IAccountQuery _accountQuery;
        private readonly IWalletApplication _walletApplication;
        private readonly ITransactionApplication _transactionApplication;
        private readonly IPersonalPageApplication _personalPageApplication;
        public PersonalPageViewModel PersonalPageViewModel { get; set; }
        public AccountQueryModel AccountQueryModel { get; set; }
        public WalletViewModel Wallet { get; set; }

        public EditorProfileWalletModel(IAuthHelper authHelper, IAccountQuery accountQuery, IPersonalPageApplication personalPageApplication, IWalletApplication walletApplication, ITransactionApplication transactionApplication)
        {
            _authHelper = authHelper;
            _accountQuery = accountQuery;
            _personalPageApplication = personalPageApplication;
            _walletApplication = walletApplication;
            _transactionApplication = transactionApplication;
        }

        public async Task OnGet()
        {
            var accountId = _authHelper.CurrentAccountId();
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(accountId);
            PersonalPageViewModel = await _personalPageApplication.GetPageInfoWithPlanByAsync(pageId);
            AccountQueryModel = await _accountQuery.GetAccountAsync(accountId);
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
            var (_, transactionId) = await _transactionApplication.AddAsync(addTransaction);
            await _transactionApplication.SuccessAsync(transactionId);

            return RedirectToPage();
        }
    }
}
