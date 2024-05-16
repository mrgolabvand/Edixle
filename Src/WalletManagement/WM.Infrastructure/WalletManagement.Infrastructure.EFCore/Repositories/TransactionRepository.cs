using _0_Framework.Infrastructure;
using WalletManagement.Domain.TransactionAgg;

namespace WalletManagement.Infrastructure.EFCore.Repositories;

public class TransactionRepository : BaseRepository<Guid, Transaction>, ITransactionRepository
{
    private readonly WalletContext _walletContext;

    public TransactionRepository(WalletContext walletContext) : base(walletContext)
    {
        _walletContext = walletContext;
    }

    public async ValueTask<Guid> CreateAndReturnIdAsync(Transaction transaction)
    {
        await _walletContext.AddAsync(transaction);
        return transaction.Id;
    }

    public async ValueTask SuccessAsync(Guid transactionId)
    {
        var transaction = await GetAsync(transactionId);
        transaction.Success();
    }

    public async ValueTask FailAsync(Guid transactionId)
    {
        var transaction = await GetAsync(transactionId);
        transaction.Failed();
    }

    public async ValueTask<decimal> GetAmountByIdAsync(Guid transactionId)
    {
        var transaction = await GetAsync(transactionId);
        return transaction.Amount;
    }
}