using _0_Framework.Domain;
using WalletManagement.Domain.TransactionAgg;

namespace WalletManagement.Domain.WalletAgg
{
    public class Wallet : BaseEntity
    {
        public Guid Id { get; private set; }
        public long AccountId { get; private set; }
        public bool IsAskedForSettlement { get; private set; }
        public decimal CurrentCredit { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public Wallet(long accountId)
        {
            AccountId = accountId;
            IsAskedForSettlement = false;
            CurrentCredit = 0;
            Transactions = new List<Transaction>();
        }

        public void Increase(decimal amount)
        {
            amount = GuardAgainstInvalidAmount(amount);
            CurrentCredit += amount;
        }

        public void Decrease(decimal amount)
        {
            amount = GuardAgainstInvalidAmount(amount);
            CurrentCredit -= amount;
        }
        private static decimal GuardAgainstInvalidAmount(decimal amount)
        {
            if (amount is <= 0 or >= 10_000_000)
                amount = 0;
            return amount;
        }

        public void AskForSettlement() => IsAskedForSettlement = true;

        public void Settlement()
        {
            IsAskedForSettlement = false;
        }
    }
}
