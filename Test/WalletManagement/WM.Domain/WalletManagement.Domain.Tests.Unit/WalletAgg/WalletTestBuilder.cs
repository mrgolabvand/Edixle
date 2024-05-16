using WalletManagement.Domain.WalletAgg;

namespace WalletManagement.Domain.Tests.Unit.WalletAgg;

public class WalletTestBuilder
{
    private long _accoundId = 1;

    public WalletTestBuilder WithAccountId(long accountId)
    {
        _accoundId = accountId;
        return this;
    }

    public Wallet Build() => new(_accoundId);
}