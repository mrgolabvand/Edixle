using AccountManagement.Domain.PortfolioAndCategoryLinkedAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class PortfolioAndCategoryLinkedMapping : IEntityTypeConfiguration<PortfolioAndCategoryLinked>
    {
        public void Configure(EntityTypeBuilder<PortfolioAndCategoryLinked> builder)
        {
            builder.ToTable("PortfolioAndCategoryLinks");
            builder.HasKey(v => new { v.PortfolioId, v.PortfolioCategoryId });

            builder.HasOne(v => v.Portfolio)
                .WithMany(v => v.Categories).HasForeignKey(v => v.PortfolioId);
            
            builder.HasOne(v => v.PortfolioCategory)
                .WithMany(v => v.Portfolios).HasForeignKey(v => v.PortfolioCategoryId);
        }
    }
}