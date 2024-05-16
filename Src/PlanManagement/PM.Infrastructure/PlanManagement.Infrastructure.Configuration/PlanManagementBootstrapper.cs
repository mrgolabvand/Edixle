using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EdixleQuery.Contracts.Plan;
using EdixleQuery.Query;
using PlanManagement.Application;
using PlanManagement.Application.Contracts.EditorPlan;
using PlanManagement.Application.Contracts.EditorPlanOrder;
using PlanManagement.Domain.EditorPlanAgg;
using PlanManagement.Domain.EditorPlanOrderAgg;
using PlanManagement.Infrastructure.EFCore;
using PlanManagement.Infrastructure.EFCore.Repository;

namespace PlanManagement.Infrastructure.Configuration
{
    public class PlanManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IEditorPlanRepository, EditorPlanRepository>();
            services.AddTransient<IEditorPlanApplication, EditorPlanApplication>();

            services.AddTransient<IEditorPlanOrderRepository, EditorPlanOrderRepository>();
            services.AddTransient<IEditorPlanOrderApplication, EditorPlanOrderApplication>();

            services.AddTransient<IEditorPlanQuery, EditorPlanQuery>();

            services.AddDbContext<PlanContext>(v => v.UseSqlServer(connectionString));
        }
    }
}
