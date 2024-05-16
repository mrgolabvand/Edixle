using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mapping
{
    public class ArticleCategoryMapping : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories", "Blog");
            
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name).HasMaxLength(500);
            builder.Property(v => v.MetaDescription).HasMaxLength(150);
            builder.Property(v => v.Keyeords).HasMaxLength(100);
            builder.Property(v => v.CanonicalAddress).HasMaxLength(1000);
            builder.Property(v => v.Description).HasMaxLength(100000);
            builder.Property(v => v.Slug).HasMaxLength(600);
            builder.Property(v => v.PictureTitle).HasMaxLength(500);
            builder.Property(v => v.PictureAlt).HasMaxLength(500);

            builder.HasMany(v => v.Articles).WithOne(v => v.Category).HasForeignKey(v => v.CategoryId);
        }
    }
}
