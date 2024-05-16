using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReviewManagement.Application;
using ReviewManagement.Application.Contracts.Review;
using ReviewManagement.Domain.ReviewAgg;
using ReviewManagement.Infrastructure.EFCore;
using ReviewManagement.Infrastructure.EFCore.Repository;

namespace ReviewManagement.Infrastructure.Configuration
{
    public class ReviewManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IReviewApplication, ReviewApplication>();

            services.AddDbContext<ReviewContext>(v => v.UseSqlServer(connectionString));
        }
    }
}
