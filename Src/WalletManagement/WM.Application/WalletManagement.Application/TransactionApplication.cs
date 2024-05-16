using _0_Framework.Application;
using WalletManagement.Application.Contracts.Transaction;
using WalletManagement.Domain.TransactionAgg;
using WalletManagement.Domain.WalletAgg;

namespace WalletManagement.Application;

public class TransactionApplication : ITransactionApplication
{
    private readonly IWalletRepository _walletRepository;
    private readonly ITransactionRepository _transactionRepository;
    public TransactionApplication(ITransactionRepository transactionRepository, IWalletRepository walletRepository)
    {
        _transactionRepository = transactionRepository;
        _walletRepository = walletRepository;
    }

    public async ValueTask<(OperationResult, Guid)> AddAsync(AddTransaction addTransaction)
    {
        var result = new OperationResult();

        if (addTransaction.Amount is <= 0 or >= 1000_000_000)
            return (result.Failed(ValidationMessage.InvalidAmount), Guid.Empty);

        if (addTransaction.Description.Length >= 200)
            return (result.Failed(ValidationMessage.MaxLength), Guid.Empty);

        var transaction = new Transaction(addTransaction.WalletId, addTransaction.ReceiverWalletId,
            addTransaction.Amount, addTransaction.Description);

        var transactionId = await _transactionRepository.CreateAndReturnIdAsync(transaction);



        await _walletRepository.SaveChangesAsync();

        return (result.Succeeded(), transactionId);
    }

    public async ValueTask SuccessAsync(Guid transactionId)
    {
        var transaction = await _transactionRepository.GetAsync(transactionId);
        if (transaction == null) return;
        if (transaction.IsDeposit)
        {
            await _walletRepository.IncreaseCreditAsync(transaction.WalletId, transaction.Amount);
        }
        else
        {
            if (transaction.ReceiverWalletId == Guid.Empty)
            {
                await _walletRepository.DecreaseCreditAsync(transaction.WalletId, transaction.Amount);
            }
            else
            {
                await _walletRepository.DecreaseCreditAsync(transaction.WalletId, transaction.Amount);
                await _walletRepository.IncreaseCreditAsync(transaction.ReceiverWalletId, transaction.Amount);
            }
        }
        await _transactionRepository.SuccessAsync(transactionId);
        await _walletRepository.SaveChangesAsync();
    }

    public async ValueTask FailAsync(Guid transactionId) => await _transactionRepository.FailAsync(transactionId);

    public async ValueTask<decimal> GetAmountByIdAsync(Guid transactionId) =>
        await _transactionRepository.GetAmountByIdAsync(transactionId);
}