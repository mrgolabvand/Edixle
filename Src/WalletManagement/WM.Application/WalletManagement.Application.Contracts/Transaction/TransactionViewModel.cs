namespace WalletManagement.Application.Contracts.Transaction;

public class TransactionViewModel
{
    public Guid Id { get; set; }
    public Guid WalletId { get; set; }
    public Guid ReceiverWalletId { get; set; }
    public string ReceiverAccountName { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public bool IsDeposit { get; set; }
    public bool IsSuccess { get; set; }
    public string CreationDate { get; set; }
    public DateTime CreationDateTime { get; set; }
}