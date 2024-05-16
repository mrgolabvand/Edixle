using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.JobHistory
{
    public interface IJobHistoryApplication
    {
        ValueTask<OperationResult> AddAsync(AddJobHistory command);
        ValueTask<OperationResult> AddAsync(List<AddJobHistory> commands);
        ValueTask<OperationResult> EditAsync(EditJobHistory command);
        ValueTask<OperationResult> RemoveAsync(long id);
        ValueTask<OperationResult> RestoreAsync(long id);
        ValueTask<EditJobHistory> GetDetailsAsync(long id);
        ValueTask<List<long>> GetJobsIdByAsync(long pageId);
        ValueTask<List<JobHistoryViewModel>> SearchAsync(JobHistorySearchModel searchModel);
        ValueTask<List<JobHistoryViewModel>> GetListByAsync(long pageId);
    }
}
