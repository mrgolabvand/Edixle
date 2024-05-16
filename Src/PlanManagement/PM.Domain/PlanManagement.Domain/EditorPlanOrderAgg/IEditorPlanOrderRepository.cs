using System.Threading.Tasks;
using _0_Framework.Domain;

namespace PlanManagement.Domain.EditorPlanOrderAgg
{
    public interface IEditorPlanOrderRepository : IBaseRepository<long, EditorPlanOrder>
    {
        ValueTask<double> GetAmountByAsync(long id);
    }
}
