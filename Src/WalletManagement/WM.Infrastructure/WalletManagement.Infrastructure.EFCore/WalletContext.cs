using Microsoft.EntityFrameworkCore;
using WalletManagement.Domain.TransactionAgg;
using WalletManagement.Domain.WalletAgg;
using WalletManagement.Infrastructure.EFCore.Mappings;

namespace WalletManagement.Infrastructure.EFCore;

public class WalletContext : DbContext
{
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public WalletContext(DbContextOptions<WalletContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(WalletMapping).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(modelBuilder);
    }
}