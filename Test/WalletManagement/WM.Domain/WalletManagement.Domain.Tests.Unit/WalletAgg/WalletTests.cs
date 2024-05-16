using FluentAssertions;
using WalletManagement.Domain.WalletAgg;

namespace WalletManagement.Domain.Tests.Unit.WalletAgg
{
    public class WalletTests
    {
        private readonly WalletTestBuilder _builder;

        public WalletTests()
        {
            _builder = new WalletTestBuilder();
        }

        [Fact]
        public void Constructor_ShouldConstructWalletProperly()
        {
            // Arrange
            const long accountId = 10;

            //Act
            var wallet = _builder.WithAccountId(accountId).Build();

            //Assert
            wallet.AccountId.Should().Be(accountId);
            wallet.CurrentCredit.Should().Be(0);
            wallet.IsAskedForSettlement.Should().BeFalse();
            //wallet.IsSuccess.Should().BeFalse();
            wallet.Transactions.Should().BeEmpty();
        }

        [Fact]
        public void Increase_ShouldIncreaseProperly()
        {
            // Arrange
            const decimal amount = 20000;
            var wallet = _builder.Build();

            // Act
            wallet.Increase(amount);

            // Assert
            wallet.CurrentCredit.Should().Be(amount);
        }

        [Theory]
        [InlineData(-10000)]
        [InlineData(0)]
        public void Increase_ShouldSetAmountToZero_WhenInvalidAmountPassed(decimal amount)
        {
            // Arrange
            var wallet = _builder.Build();

            // Act
            wallet.Increase(amount);

            // Assert
            wallet.CurrentCredit.Should().Be(0);
        }

        [Theory]
        [InlineData(-10000)]
        [InlineData(0)]
        public void Decrease_ShouldSetAmountToZero_WhenInvalidAmountPassed(decimal amount)
        {
            // Arrange
            var wallet = _builder.Build();

            // Act
            wallet.Decrease(amount);

            // Assert
            wallet.CurrentCredit.Should().Be(0);
        }

        [Fact]
        public void Decrease_ShouldDecreaseProperly()
        {
            // Arrange
            const decimal increaseAmount = 20000;
            const decimal decreaseAmount = 10000;
            const decimal result = increaseAmount - decreaseAmount;

            var wallet = _builder.Build();
            wallet.Increase(increaseAmount);

            // Act
            wallet.Decrease(decreaseAmount);

            // Assert
            wallet.CurrentCredit.Should().Be(result);
        }

        [Fact]
        public void AskForSettlement_ShouldSetIsAskedForSettlementToTrue()
        {
            var wallet = _builder.Build();

            wallet.AskForSettlement();

            wallet.IsAskedForSettlement.Should().BeTrue();
        }

        [Fact]
        public void Settlement_ShouldSetIsAskedForSettlementToFalseAndSetCurrentCreditToZero()
        {
            var wallet = _builder.Build();
            wallet.Increase(10000M);
            wallet.AskForSettlement();

            wallet.Settlement();

            wallet.CurrentCredit.Should().Be(0);
            wallet.IsAskedForSettlement.Should().BeFalse();
        }

  
    }
}