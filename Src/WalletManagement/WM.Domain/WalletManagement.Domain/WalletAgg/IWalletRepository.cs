using _0_Framework.Domain;
using WalletManagement.Application.Contracts.Wallet;

namespace WalletManagement.Domain.WalletAgg;

public interface IWalletRepository : IBaseRepository<Guid, Wallet>
{
    ValueTask IncreaseCreditAsync(Guid walletId, decimal amount);
    ValueTask DecreaseCreditAsync(Guid walletId, decimal amount);
    ValueTask AskForSettlementAsync(Guid walletId);
    ValueTask SettlementAsync(Guid walletId);
    ValueTask<Guid> GetWalletIdByAccountIdAsync(long accountId);
    ValueTask<long> GetAccountIdByWalletIdAsync(Guid walletId);
    ValueTask<WalletViewModel> GetWalletByAccountIdAsync(long accountId);
    ValueTask<bool> HasCredit(Guid walletId, decimal value);
    ValueTask<List<WalletViewModel>> GetAllAsync();
    ValueTask<WalletViewModel> GetWalletByIdAsync(Guid id);
}