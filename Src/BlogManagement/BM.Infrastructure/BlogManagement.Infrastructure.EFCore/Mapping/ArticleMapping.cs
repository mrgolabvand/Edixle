using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EFCore.Mapping
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles", "Blog");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Title).HasMaxLength(500);
            builder.Property(v => v.MetaDescription).HasMaxLength(150);
            builder.Property(v => v.Keywords).HasMaxLength(100);
            builder.Property(v => v.CanonicalAddress).HasMaxLength(1000);
            builder.Property(v => v.ShortDescription).HasMaxLength(1000);
            builder.Property(v => v.Slug).HasMaxLength(600);
            builder.Property(v => v.PictureTitle).HasMaxLength(500);
            builder.Property(v => v.PictureAlt).HasMaxLength(500);

            builder.HasOne(v => v.Category).WithMany(v => v.Articles).HasForeignKey(v => v.CategoryId);
        }
    }
}
