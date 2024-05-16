using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EFCore.Mapping
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name).HasMaxLength(500);
            builder.Property(v => v.Email).HasMaxLength(500);
            builder.Property(v => v.Website).HasMaxLength(500);
            builder.Property(v => v.Message).HasMaxLength(1500);

            builder.HasMany(v => v.Children)
                .WithOne(v => v.Parent)
                .HasForeignKey(v => v.ParentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
