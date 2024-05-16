using AccountManagement.Domain.FeatureAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class FeatureMapping : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable("Features");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Picture).HasMaxLength(400).IsRequired();
            builder.Property(v => v.PictureAlt).HasMaxLength(100);
            builder.Property(v => v.PictureTitle).HasMaxLength(100);
            builder.Property(v => v.Title).HasMaxLength(300).IsRequired();
            builder.Property(v => v.Description).HasMaxLength(700).IsRequired();

        }
    }
}
