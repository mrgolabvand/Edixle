using _0_Framework.Application;

namespace WalletManagement.Application.Contracts.Wallet;

public interface IWalletApplication
{
    ValueTask<OperationResult> CreateAsync(CreateWallet command);
    ValueTask SettlementAsync(Guid walletId);
    ValueTask AskForSettlementAsync(Guid walletId);
    ValueTask<Guid> GetWalletIdByAccountIdAsync(long accountId);
    ValueTask<WalletViewModel> GetWalletByAccountIdAsync(long accountId);
    ValueTask<WalletViewModel> GetWalletByIdAsync(Guid id);
    ValueTask<List<WalletViewModel>> GetAllAsync();
}