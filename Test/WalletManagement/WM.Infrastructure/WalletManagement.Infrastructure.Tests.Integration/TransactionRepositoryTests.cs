using FluentAssertions;
using WalletManagement.Domain.Tests.Unit.TransactionAgg;
using WalletManagement.Domain.TransactionAgg;
using WalletManagement.Infrastructure.EFCore.Repositories;

namespace WalletManagement.Infrastructure.Tests.Integration;

public class TransactionRepositoryTests : IClassFixture<RealDatabaseFixture>
{
    private readonly TransactionTestBuilder _transactionTestBuilder;
    private readonly TransactionRepository _transactionRepository;
    public TransactionRepositoryTests(RealDatabaseFixture databaseFixture)
    {
        _transactionTestBuilder = new TransactionTestBuilder();
        _transactionRepository = new TransactionRepository(databaseFixture.Context);
    }
    [Fact]
    public async Task AddAsync_ShouldReturnSuccessOperationResultAndTransactionId_WhenTransactionAdded()
    {
        var transaction = _transactionTestBuilder.Build();

        var transactionId = await _transactionRepository.CreateAndReturnIdAsync(transaction);

        transactionId.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task SuccessAsync_ShouldSetIsSuccessToTrue()
    {
        var transaction = _transactionTestBuilder.Build();
        await _transactionRepository.CreateAsync(transaction);

        await _transactionRepository.SuccessAsync(transaction.Id);

        transaction.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task FailAsync_ShouldSetIsSuccessToFalse()
    {
        var transaction = _transactionTestBuilder.Build();
        await _transactionRepository.CreateAsync(transaction);
        await _transactionRepository.SuccessAsync(transaction.Id);

        await _transactionRepository.FailAsync(transaction.Id);

        transaction.IsSuccess.Should().BeFalse();
    }
}