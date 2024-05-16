using AccountManagement.Domain.PortfolioAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class PortfolioMapping : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.ToTable("Portfolios");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Description).HasMaxLength(3000);
            builder.Property(v => v.Video).HasMaxLength(400);
            builder.Property(v => v.Keywords).HasMaxLength(200);
            builder.Property(v => v.MetaDescription).HasMaxLength(200);
            builder.Property(v => v.Name).HasMaxLength(255).IsRequired();
            builder.Property(v => v.Picture).HasMaxLength(400);
            builder.Property(v => v.PictureAlt).HasMaxLength(200);
            builder.Property(v => v.PictureTitle).HasMaxLength(200);
            builder.Property(v => v.ShortDescription).HasMaxLength(2000);
            builder.Property(v => v.Slug).HasMaxLength(400).IsRequired();
            builder.Property(v => v.Tags).HasMaxLength(200);

            //builder.HasOne(v => v.Category)
                //.WithMany(v => v.Portfolios).HasForeignKey(v => v.CategoryId);

            builder.HasOne(v => v.Page)
                .WithMany(v => v.Portfolios).HasForeignKey(v => v.PageId);

            builder.HasMany(v => v.PortfolioDownloads).WithOne(v => v.Portfolio).HasForeignKey(v => v.PortfolioId);
        }
    }
}
