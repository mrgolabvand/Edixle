using WalletManagement.Domain.TransactionAgg;

namespace WalletManagement.Domain.Tests.Unit.TransactionAgg;

public class TransactionTestBuilder
{
    private Guid _walletId = Guid.NewGuid();
    private Guid _receiverWalletId = Guid.NewGuid();
    private decimal _amount = 10000;
    private string _description = "test";

    public TransactionTestBuilder WithWalletId(Guid walletId)
    {
        _walletId = walletId;
        return this;
    }

    public TransactionTestBuilder WithReceiverWalletId(Guid receiverWalletId)
    {
        _receiverWalletId = receiverWalletId;
        return this;
    }

    public TransactionTestBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public TransactionTestBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public Transaction Build() =>
        new(_walletId, _receiverWalletId, _amount, _description);
}