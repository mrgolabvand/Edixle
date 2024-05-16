using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace PlanManagement.Application.Contracts.EditorPlan
{
    public interface IEditorPlanApplication
    {
        ValueTask<List<EditorPlanViewModel>> GetListAsync();
        ValueTask<EditEditorPlan> GetDetailsAsync(long id);
        ValueTask<OperationResult> CreateAsync(AddEditorPlan command);
        ValueTask<OperationResult> EditAsync(EditEditorPlan command);
        ValueTask<EditorPlanViewModel> GetPlanAsync(long id);
        ValueTask RemoveAsync(long id);
        ValueTask RestoreAsync(long id);
    }
}