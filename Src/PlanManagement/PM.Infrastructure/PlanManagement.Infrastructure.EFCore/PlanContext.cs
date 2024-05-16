using Microsoft.EntityFrameworkCore;
using PlanManagement.Domain.EditorPlanAgg;
using PlanManagement.Domain.EditorPlanOrderAgg;
using PlanManagement.Infrastructure.EFCore.Mapping;

namespace PlanManagement.Infrastructure.EFCore
{
    public class PlanContext : DbContext
    {

        public DbSet<EditorPlan> EditorPlans { get; set; }
        public DbSet<EditorPlanOrder> EditorPlanOrders { get; set; }

        public PlanContext(DbContextOptions<PlanContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(EditorPlanMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
