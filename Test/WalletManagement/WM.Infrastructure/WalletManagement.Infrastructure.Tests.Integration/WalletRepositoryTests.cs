using FluentAssertions;
using WalletManagement.Domain.Tests.Unit.WalletAgg;
using WalletManagement.Infrastructure.EFCore.Repositories;

namespace WalletManagement.Infrastructure.Tests.Integration
{
    public class WalletRepositoryTests : IClassFixture<RealDatabaseFixture>
    {
        private readonly WalletRepository _walletRepository;
        private readonly WalletTestBuilder _walletTestBuilder;

        public WalletRepositoryTests(RealDatabaseFixture databaseFixture)
        {
            _walletTestBuilder = new WalletTestBuilder();
            _walletRepository = new WalletRepository(databaseFixture.Context);
        }

        [Fact]
        public async Task IncreaseCreditAsync_ShouldIncreaseWalletCredit()
        {
            var amount = 10000M;
            var wallet = _walletTestBuilder.Build();
            await _walletRepository.CreateAsync(wallet);

            await _walletRepository.IncreaseCreditAsync(wallet.Id, amount);

            wallet.CurrentCredit.Should().Be(amount);
        }
        [Fact]
        public async Task DecreaseCreditAsync_ShouldDecreaseWalletCredit()
        {
            var increaseAmount = 10000M;
            var decreaseAmount = 5000M;
            var wallet = _walletTestBuilder.Build();
            await _walletRepository.CreateAsync(wallet);
            await _walletRepository.IncreaseCreditAsync(wallet.Id, increaseAmount);

            await _walletRepository.DecreaseCreditAsync(wallet.Id, decreaseAmount);

            var result = increaseAmount - decreaseAmount;
            wallet.CurrentCredit.Should().Be(result);
        }

        [Fact]
        public async Task AskForSettlementAsync_ShouldSetIsAskedForSettlementToTrue()
        {
            var wallet = _walletTestBuilder.Build();
            await _walletRepository.CreateAsync(wallet);

            await _walletRepository.AskForSettlementAsync(wallet.Id);

            wallet.IsAskedForSettlement.Should().BeTrue();
        }

        [Fact]
        public async Task SettlementAsync_ShouldSetIsAskedForSettlementToFalseAndSetCurrentCreditToZero()
        {
            var wallet = _walletTestBuilder.Build();
            await _walletRepository.CreateAsync(wallet);
            await _walletRepository.IncreaseCreditAsync(wallet.Id, 10000M);
            await _walletRepository.AskForSettlementAsync(wallet.Id);

            await _walletRepository.SettlementAsync(wallet.Id);

            wallet.CurrentCredit.Should().Be(0);
            wallet.IsAskedForSettlement.Should().BeFalse();
        }

    }
}