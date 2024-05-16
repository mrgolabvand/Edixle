using _0_Framework.Application;

namespace WalletManagement.Application.Contracts.Transaction;

public interface ITransactionApplication
{
    ValueTask<(OperationResult, Guid)> AddAsync(AddTransaction addTransaction);
    ValueTask SuccessAsync(Guid transactionId);
    ValueTask FailAsync(Guid transactionId);
    ValueTask<decimal> GetAmountByIdAsync(Guid transactionId);
}