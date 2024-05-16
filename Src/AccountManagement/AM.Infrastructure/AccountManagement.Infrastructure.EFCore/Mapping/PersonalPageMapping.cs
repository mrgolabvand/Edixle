using AccountManagement.Domain.PersonalPageAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class PersonalPageMapping : IEntityTypeConfiguration<PersonalPage>
    {
        public void Configure(EntityTypeBuilder<PersonalPage> builder)
        {
            builder.ToTable("PersonalPages");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Age).HasMaxLength(3);
            builder.Property(v => v.About).HasMaxLength(2000);
            builder.Property(v => v.MinPay).HasMaxLength(100);
            builder.Property(v => v.MaxPay).HasMaxLength(100);
            builder.Property(v => v.PayDate).HasMaxLength(100);
            builder.Property(v => v.FullName).HasMaxLength(200).IsRequired();
            builder.Property(v => v.PictureTitle).HasMaxLength(200);
            builder.Property(v => v.PictureAlt).HasMaxLength(200);
            builder.Property(v => v.Picture).HasMaxLength(300);
            builder.Property(v => v.Card).HasMaxLength(100);
            builder.Property(v => v.Slug).HasMaxLength(400).IsRequired();

            builder.HasMany(v => v.Portfolios).WithOne(v => v.Page).HasForeignKey(v => v.PageId);
        }
    }
}
