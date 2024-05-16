using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanManagement.Domain.EditorPlanAgg;

namespace PlanManagement.Infrastructure.EFCore.Mapping
{
    public class EditorPlanMapping : IEntityTypeConfiguration<EditorPlan>
    {
        public void Configure(EntityTypeBuilder<EditorPlan> builder)
        {
            builder.ToTable("EditorPlans");

            builder.HasKey(x => x.Id);

            builder.Property(v => v.Title).HasMaxLength(250).IsRequired();
            builder.Property(v => v.Description).HasMaxLength(550);
        }
    }
}
