using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace WalletManagement.Domain.Tests.Unit.TransactionAgg;

public class TransactionTests
{
    private readonly TransactionTestBuilder _builder;

    public TransactionTests()
    {
        _builder = new TransactionTestBuilder();
    }

    [Fact]
    public void Constructor_ShouldConstructProperly()
    {
        // Arrange
        var walletId = Guid.NewGuid();
        var receiverWalletId = Guid.NewGuid();
        var amount = 10000M;
        var description = "lorem";

        // Act
        var transaction = _builder
            .WithWalletId(walletId)
            .WithReceiverWalletId(receiverWalletId)
            .WithAmount(amount)
            .WithDescription(description)
            .Build();

        // Assert
        transaction.WalletId.Should().Be(walletId);
        transaction.ReceiverWalletId.Should().Be(receiverWalletId);
        transaction.Amount.Should().Be(amount);
        transaction.Description.Should().Be(description);
        transaction.IsDeposit.Should().BeFalse();
    }

    [Fact]
    public void Constructor_ShouldSetIsDepositTrue_WhenWalletIdAndReceiverWalletIdAreSame()
    {
        // Arrange
        var walletId = Guid.NewGuid();

        // Act
        var transaction = _builder
            .WithWalletId(walletId)
            .WithReceiverWalletId(walletId)
            .Build();

        // Assert
        transaction.IsDeposit.Should().BeTrue();
    }

    [Fact]
    public void Constructor_ShouldSetIsDepositFalse_WhenWalletIdAndReceiverWalletIdAreNotSame()
    {
        // Arrange
        var walletId = Guid.NewGuid();
        var receiverWalletId = Guid.NewGuid();

        // Act
        var transaction = _builder
            .WithWalletId(walletId)
            .WithReceiverWalletId(receiverWalletId)
            .Build();

        // Assert
        transaction.IsDeposit.Should().BeFalse();
    }

    [Theory]
    [InlineData(-20000)]
    [InlineData(2000_000_000)]
    public void Constructor_ShouldSetAmountToZero_WhenPassedInvalidAmount(decimal amount)
    {
        // Arrange

        // Act
        var transaction = _builder.WithAmount(amount).Build();

        // Assert
        transaction.Amount.Should().Be(0);
    }

    [Fact]
    public void Success_ShouldSetIsSuccessToTrue()
    {
        var transaction = _builder.Build();

        transaction.Success();

        transaction.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Failed_ShouldSetIsSuccessToFalse()
    {
        var transaction = _builder.Build();

        transaction.Failed();

        transaction.IsSuccess.Should().BeFalse();
    }

}