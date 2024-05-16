using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using WalletManagement.Application.Contracts.Transaction;
using WalletManagement.Application.Contracts.Wallet;
using WalletManagement.Domain.TransactionAgg;
using WalletManagement.Domain.WalletAgg;

namespace WalletManagement.Infrastructure.EFCore.Repositories;

public class WalletRepository : BaseRepository<Guid, Wallet>, IWalletRepository
{
    private readonly WalletContext _context;
    public WalletRepository(WalletContext context) : base(context)
    {
        _context = context;
    }

    public async ValueTask IncreaseCreditAsync(Guid walletId, decimal amount)
    {
        var wallet = await GetAsync(walletId);
        wallet.Increase(amount);
    }

    public async ValueTask DecreaseCreditAsync(Guid walletId, decimal amount)
    {
        var wallet = await GetAsync(walletId);
        wallet.Decrease(amount);
    }

    public async ValueTask AskForSettlementAsync(Guid walletId)
    {
        var wallet = await GetAsync(walletId);
        wallet.AskForSettlement();
    }

    public async ValueTask SettlementAsync(Guid walletId)
    {
        var wallet = await GetAsync(walletId);
        wallet.Settlement();
    }

    public async ValueTask<Guid> GetWalletIdByAccountIdAsync(long accountId)
    {
        var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.AccountId == accountId);
        return wallet?.Id ?? Guid.Empty;
    }

    public async ValueTask<long> GetAccountIdByWalletIdAsync(Guid walletId)
    {
        var wallet = await _context.Wallets.FirstOrDefaultAsync(v => v.Id == walletId);
        return wallet is null ? 0 : wallet.AccountId;
    }

    public async ValueTask<WalletViewModel> GetWalletByAccountIdAsync(long accountId)
    {
        var wallet = await _context.Wallets
            .Include(v => v.Transactions)
            .Select(v => new WalletViewModel
            {
                Id = v.Id,
                AccountId = v.AccountId,
                CurrentCredit = v.CurrentCredit,
                IsAskedForSettlement = v.IsAskedForSettlement,
                Transactions = MapTransactions(v.Transactions)
            }).AsNoTracking()
            .FirstOrDefaultAsync(v => v.AccountId == accountId);

        var transactions = await _context.Transactions.ToListAsync();

        foreach (var transaction in transactions)
        {
            if (transaction.ReceiverWalletId == wallet.Id && transaction.ReceiverWalletId != transaction.WalletId)
            {
                var transactionModel = new TransactionViewModel()
                {
                    Id = transaction.Id,
                    Amount = transaction.Amount,
                    ReceiverWalletId = transaction.ReceiverWalletId,
                    CreationDate = transaction.CreationDate.ToFarsi(),
                    CreationDateTime = transaction.CreationDate,
                    Description = transaction.Description,
                    IsDeposit = true,
                    IsSuccess = transaction.IsSuccess,
                    WalletId = transaction.WalletId,
                };

                wallet.Transactions.Add(transactionModel);
            }
        }
        return wallet;
    }


    public async ValueTask<bool> HasCredit(Guid walletId, decimal value) =>
        await _context.Wallets.AnyAsync(v => v.Id == walletId && v.CurrentCredit >= value);

    public async ValueTask<List<WalletViewModel>> GetAllAsync() =>
        await _context.Wallets.Select(v => new WalletViewModel()
        {
            Id = v.Id,
            AccountId = v.AccountId,
            CurrentCredit = v.CurrentCredit,
            IsAskedForSettlement = v.IsAskedForSettlement
        }).AsNoTracking().ToListAsync();

    public async ValueTask<WalletViewModel> GetWalletByIdAsync(Guid id)
    {
        var wallet = await _context.Wallets
            .Include(v => v.Transactions)
            .Select(v => new WalletViewModel
            {
                Id = v.Id,
                AccountId = v.AccountId,
                CurrentCredit = v.CurrentCredit,
                IsAskedForSettlement = v.IsAskedForSettlement,
                Transactions = MapTransactions(v.Transactions)
            }).AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == id);

        var transactions = await _context.Transactions.ToListAsync();

        foreach (var transaction in transactions)
        {
            if (transaction.ReceiverWalletId == wallet.Id && transaction.ReceiverWalletId != transaction.WalletId)
            {
                var transactionModel = new TransactionViewModel()
                {
                    Id = transaction.Id,
                    Amount = transaction.Amount,
                    ReceiverWalletId = transaction.ReceiverWalletId,
                    CreationDate = transaction.CreationDate.ToFarsi(),
                    CreationDateTime = transaction.CreationDate,
                    Description = transaction.Description,
                    IsDeposit = true,
                    IsSuccess = transaction.IsSuccess,
                    WalletId = transaction.WalletId,
                };

                wallet.Transactions.Add(transactionModel);
            }
        }
        return wallet;
    }

    private static List<TransactionViewModel> MapTransactions(List<Transaction> transactions) =>
        transactions.Count == 0 ?
            new List<TransactionViewModel>() :
            transactions.Select(v => new TransactionViewModel
            {
                Id = v.Id,
                Amount = v.Amount,
                IsSuccess = v.IsSuccess,
                CreationDate = v.CreationDate.ToFarsi(),
                Description = v.Description,
                IsDeposit = v.IsDeposit,
                ReceiverWalletId = v.ReceiverWalletId,
                WalletId = v.WalletId,
                CreationDateTime = v.CreationDate
            }).OrderByDescending(v => v.CreationDateTime).ToList();



}