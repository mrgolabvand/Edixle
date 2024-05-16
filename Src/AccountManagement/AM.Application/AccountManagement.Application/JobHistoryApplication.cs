#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.JobHistory;
using AccountManagement.Domain.JobHistoryAgg;

namespace AccountManagement.Application
{
    public class JobHistoryApplication : IJobHistoryApplication
    {
        private readonly IJobHistoryRepository _jobHistoryRepository;

        public JobHistoryApplication(IJobHistoryRepository jobHistoryRepository)
        {
            _jobHistoryRepository = jobHistoryRepository;
        }

        public async ValueTask<OperationResult> AddAsync(AddJobHistory command)
        {
            var operation = new OperationResult();

            var jobHistory = new JobHistory(command.JobTitle, command.EmployerName, command.Description, command.PageId);
            await _jobHistoryRepository.CreateAsync(jobHistory);
            await _jobHistoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> AddAsync(List<AddJobHistory> commands)
        {
            var operation = new OperationResult();

            foreach (var command in commands)
            {
                var jobHistory = new JobHistory(command.JobTitle, command.EmployerName, command.Description, command.PageId);
                await _jobHistoryRepository.CreateAsync(jobHistory);
                await _jobHistoryRepository.SaveChangesAsync();
            }
            
            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditJobHistory command)
        {
            var operation = new OperationResult();

            var jobHistory = await _jobHistoryRepository.GetAsync(command.Id);

            if (jobHistory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            jobHistory.Edit(command.JobTitle, command.EmployerName, command.Description);
            await _jobHistoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> RemoveAsync(long id)
        {
            var operation = new OperationResult();

            var jobHistory = await _jobHistoryRepository.GetAsync(id);

            if (jobHistory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            jobHistory.Remove();
            await _jobHistoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> RestoreAsync(long id)
        {
            var operation = new OperationResult();

            var jobHistory = await _jobHistoryRepository.GetAsync(id);

            if (jobHistory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            jobHistory.Restore();
            await _jobHistoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditJobHistory> GetDetailsAsync(long id) => await _jobHistoryRepository.GetDetailsAsync(id);

        public async ValueTask<List<long>> GetJobsIdByAsync(long pageId) => await _jobHistoryRepository.GetJobsIdByAsync(pageId);

        public async ValueTask<List<JobHistoryViewModel>> SearchAsync(JobHistorySearchModel searchModel) =>
            await _jobHistoryRepository.SearchAsync(searchModel);

        public async ValueTask<List<JobHistoryViewModel>> GetListByAsync(long pageId) =>
            await _jobHistoryRepository.GetListByAsync(pageId);
    }
}