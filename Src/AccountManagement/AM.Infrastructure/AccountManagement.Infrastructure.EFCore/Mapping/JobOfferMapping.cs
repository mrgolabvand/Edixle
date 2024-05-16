using AccountManagement.Domain.JobOfferAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class JobOfferMapping : IEntityTypeConfiguration<JobOffer>
    {
        public void Configure(EntityTypeBuilder<JobOffer> builder)
        {
            builder.ToTable("JobOffers");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Title).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Price).HasMaxLength(100).IsRequired();
            builder.Property(v => v.Description).HasMaxLength(500);

            builder.HasOne(v => v.EmployerPage).WithMany(v => v.JobOffers).HasForeignKey(v => v.EmployerPageId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(v => v.EditorPage).WithMany(v => v.JobOffers).HasForeignKey(v => v.EditorPageId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
