using WalletManagement.Application.Contracts.Transaction;

namespace WalletManagement.Application.Contracts.Wallet;

public class WalletViewModel
{
    public Guid Id { get; set; }
    public long AccountId { get; set; }
    public bool IsAskedForSettlement { get; set; }
    public decimal CurrentCredit { get; set; }
    public string AccountName { get; set; }
    public List<TransactionViewModel> Transactions { get; set; }
}