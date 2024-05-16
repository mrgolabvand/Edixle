using AccountManagement.Domain.JobHistoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class JobHistoryMapping : IEntityTypeConfiguration<JobHistory>
    {
        public void Configure(EntityTypeBuilder<JobHistory> builder)
        {
            builder.ToTable("JobHistories");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.JobTitle).HasMaxLength(200).IsRequired();
            builder.Property(v => v.EmployerName).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Description).HasMaxLength(1000);

            builder.HasOne(v => v.Page)
                .WithMany(v => v.JobHistories)
                .HasForeignKey(v => v.PageId);
        }
    }
}