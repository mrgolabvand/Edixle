using _0_Framework.Application;
using AccountManagement.Domain.AccountAgg;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using WalletManagement.Application.Contracts.Wallet;
using WalletManagement.Domain.WalletAgg;

namespace WalletManagement.Application.Tests.Unit;

public class WalletApplicationTests
{
    private readonly IAccountRepository _accountRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly WalletApplication _walletApplication;

    public WalletApplicationTests()
    {
        _accountRepository = Substitute.For<IAccountRepository>();
        _walletRepository = Substitute.For<IWalletRepository>();
        _walletApplication = new WalletApplication(_walletRepository, _accountRepository);
    }
    [Fact]
    public async Task CreateAsync_ShouldReturnSuccessOperationResult_WhenWalletCreated()
    {
        var command = new CreateWallet{AccountId = 1};

        var result = await _walletApplication.CreateAsync(command);

        result.IsSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task CreateAsync_ShouldAddNewWallet()
    {
        var command = new CreateWallet { AccountId = 1 };

        await _walletApplication.CreateAsync(command);

        await _walletRepository.ReceivedWithAnyArgs().CreateAsync(default);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnFailOperationResultWithInvalidIdMessage_WhenInvalidAccountIdPassed()
    {
        var command = new CreateWallet { AccountId = -1 };

        var result = await _walletApplication.CreateAsync(command);

        result.IsSucceeded.Should().BeFalse();
        result.Message.Should().Be(ValidationMessage.InvalidId);
    }

    [Fact]
    public async Task AskForSettlementAsync_ShouldCallAskForSettlementAsyncOnWalletRepository()
    {
        var walletId = Guid.NewGuid();

        await _walletApplication.AskForSettlementAsync(walletId);

        await _walletRepository.Received().AskForSettlementAsync(walletId);
    }

    [Fact]
    public async Task SettlementAsync_ShouldCallSettlementAsyncOnWalletRepository()
    {
        var walletId = Guid.NewGuid();

        await _walletApplication.SettlementAsync(walletId);
        
        await _walletRepository.Received().SettlementAsync(walletId);
    }

    [Fact]
    public async Task GetWalletIdByAccountIdAsync_ShouldCallGetWalletIdByAccountIdAsyncOnRepository()
    {
        var accountId = 13;

        var _ = await _walletApplication.GetWalletIdByAccountIdAsync(accountId);

        await _walletRepository.Received().GetWalletIdByAccountIdAsync(accountId);
    }
    [Fact]
    public async Task GetWalletByAccountIdAsync_ShouldCallGetWalletByAccountIdAsyncOnRepository()
    {
        var accountId = 9;
        var command = new CreateWallet { AccountId = 9 };
        var result = await _walletApplication.CreateAsync(command);
        
        var _ = await _walletApplication.GetWalletByAccountIdAsync(accountId);

        await _walletRepository.Received().GetWalletByAccountIdAsync(accountId);
    }

}