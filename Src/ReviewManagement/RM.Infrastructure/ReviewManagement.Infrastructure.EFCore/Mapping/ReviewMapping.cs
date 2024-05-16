using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewManagement.Domain.ReviewAgg;

namespace ReviewManagement.Infrastructure.EFCore.Mapping
{
    public class ReviewMapping : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name).HasMaxLength(100).IsRequired();
            builder.Property(v => v.Message).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Strength).HasMaxLength(100);
            builder.Property(v => v.Weakness).HasMaxLength(100);
            builder.Property(v => v.IsHelpful).HasMaxLength(4);
            builder.Property(v => v.IsHarmful).HasMaxLength(4);
            builder.Property(v => v.UseProperSoundEffects).HasMaxLength(2);
            builder.Property(v => v.EditVideoQuality).HasMaxLength(2);
            builder.Property(v => v.UseProperFontAndColors).HasMaxLength(2);
            builder.Property(v => v.UseProperMemes).HasMaxLength(2);
            builder.Property(v => v.UseProperVideoEffects).HasMaxLength(2);
            builder.Property(v => v.EditSoundQuality).HasMaxLength(2);
            builder.Property(v => v.Rate).HasMaxLength(2);
        }
    }
}
