using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalletManagement.Application.Contracts.Wallet;

namespace ServiceHost.Areas.Administration.Pages.Wallets
{
    public class TransactionsModel : PageModel
    {
        private readonly IWalletApplication _walletApplication;

        public WalletViewModel Wallet { get; set; }
        public TransactionsModel(IWalletApplication walletApplication)
        {
            _walletApplication = walletApplication;
        }

        public async Task OnGet(Guid Id)
        {
            Wallet = await _walletApplication.GetWalletByIdAsync(Id);
        }
    }
}
