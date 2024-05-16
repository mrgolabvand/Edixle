using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Project;
using AccountManagement.Application.Contracts.ProjectOffer;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WalletManagement.Application.Contracts.Wallet;

namespace ServiceHost.Areas.Administration.Pages.Wallets
{
    public class IndexModel : PageModel
    {
        private readonly IWalletApplication _walletApplication;

        public IndexModel(IWalletApplication walletApplication)
        {
            _walletApplication = walletApplication;
        }

        [TempData]
        public string Message { get; set; }

        public List<WalletViewModel> Wallets { get; set; }


        public async Task OnGet(string title)
        {
            Wallets = await _walletApplication.GetAllAsync();
        }


        public async Task<IActionResult> OnGetSettlement(Guid id)
        {
            await _walletApplication.SettlementAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
