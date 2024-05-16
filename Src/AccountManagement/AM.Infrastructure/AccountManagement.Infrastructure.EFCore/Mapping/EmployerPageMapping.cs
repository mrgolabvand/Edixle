using AccountManagement.Domain.EmployerPageAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class EmployerPageMapping : IEntityTypeConfiguration<EmployerPage>
    {
        public void Configure(EntityTypeBuilder<EmployerPage> builder)
        {
            builder.ToTable("EmployerPages");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.FullName).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Picture).HasMaxLength(500);
            builder.Property(v => v.Card).HasMaxLength(100);

            builder.HasMany(v => v.Projects).WithOne(v => v.EmployerPage).HasForeignKey(v => v.EmployerPageId);
            builder.HasMany(v => v.JobOffers).WithOne(v => v.EmployerPage).HasForeignKey(v => v.EmployerPageId);
        }
    }
}
