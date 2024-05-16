using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.JobHistory;
using AccountManagement.Application.Contracts.Skill;
using AccountManagement.Domain.JobHistoryAgg;
using AccountManagement.Domain.SkillAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class JobHistoryRepository : BaseRepository<long, JobHistory>, IJobHistoryRepository
    {
        private readonly AccountContext _accountContext;

        public JobHistoryRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<EditJobHistory> GetDetailsAsync(long id) =>
                await _accountContext.JobHistories.Select(v => new EditJobHistory
                {
                    Id = v.Id,
                    PageId = v.PageId,
                    EmployerName = v.EmployerName,
                    Description = v.Description,
                    JobTitle = v.JobTitle
                }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<JobHistoryViewModel>> SearchAsync(JobHistorySearchModel searchModel)
        {
            var query = _accountContext.JobHistories.Select(v => new JobHistoryViewModel
            {
                Id = v.Id,
                EmployerName = v.EmployerName,
                Description = v.Description,
                JobTitle = v.JobTitle,
                PageId = v.PageId
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(v => v.EmployerName.Contains(searchModel.Name));

            var result = await query.ToListAsync();

            foreach (var skillViewModel in result)
            {
                var page = await _accountContext.PersonalPages
                    .FirstOrDefaultAsync(v => v.Id == skillViewModel.PageId);

                skillViewModel.FullName = page?.FullName;
                skillViewModel.UserName = page?.Account.UserName;
            }

            return result;
        }

        public async ValueTask<List<JobHistoryViewModel>> GetListByAsync(long pageId) =>
            await _accountContext.JobHistories
                .Where(v => v.PageId == pageId)
                .Select(v => new JobHistoryViewModel
                {
                    Id = v.Id,
                    PageId = pageId,
                    JobTitle = v.JobTitle,
                    EmployerName = v.EmployerName,
                    Description = v.Description.Substring(0, Math.Min(v.Description.Length, 20)) + " ...",
                }).AsNoTracking().ToListAsync();

        public async ValueTask<List<long>> GetJobsIdByAsync(long pageId) =>
            await _accountContext.JobHistories
                .Where(v => v.PageId == pageId)
                .Select(v => v.Id)
                .ToListAsync();
    }
}
