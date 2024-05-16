using AccountManagement.Domain.ProjectAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class ProjectMapping : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Title).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Description).HasMaxLength(2000);
            builder.Property(v => v.Budget).HasMaxLength(100);
            builder.Property(v => v.Tags).HasMaxLength(200);

            builder.HasOne(v => v.EmployerPage).WithMany(v => v.Projects).HasForeignKey(v => v.EmployerPageId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(v => v.ProjectOffers).WithOne(v => v.Project).HasForeignKey(v => v.ProjectId);
        }
    }
}
