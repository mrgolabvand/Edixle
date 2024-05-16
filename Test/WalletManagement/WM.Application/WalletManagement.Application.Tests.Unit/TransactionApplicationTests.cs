using System.Text;
using _0_Framework.Application;
using FluentAssertions;
using NSubstitute;
using WalletManagement.Application.Contracts.Transaction;
using WalletManagement.Application.Tests.Unit.TestBuilders;
using WalletManagement.Domain.TransactionAgg;
using WalletManagement.Domain.WalletAgg;

namespace WalletManagement.Application.Tests.Unit;

public class TransactionApplicationTests
{
    private readonly IWalletRepository _walletRepository;
    private readonly AddTransactionTestBuilder _builder;
    private readonly ITransactionRepository _transactionRepository;
    private readonly TransactionApplication _transactionApplication;
    public TransactionApplicationTests()
    {
        _builder = new AddTransactionTestBuilder();
        _walletRepository = Substitute.For<IWalletRepository>();
        _transactionRepository = Substitute.For<ITransactionRepository>();
        _transactionApplication = new TransactionApplication(_transactionRepository, _walletRepository);
    }

    [Fact]
    public async Task AddAsync_ShouldCallCreateAndReturnIdAsync_WhenTransactionAdded()
    {
        var addTransaction = _builder.Build();

        var (_, _) = await _transactionApplication.AddAsync(addTransaction);

        await _transactionRepository.ReceivedWithAnyArgs().CreateAndReturnIdAsync(default);
    }

    [Fact]
    public async Task AddAsync_ShouldAddNewTransaction()
    {
        var addTransaction = _builder.Build();

        await _transactionApplication.AddAsync(addTransaction);

        await _transactionRepository.ReceivedWithAnyArgs().CreateAndReturnIdAsync(default);
    }

    [Theory]
    [InlineData(-10000)]
    [InlineData(0)]
    [InlineData(2000_000_000)]
    public async Task AddAsync_ShouldReturnFailOperationResultWithInvalidAmountMessage_WhenInvalidAmountPassed(decimal amount)
    {
        var addTransaction = _builder.WithAmount(amount).Build();

        var (result, transactionId) = await _transactionApplication.AddAsync(addTransaction);

        result.IsSucceeded.Should().BeFalse();
        result.Message.Should().Be(ValidationMessage.InvalidAmount);
    }

    [Fact]
    public async Task AddAsync_ShouldReturnFailOperationResultWithMaxLengthMessage_WhenDescriptionLengthIsSoLarge()
    {
        var addTransaction = _builder.WithDescription("Test".PadRight(210)).Build();

        var (result, transactionId) = await _transactionApplication.AddAsync(addTransaction);

        result.IsSucceeded.Should().BeFalse();
        result.Message.Should().Be(ValidationMessage.MaxLength);
    }

    [Fact]
    public async Task SuccessAsync_ShouldCallIncreaseForWallet_WhenIsDepositTrue()
    {
        var id = Guid.NewGuid();
        var transactionViewModel = new TransactionViewModel()
        {
            WalletId = id,
            ReceiverWalletId = id,
            Amount = 10000
        };

        await _transactionApplication.SuccessAsync(id);

        var isDeposit = true;
        isDeposit.Should().BeTrue();
        await _walletRepository.Received().IncreaseCreditAsync(id, transactionViewModel.Amount);
    }

    [Fact]
    public async Task SuccessAsync_ShouldCallDecreaseForSenderWalletAndCallIncreaseForReceiverWallet_WhenIsDepositFalse()
    {
        var transactionId = Guid.NewGuid();
        var transaction = new TransactionViewModel()
        {
            WalletId = Guid.NewGuid(),
            ReceiverWalletId = Guid.NewGuid(),
            Amount = 10000
        };

        await _transactionApplication.SuccessAsync(transactionId);

        var isDeposit = false;
        isDeposit.Should().BeFalse();
        await _walletRepository.Received().DecreaseCreditAsync(transaction.WalletId, transaction.Amount);
        await _walletRepository.Received().IncreaseCreditAsync(transaction.ReceiverWalletId, transaction.Amount);
    }

    [Fact]
    public async Task SuccessAsync_ShouldCallSuccessAsyncOnTransactionRepository()
    {
        var transactionId = Guid.NewGuid();

        await _transactionApplication.SuccessAsync(transactionId);

        await _transactionRepository.Received().SuccessAsync(transactionId);
    }
    [Fact]
    public async Task FailAsync_ShouldCallFailAsyncOnTransactionRepository()
    {
        var transactionId = Guid.NewGuid();

        await _transactionApplication.FailAsync(transactionId);

        await _transactionRepository.Received().FailAsync(transactionId);
    }
    [Fact]
    public async Task GetAmountByIdAsync_ShouldCallGetAmountByIdAsyncOnTransactionRepository()
    {
        var transactionId = Guid.NewGuid();

        await _transactionApplication.GetAmountByIdAsync(transactionId);

        await _transactionRepository.Received().GetAmountByIdAsync(transactionId);
    }
    private static bool IsDeposit(AddTransaction addTransaction)
    {
        return addTransaction.WalletId == addTransaction.ReceiverWalletId;
    }
}