using _0_Framework.Application;
using AccountManagement.Domain.AccountAgg;
using WalletManagement.Application.Contracts.Wallet;
using WalletManagement.Domain.TransactionAgg;
using WalletManagement.Domain.WalletAgg;

namespace WalletManagement.Application
{
    public class WalletApplication : IWalletApplication
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IAccountRepository _accountRepository;
        public WalletApplication(IWalletRepository walletRepository, IAccountRepository accountRepository)
        {
            _walletRepository = walletRepository;
            _accountRepository = accountRepository;
        }

        public async ValueTask<OperationResult> CreateAsync(CreateWallet command)
        {
            if (command.AccountId <= 0)
                return new OperationResult().Failed(ValidationMessage.InvalidId);

            var wallet = new Wallet(command.AccountId);

            await _walletRepository.CreateAsync(wallet);

            await _walletRepository.SaveChangesAsync();

            return new OperationResult().Succeeded();
        }

        public async ValueTask AskForSettlementAsync(Guid walletId)
        {
            await _walletRepository.AskForSettlementAsync(walletId);
            await _walletRepository.SaveChangesAsync();
        }

        public async ValueTask SettlementAsync(Guid walletId)
        {
            await _walletRepository.SettlementAsync(walletId);
            await _walletRepository.SaveChangesAsync();
        }

        public async ValueTask<Guid> GetWalletIdByAccountIdAsync(long accountId) =>
            await _walletRepository.GetWalletIdByAccountIdAsync(accountId);

        public async ValueTask<WalletViewModel> GetWalletByAccountIdAsync(long accountId)
        {
            var wallet = await _walletRepository.GetWalletByAccountIdAsync(accountId);

            foreach (var transaction in wallet.Transactions)
            {
                if (transaction.ReceiverWalletId == Guid.Empty)
                {
                    transaction.ReceiverAccountName = "";
                }
                else
                {
                    var receiverAccountId = await _walletRepository.GetAccountIdByWalletIdAsync(transaction.ReceiverWalletId);
                    var receiverAccount = await _accountRepository.GetDetailsAsync(receiverAccountId);
                    transaction.ReceiverAccountName = receiverAccount.UserName;
                }
            }

            return wallet;
        }

        public async ValueTask<WalletViewModel> GetWalletByIdAsync(Guid id)
        {
            var wallet = await _walletRepository.GetWalletByIdAsync(id);

            foreach (var transaction in wallet.Transactions)
            {
                if (transaction.ReceiverWalletId == Guid.Empty)
                {
                    transaction.ReceiverAccountName = "";
                }
                else
                {
                    var receiverAccountId = await _walletRepository.GetAccountIdByWalletIdAsync(transaction.ReceiverWalletId);
                    var receiverAccount = await _accountRepository.GetDetailsAsync(receiverAccountId);
                    transaction.ReceiverAccountName = receiverAccount.UserName;
                }
            }

            return wallet;
        }

        public  async ValueTask<List<WalletViewModel>> GetAllAsync()
        {
            var wallets = await _walletRepository.GetAllAsync();
            var accounts = await _accountRepository.GetListAsync();
            wallets.ForEach(v => v.AccountName = accounts.First(a => a.Id == v.AccountId).UserName);
            return wallets;
        }
    }
}