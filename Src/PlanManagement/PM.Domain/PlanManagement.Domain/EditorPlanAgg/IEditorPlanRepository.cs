using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Domain;
using PlanManagement.Application.Contracts.EditorPlan;

namespace PlanManagement.Domain.EditorPlanAgg
{
    public interface IEditorPlanRepository : IBaseRepository<long, EditorPlan>
    {
        ValueTask<List<EditorPlanViewModel>> GetListAsync();
        ValueTask<EditEditorPlan> GetDetailsAsync(long id);
        ValueTask<EditorPlanViewModel> GetPlanAsync(long id);
    }

 
}
