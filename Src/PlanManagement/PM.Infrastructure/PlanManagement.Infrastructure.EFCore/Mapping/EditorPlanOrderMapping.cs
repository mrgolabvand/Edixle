using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanManagement.Domain.EditorPlanOrderAgg;

namespace PlanManagement.Infrastructure.EFCore.Mapping
{
    public class EditorPlanOrderMapping : IEntityTypeConfiguration<EditorPlanOrder>
    {
        public void Configure(EntityTypeBuilder<EditorPlanOrder> builder)
        {
            builder.ToTable("EditorPlanOrders");

            builder.HasKey(x => x.Id);
        }
    }
}