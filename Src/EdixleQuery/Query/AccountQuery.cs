using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.Account;
using Microsoft.EntityFrameworkCore;
using WalletManagement.Application.Contracts.Wallet;
using WalletManagement.Infrastructure.EFCore;

namespace EdixleQuery.Query
{
    public class AccountQuery : IAccountQuery
    {
        private readonly AccountContext _context;
        private readonly WalletContext _walletContext;
        private readonly IWalletApplication _walletApplication;
        public AccountQuery(AccountContext context, IWalletApplication walletApplication, WalletContext walletContext)
        {
            _context = context;
            _walletApplication = walletApplication;
            _walletContext = walletContext;
        }

        public async ValueTask<AccountQueryModel> GetAccountAsync(long id) =>
            await _context.Accounts.Select(v => new AccountQueryModel
            {
                UserName = v.UserName,
                Email = v.Email,
                PhoneNumber = v.PhoneNumber,
                RoleId = v.RoleId,
                Id = v.Id
            }).FirstOrDefaultAsync(v => v.Id == id);

   
    }
}
