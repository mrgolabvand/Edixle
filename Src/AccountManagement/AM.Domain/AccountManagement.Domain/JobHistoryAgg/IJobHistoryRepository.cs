using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.JobHistory;
using AccountManagement.Application.Contracts.Skill;

namespace AccountManagement.Domain.JobHistoryAgg
{
    public interface IJobHistoryRepository : IBaseRepository<long, JobHistory>
    {
        ValueTask<EditJobHistory> GetDetailsAsync(long id);
        ValueTask<List<JobHistoryViewModel>> SearchAsync(JobHistorySearchModel searchModel);
        ValueTask<List<JobHistoryViewModel>> GetListByAsync(long pageId);
        ValueTask<List<long>> GetJobsIdByAsync(long pageId);
    }
}
