namespace WalletManagement.Application.Contracts.Transaction;

public class AddTransaction
{
    public Guid WalletId { get; set; }
    public Guid ReceiverWalletId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}