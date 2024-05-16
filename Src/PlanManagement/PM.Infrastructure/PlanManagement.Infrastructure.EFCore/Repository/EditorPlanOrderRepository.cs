using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PlanManagement.Domain.EditorPlanOrderAgg;

namespace PlanManagement.Infrastructure.EFCore.Repository
{
    public class EditorPlanOrderRepository : BaseRepository<long, EditorPlanOrder>, IEditorPlanOrderRepository
    {
        private readonly PlanContext _context;

        public EditorPlanOrderRepository(PlanContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<double> GetAmountByAsync(long id)
        {
           var order= await _context.EditorPlanOrders.Select(v => new { v.Id, v.PayAmount })
                .FirstOrDefaultAsync(v => v.Id == id);
           return order.PayAmount;
        }
    }
}
