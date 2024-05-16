using AccountManagement.Domain.TextSliderAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class TextSliderMapping : IEntityTypeConfiguration<TextSlider>
    {
        public void Configure(EntityTypeBuilder<TextSlider> builder)
        {
            builder.ToTable("TextSliders");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Title).HasMaxLength(100).IsRequired();
            builder.Property(v => v.Link).HasMaxLength(500).IsRequired();
            builder.Property(v => v.Description).HasMaxLength(300).IsRequired();
        }
    }
}
