using AccountManagement.Domain.PortfolioBaseCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class PortfolioBaseCategoryMapping : IEntityTypeConfiguration<PortfolioBaseCategory>
    {
        public void Configure(EntityTypeBuilder<PortfolioBaseCategory> builder)
        {
            builder.ToTable("PortfolioBaseCategories");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Keywords).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Slug).HasMaxLength(200).IsRequired();
            builder.Property(v => v.MetaDescription).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Name).HasMaxLength(255).IsRequired();
            builder.Property(v => v.Picture).HasMaxLength(500);
            builder.Property(v => v.PictureTitle).HasMaxLength(100);

            builder.HasMany(v => v.PortfolioCategories)
                .WithOne(v => v.PortfolioBaseCategory).HasForeignKey(v => v.PortfolioBaseCategoryId);
        }
    }
}
