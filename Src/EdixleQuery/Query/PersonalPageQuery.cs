using _0_Framework.Application;
using AccountManagement.Domain.JobHistoryAgg;
using AccountManagement.Domain.PortfolioAgg;
using AccountManagement.Domain.SkillAgg;
using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts;
using EdixleQuery.Contracts.JobHistory;
using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.Portfolio;
using EdixleQuery.Contracts.Review;
using EdixleQuery.Contracts.Skill;
using Microsoft.EntityFrameworkCore;
using ReviewManagement.Domain.ReviewAgg;
using ReviewManagement.Infrastructure.EFCore;

namespace EdixleQuery.Query;

public class PersonalPageQuery : IPersonalPageQuery
{
    private readonly ISharedQuery _sharedQuery;
    private readonly AccountContext _accountContext;
    private readonly ReviewContext _reviewContext;

    public PersonalPageQuery(AccountContext accountContext, ReviewContext reviewContext, ISharedQuery sharedQuery)
    {
        _accountContext = accountContext;
        _reviewContext = reviewContext;
        _sharedQuery = sharedQuery;
    }

    public async ValueTask<PersonalPageQueryModel> GetPageAsync(long pageId)
    {
        var page = await _accountContext.PersonalPages
            .Where(v => v.IsActive && v.IsConfirm)
            .Select(v => new PersonalPageQueryModel
            {
                Id = v.Id,
                AccountId = v.AccountId,
                About = v.About,
                Age = v.Age,
                FullName = v.FullName,
                IsActive = v.IsActive,
                Picture = v.Picture,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                Skills = MapSkills(v.Skills),
                Slug = v.Slug,
                Portfolios = new List<PortfolioQueryModel>(),
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                IsConfirm = v.IsConfirm,
                MaxPay = v.MaxPay.StringToMoney(),
                IsBusy = v.IsBusy,
                MinPay = v.MinPay.StringToMoney(),
                IntMaxPay = int.Parse(v.MaxPay),
                IntMinPay = int.Parse(v.MinPay),
                PayDate = v.PayDate,
            }).AsNoTracking().FirstOrDefaultAsync(v => v.Id == pageId);

        if (page == null)
            return null;

        page = await _sharedQuery.ConfigurePagePortfolios(page);

        return page;
    }




    public async ValueTask<PersonalPageQueryModel> GetPageAsync(string slug, long pageId)
    {
        var page = await _accountContext.PersonalPages
            .Where(v => v.IsActive && v.IsConfirm)
            .Select(v => new PersonalPageQueryModel
            {
                Id = v.Id,
                AccountId = v.AccountId,
                About = v.About,
                Age = v.Age,
                FullName = v.FullName,
                IsActive = v.IsActive,
                Picture = v.Picture,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                Skills = MapSkills(v.Skills),
                JobHistories = MapJobHistories(v.JobHistories),
                Slug = v.Slug,
                Portfolios = new List<PortfolioQueryModel>(),
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                IsConfirm = v.IsConfirm,
                MaxPay = v.MaxPay.StringToMoney(),
                IsBusy = v.IsBusy,
                MinPay = v.MinPay.StringToMoney(),
                IntMaxPay = int.Parse(v.MaxPay),
                IntMinPay = int.Parse(v.MinPay),
                PayDate = v.PayDate,
            }).AsNoTracking().FirstOrDefaultAsync(v => v.Slug == slug);

        page = await _sharedQuery.ConfigurePagePortfolios(page);

        return page;
    }

    private static List<JobHistoryQueryModel> MapJobHistories(List<JobHistory> jobHistories)
    {
        return jobHistories.Where(v => !v.IsRemoved).Select(v => new JobHistoryQueryModel()
        {
            Id = v.Id,
            Description = v.Description,
            IsRemoved = v.IsRemoved,
            EmployerName = v.EmployerName,
            PageId = v.PageId,
            JobTitle = v.JobTitle
        }).ToList();
    }


    public async ValueTask<bool> IsEditorAsync(long accountId) =>
        await _accountContext.PersonalPages.AnyAsync(v => v.AccountId == accountId);

    public async ValueTask<string> GetPersonalPageSlugAsync(long accountId)
    {
        var page = await _accountContext.PersonalPages
            .Select(v => new { v.AccountId, v.Slug })
            .FirstOrDefaultAsync(v => v.AccountId == accountId);

        return page?.Slug;
    }

    public async ValueTask<List<PersonalPageQueryModel>> SearchAsync(string searchWord, int orderId = 1, int minPay = 1000, int maxPay = 10000000, bool isBusy = false,
        string payDate = null)
    {
        var query = await _accountContext.PersonalPages
            .Where(v => v.IsActive && v.IsConfirm)
            .Select(v => new PersonalPageQueryModel
            {
                Id = v.Id,
                AccountId = v.AccountId,
                About = v.About,
                Age = v.Age,
                FullName = v.FullName,
                IsActive = v.IsActive,
                Picture = v.Picture,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                Skills = MapSkills(v.Skills),
                Slug = v.Slug,
                Portfolios = new List<PortfolioQueryModel>(),
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                IsConfirm = v.IsConfirm,
                MaxPay = v.MaxPay.StringToMoney(),
                IsBusy = v.IsBusy,
                MinPay = v.MinPay.StringToMoney(),
                IntMaxPay = int.Parse(v.MaxPay),
                IntMinPay = int.Parse(v.MinPay),
                PayDate = v.PayDate,
            }).AsNoTracking().ToListAsync();

        if (!string.IsNullOrWhiteSpace(searchWord))
            query = query.Where(v => v.FullName.Contains(searchWord)).ToList();

        if (isBusy)
            query = query.Where(v => v.IsBusy = false).ToList();

        query = query.Where(v => v.IntMinPay >= minPay && v.IntMaxPay <= maxPay).ToList();

        if (!string.IsNullOrWhiteSpace(payDate))
            query = query.Where(v => v.PayDate.Contains(payDate)).ToList();

        for (var index = 0; index < query.Count; index++)
            query[index] = await _sharedQuery.ConfigurePagePortfolios(query[index]);

        return orderId switch
        {
            1 => query.OrderByDescending(v => v.ReviewsTotalRate).ToList(),
            2 => query.OrderByDescending(v => v.Id).ToList(),
            _ => query
        };
    }

    public async ValueTask<List<PersonalPageQueryModel>> GetBestEditorsAsync()
    {
        var query = await _accountContext.PersonalPages
            .Where(v => v.IsActive && v.IsConfirm)
            .Select(v => new PersonalPageQueryModel
            {
                Id = v.Id,
                AccountId = v.AccountId,
                About = v.About,
                Age = v.Age,
                FullName = v.FullName,
                IsActive = v.IsActive,
                Picture = v.Picture,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                Skills = MapSkills(v.Skills),
                Slug = v.Slug,
                Portfolios = new List<PortfolioQueryModel>(),
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                IsConfirm = v.IsConfirm,
                MaxPay = v.MaxPay.StringToMoney(),
                IsBusy = v.IsBusy,
                MinPay = v.MinPay.StringToMoney(),
                IntMaxPay = int.Parse(v.MaxPay),
                IntMinPay = int.Parse(v.MinPay),
                PayDate = v.PayDate,
            }).AsNoTracking().ToListAsync();

        for (var index = 0; index < query.Count; index++)
            query[index] = await _sharedQuery.ConfigurePagePortfolios(query[index]);

        return query.OrderByDescending(v => v.ReviewsTotalRate).Take(9).ToList();
    }

    public async ValueTask<PersonalPagesCountViewModel> GetPersonalPagesCountAsync()
    {
        var personalPagesCount = new PersonalPagesCountViewModel()
        {
            TotalPages = await _accountContext.PersonalPages.LongCountAsync(),
            TotalTopPages = await _accountContext.PersonalPages
                .Where(v => v.Portfolios.Any()).LongCountAsync()
        };

        var divide = personalPagesCount.TotalTopPages / personalPagesCount.TotalPages;
        personalPagesCount.TotalTopPagesPercent = Math.Round(divide * 100);

        return personalPagesCount;
    }


    private static List<SkillQueryModel> MapSkills(List<Skill> skills)
    {
        return skills.Select(v => new SkillQueryModel
        {
            Id = v.Id,
            PageId = v.PageId,
            Name = v.Name,
            Value = v.Value
        }).ToList();
    }
}