using WalletManagement.Application.Contracts.Transaction;

namespace WalletManagement.Application.Tests.Unit.TestBuilders;

public class AddTransactionTestBuilder
{
    private Guid _walletId = Guid.NewGuid();
    private Guid _receiverWalletId = Guid.NewGuid();
    private decimal _amount = 20_000;
    private string _description = "Test";

    public AddTransactionTestBuilder WithWalletId(Guid id)
    {
        _walletId = id;
        return this;
    }
    public AddTransactionTestBuilder WitReceiverWalletId(Guid id)
    {
        _receiverWalletId = id;
        return this;
    }
    public AddTransactionTestBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }
    public AddTransactionTestBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }
    public AddTransaction Build() =>
        new AddTransaction
        {
            WalletId = _walletId,
            ReceiverWalletId = _receiverWalletId,
            Amount = _amount,
            Description = _description
        };
}