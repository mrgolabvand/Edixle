using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletManagement.Domain.WalletAgg;

namespace WalletManagement.Infrastructure.EFCore.Mappings;

public class WalletMapping : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(v => v.Transactions)
            .WithOne(v => v.Wallet)
            .HasForeignKey(v => v.WalletId);
    }
}