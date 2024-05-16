using _0_Framework.Domain;
using WalletManagement.Domain.WalletAgg;

namespace WalletManagement.Domain.TransactionAgg;

public class Transaction : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid WalletId { get; private set; }
    public Guid ReceiverWalletId { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }
    public bool IsDeposit { get; private set; }
    public Wallet Wallet { get; private set; }
    public bool IsSuccess { get; private set; }

    public Transaction(Guid walletId, Guid receiverWalletId, decimal amount, string description)
    {
        Id = Guid.NewGuid();
        WalletId = walletId;
        ReceiverWalletId = receiverWalletId;
        Amount = amount is >= 0 and <= 1000_000_000 ? amount : 0;
        Description = description;
        IsDeposit = walletId == receiverWalletId;
    }

    public void Success() => IsSuccess = true;

    public void Failed() => IsSuccess = false;
}