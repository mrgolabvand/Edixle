using _0_Framework.Domain;

namespace WalletManagement.Domain.TransactionAgg;

public interface ITransactionRepository : IBaseRepository<Guid, Transaction>
{
    ValueTask<Guid> CreateAndReturnIdAsync(Transaction transaction);
    ValueTask SuccessAsync(Guid walletId);
    ValueTask FailAsync(Guid walletId);
    ValueTask<decimal> GetAmountByIdAsync(Guid transactionId);
}