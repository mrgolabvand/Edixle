using _0_Framework.Application;
using AccountManagement.Domain.ProjectOfferAgg;
using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.Project;
using EdixleQuery.Contracts.ProjectOffer;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query;

public class ProjectQuery : IProjectQuery
{
    private readonly AccountContext _accountContext;

    public ProjectQuery(AccountContext accountContext)
    {
        _accountContext = accountContext;
    }

    public async ValueTask<List<ProjectQueryModel>> GetLatestProjectsAsync() =>
        await _accountContext.Projects.Include(v => v.EmployerPage)
            .Where(v => v.IsConfirm)
            .Select(v => new ProjectQueryModel
            {
                EmployerName = v.EmployerPage.FullName,
                Budget = ((double)int.Parse(v.Budget)).ToMoney(),
                Description = v.Description,
                EmployerPageId = v.EmployerPageId,
                ExpireDate = v.ExpireDate.ToFarsi().ToPersianNumber(),
                Id = v.Id,
                IsOpened = v.IsOpened,
                IsConfirm = v.IsConfirm,
                Tags = v.Tags,
                Title = v.Title,
                EmployerPicture = v.EmployerPage.Picture
            }).AsNoTracking().OrderByDescending(v => v.Id).Take(6).ToListAsync();


    public async ValueTask<List<ProjectQueryModel>> SearchProjectsAsync(string query)
    {
        var projectsQuery = _accountContext.Projects
            .Include(v => v.EmployerPage)
            .Where(v => v.IsConfirm)
            .Select(v => new ProjectQueryModel
            {
                EmployerName = v.EmployerPage.FullName,
                Budget = ((double)int.Parse(v.Budget)).ToMoney(),
                Description = v.Description,
                EmployerPageId = v.EmployerPageId,
                ExpireDate = v.ExpireDate.ToFarsi().ToPersianNumber(),
                Id = v.Id,
                IsOpened = v.IsOpened,
                IsConfirm = v.IsConfirm,
                Tags = v.Tags,
                Title = v.Title,
                EmployerPicture = v.EmployerPage.Picture
            });

        if (!string.IsNullOrWhiteSpace(query))
        {
            projectsQuery = projectsQuery.Where(v => v.Title.Contains(query) || v.Description.Contains(query));
        }

        return await projectsQuery.AsNoTracking().OrderByDescending(v => v.Id).ToListAsync();
    }

    public async ValueTask<List<ProjectQueryModel>> SearchProjectsAsync(ProjectQuerySearchModel query)
    {
        var projectsQuery = _accountContext.Projects
            .Include(v => v.EmployerPage)
            .Include(v => v.ProjectOffers)
            .Where(v => v.IsConfirm)
            .Where(v => !query.IsOpen || v.IsOpened)
            .Select(v => new ProjectQueryModel
            {
                EmployerName = v.EmployerPage.FullName,
                Budget = ((double)int.Parse(v.Budget)).ToMoney(),
                IntBudget = int.Parse(v.Budget),
                Description = v.Description,
                EmployerPageId = v.EmployerPageId,
                ExpireDate = v.ExpireDate.ToFarsi().ToPersianNumber(),
                Id = v.Id,
                IsOpened = v.IsOpened,
                IsConfirm = v.IsConfirm,
                Tags = v.Tags,
                Title = v.Title,
                EmployerPicture = v.EmployerPage.Picture,
                ProjectOffers = MapProjectOffers(v.ProjectOffers)
            });

        if (!string.IsNullOrWhiteSpace(query.SearchWord))
        {
            projectsQuery = projectsQuery.Where(v =>
                v.Title.Contains(query.SearchWord) || v.Description.Contains(query.SearchWord));
        }

        var projects = await projectsQuery.ToListAsync();
        var result =
            projects.Where(v => v.IntBudget <= query.MaxPay && v.IntBudget >= query.MinPay).ToList();

        //result = result.Where(v => v.IsOpened == query.IsOpen).ToList();

        return query.OrderId switch
        {
            1 => result.OrderByDescending(v => v.Id).ToList(),
            2 => result.OrderByDescending(v => v.IntBudget).ToList(),
            _ => result
        };
    }

    private static List<ProjectOfferQueryModel> MapProjectOffers(List<ProjectOffer> projectOffers)
    {
        return projectOffers.Select(v => new ProjectOfferQueryModel
        {
            Id = v.Id
        }).ToList();
    }

    public async ValueTask<ProjectQueryModel> GetProjectAsync(long id) =>
        await _accountContext.Projects
            .Include(v => v.EmployerPage)
            .Where(v => v.IsConfirm)
            .Select(v => new ProjectQueryModel
            {
                EmployerName = v.EmployerPage.FullName,
                Budget = ((double)int.Parse(v.Budget)).ToMoney(),
                Description = v.Description,
                EmployerPageId = v.EmployerPageId,
                ExpireDate = v.ExpireDate.ToFarsi().ToPersianNumber(),
                Id = v.Id,
                IsOpened = v.IsOpened,
                IsConfirm = v.IsConfirm,
                Tags = v.Tags,
                Title = v.Title,
                EmployerPicture = v.EmployerPage.Picture,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber()
            }).AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

    public async ValueTask<ProjectsCountViewModel> GetProjectsCountAsync()
    {

       var projectsCount =  new ProjectsCountViewModel()
        {
            TotalProjectsCount = await _accountContext.Projects.LongCountAsync(),
            TotalOpenProjectsCount = await _accountContext.Projects.LongCountAsync(v => v.IsOpened),
        };

       var divide = projectsCount.TotalOpenProjectsCount / projectsCount.TotalProjectsCount;
       projectsCount.TotalOpenProjectsPercent = Math.Round(divide * 100);

       return projectsCount;
    }

}