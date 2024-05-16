using Microsoft.EntityFrameworkCore;
using ReviewManagement.Domain.ReviewAgg;
using ReviewManagement.Infrastructure.EFCore.Mapping;

namespace ReviewManagement.Infrastructure.EFCore
{
    public class ReviewContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public ReviewContext(DbContextOptions<ReviewContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ReviewMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
