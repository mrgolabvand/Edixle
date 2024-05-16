using _0_Framework.Application;
using AccountManagement.Application.Contracts.JobOffer;
using AccountManagement.Application.Contracts.Project;
using AccountManagement.Domain.JobOfferAgg;
using AccountManagement.Domain.ProjectAgg;
using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.EmployerPage;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class EmployerPageQuery : IEmployerPageQuery
    {
        private readonly AccountContext _accountContext;

        public EmployerPageQuery(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }


        public async ValueTask<EmployerPageQueryModel> GetPageAsync(long pageId) =>
            await _accountContext.EmployerPages.Select(v => new EmployerPageQueryModel
                {
                    Id = v.Id,
                    AccountId = v.AccountId,
                    FullName = v.FullName,
                    Picture = v.Picture,
                    JobOffers = MapJobOffers(v.JobOffers),
                    Projects = MapProjects(v.Projects)
                })
                .FirstOrDefaultAsync(v => v.Id == pageId);

        public async ValueTask<bool> IsEmployerAsync(long accountId) => 
            await _accountContext.EmployerPages.AnyAsync(v => v.AccountId == accountId);

        private static List<ProjectViewModel> MapProjects(List<Project> projects)
        {
            return projects.Select(v => new ProjectViewModel
            {
                Description = v.Description,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                Budget = v.Budget,
                EmployerPageId = v.EmployerPageId,
                ExpireDate = v.ExpireDate.ToFarsi().ToPersianNumber(),
                Id = v.Id,
                IsConfirm = v.IsConfirm,
                IsOpened = v.IsOpened,
                Tags = v.Tags,
                Title = v.Title
            }).ToList();
        }

        private static List<JobOfferViewModel> MapJobOffers(List<JobOffer> jobOffers)
        {
            return jobOffers.Select(v => new JobOfferViewModel
            {
                Description = v.Description,
                EditorPageId = v.EditorPageId,
                EmployerPageId = v.EmployerPageId,
                Id = v.Id,
                IsAccept = v.IsAccept,
                IsCancel = v.IsCancel,
                Price = v.Price.ToMoney(),
                Title = v.Title,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber()
            }).ToList();
        }
    }
}
