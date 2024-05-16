using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletManagement.Domain.TransactionAgg;

namespace WalletManagement.Infrastructure.EFCore.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(v => v.Description).HasMaxLength(200);

        builder.HasOne(v => v.Wallet)
            .WithMany(v => v.Transactions)
            .HasForeignKey(v => v.WalletId);
    }
}