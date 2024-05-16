using AccountManagement.Domain.PortfolioCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class PortfolioCategoryMapping : IEntityTypeConfiguration<PortfolioCategory>
    {
        public void Configure(EntityTypeBuilder<PortfolioCategory> builder)
        {
            builder.ToTable("PortfolioCategories");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Keywords).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Slug).HasMaxLength(200).IsRequired();
            builder.Property(v => v.MetaDescription).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Name).HasMaxLength(255).IsRequired();
            builder.Property(v => v.Picture).HasMaxLength(500);
            builder.Property(v => v.PictureTitle).HasMaxLength(100);

            builder.HasOne(v => v.PortfolioBaseCategory)
                .WithMany(v => v.PortfolioCategories).HasForeignKey(v => v.PortfolioBaseCategoryId);

            //builder.HasMany(v => v.Portfolios)
                //.WithOne(v => v.Category).HasForeignKey(v => v.CategoryId);
        }
    }
}
