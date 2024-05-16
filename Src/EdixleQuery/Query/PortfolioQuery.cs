using _0_Framework.Application;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Application.Contracts.PortfolioAndCategoryLinked;
using AccountManagement.Domain.PortfolioAndCategoryLinkedAgg;
using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts;
using EdixleQuery.Contracts.Portfolio;
using EdixleQuery.Contracts.PortfolioCategory;
using EdixleQuery.Contracts.Review;
using Microsoft.EntityFrameworkCore;
using ReviewManagement.Infrastructure.EFCore;

namespace EdixleQuery.Query;

public class PortfolioQuery : IPortfolioQuery
{
    private readonly AccountContext _accountContext;
    private readonly ReviewContext _reviewContext;
    private readonly ISharedQuery _sharedQuery;
    private readonly IPersonalPageApplication _personalPageApplication;
    private readonly IPortfolioCategoryQuery _portfolioCategoryQuery;
    public PortfolioQuery(AccountContext accountContext, IPersonalPageApplication personalPageApplication, ReviewContext reviewContext, IPortfolioCategoryQuery portfolioCategoryQuery, ISharedQuery sharedQuery)
    {
        _accountContext = accountContext;
        _personalPageApplication = personalPageApplication;
        _reviewContext = reviewContext;
        _portfolioCategoryQuery = portfolioCategoryQuery;
        _sharedQuery = sharedQuery;
    }

    public async ValueTask<List<PortfolioQueryModel>> SearchAsync(PortfolioSearchQuery searchModel)
    {
        var query = _accountContext.Portfolios
            .Include(v => v.Page)
            .Where(v => v.PageId == searchModel.pageId && !v.IsRemoved && v.IsConfirm && v.Page.IsActive && v.Page.IsConfirm)
            .Select(v => new PortfolioQueryModel
            {
                Id = v.Id,
                ShortDescription = v.ShortDescription,
                Tags = v.Tags,
                Picture = v.Picture,
                Name = v.Name,
                CreationDate = v.CreationDate.ToFarsi(),
                PageId = v.PageId,
                IsRemoved = v.IsRemoved
            });

        if (searchModel == null) return await query.ToListAsync();

        if (string.IsNullOrWhiteSpace(searchModel.Name)) return await query.ToListAsync();

        query = query.Where(v => v.Name.Contains(searchModel.Name));
        query = query.Where(v => v.Tags.Contains(searchModel.Name));

        return await query.ToListAsync();
    }

    public async ValueTask<List<PortfolioQueryModel>> UserSearchAsync(PortfolioUserSearchQuery searchModel)
    {
        var query = _accountContext.Portfolios
            .Include(v => v.Categories)
            .Include(v => v.Page)
            .Where(v => !v.IsRemoved && v.IsConfirm && v.Page.IsActive && v.Page.IsConfirm)
            .Select(v => new PortfolioQueryModel
            {
                Id = v.Id,
                CreationDate = v.CreationDate.ToFarsi(),
                Description = v.Description,
                Video = v.Video,
                Keywords = v.Keywords,
                MetaDescription = v.MetaDescription,
                Name = v.Name,
                PageId = v.PageId,
                Picture = v.Picture,
                Slug = v.Slug,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                PagePicture = v.Page.Picture,
                PagePictureAlt = v.Page.PictureAlt,
                PagePictureTitle = v.Page.PictureTitle,
                AccountName = v.Page.FullName,
                AccountSlug = v.Page.Slug,
                ShortDescription = v.ShortDescription,
                Tags = v.Tags,
                IsRemoved = v.IsRemoved,
                PortfoliosAndCategories = MapPortfolioAndCategory(v.Categories)
            });

        var result = new List<PortfolioQueryModel>();

        if (searchModel != null)
        {
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(v => v.Name.Contains(searchModel.Name));
            }

            if (searchModel.CategoriesDictionary.Any(v => v.Value))
            {
                foreach (var portfolio in await query.ToListAsync())
                {
                    foreach (var dictionary in searchModel.CategoriesDictionary.Where(v => v.Value))
                    {
                        result.AddRange(from category in portfolio.PortfoliosAndCategories where category.CategoryId == dictionary.Key select portfolio);
                    }
                }
            }

            result = result.Any() ? result.Distinct().ToList() : await query.ToListAsync();
        }

        var portfolios = new List<PortfolioQueryModel>();

        foreach (var portfolio in result)
        {
            portfolios.Add(await _sharedQuery.ConfigurePortfolio(portfolio));
        }

        if (searchModel == null) return portfolios;
        {
            portfolios = searchModel.OrderId switch
            {
                1 => portfolios.OrderByDescending(v => v.Id).ToList(),
                2 => portfolios.OrderByDescending(v => v.ReviewsTotalRate).ToList(),
                _ => portfolios
            };
        }
        return portfolios;
    }

    public async ValueTask<List<PortfolioQueryModel>> SettingSearchAsync(PortfolioSearchQuery searchModel)
    {
        var query = _accountContext.Portfolios
            .Include(v => v.Page)
            .Where(v => v.PageId == searchModel.pageId && v.IsConfirm && v.Page.IsActive)
            .Select(v => new PortfolioQueryModel
            {
                Id = v.Id,
                ShortDescription = v.ShortDescription,
                Tags = v.Tags,
                Picture = v.Picture,
                Name = v.Name,
                CreationDate = v.CreationDate.ToFarsi(),
                PageId = v.PageId,
                IsRemoved = v.IsRemoved
            });

        if (searchModel != null)
        {
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(v => v.Name.Contains(searchModel.Name));
        }

        return await query.ToListAsync();
    }

    public async ValueTask<List<PortfolioQueryModel>> GetEditorPortfoliosAsync(long pageId) =>
        await _accountContext.Portfolios
            .Include(v => v.Page)
            .Where(v => v.PageId == pageId && v.IsConfirm && v.Page.IsActive)
            .Select(v => new PortfolioQueryModel
            {
                Id = v.Id,
                ShortDescription = v.ShortDescription,
                Tags = v.Tags,
                Picture = v.Picture,
                Name = v.Name,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                PageId = v.PageId,
                IsRemoved = v.IsRemoved,
            }).AsNoTracking().ToListAsync();

    public async ValueTask<PortfolioQueryModel> GetPortfolioByAsync(string slug)
    {
        var portfolio = await _accountContext.Portfolios
            .Include(v => v.Categories)
            .Include(v => v.Page)
            .Where(v => !v.IsRemoved && v.IsConfirm && v.Page.IsActive && v.Page.IsConfirm)
            .Select(v => new PortfolioQueryModel
            {
                Id = v.Id,
                CreationDate = v.CreationDate.ToFarsi(),
                Description = v.Description,
                Video = v.Video,
                Keywords = v.Keywords,
                MetaDescription = v.MetaDescription,
                Name = v.Name,
                PageId = v.PageId,
                Picture = v.Picture,
                Slug = v.Slug,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                PagePicture = v.Page.Picture,
                PagePictureAlt = v.Page.PictureAlt,
                PagePictureTitle = v.Page.PictureTitle,
                AccountName = v.Page.FullName,
                AccountSlug = v.Page.Slug,
                ShortDescription = v.ShortDescription,
                Tags = v.Tags,
                IsRemoved = v.IsRemoved,
                PortfoliosAndCategories = MapPortfolioAndCategory(v.Categories)
            }).FirstOrDefaultAsync(v => v.Slug == slug);

        if (portfolio == null)
            return null;

        return await _sharedQuery.ConfigurePortfolio(portfolio);
    }

    private static List<PortfolioAndCategoryLinkViewModel> MapPortfolioAndCategory(List<PortfolioAndCategoryLinked> categories)
    {
        return categories.Select(v => new PortfolioAndCategoryLinkViewModel
        {
            CategoryId = v.PortfolioCategoryId,
            PortfolioId = v.PortfolioId
        }).ToList();
    }

    public async ValueTask<List<PortfolioQueryModel>> GetPortfoliosByCategoryAsync(string category) =>
        await _accountContext.Portfolios
            .Include(v => v.Page)
            .Where(v => !v.IsRemoved && v.IsConfirm && v.Page.IsActive && v.Page.IsConfirm)
            .Select(v => new PortfolioQueryModel
            {
                Id = v.Id,
                Name = v.Name,
                Picture = v.Picture,
                Slug = v.Slug,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                PagePicture = v.Page.Picture,
                PagePictureAlt = v.Page.PictureAlt,
                PagePictureTitle = v.Page.PictureTitle,
                AccountName = v.Page.FullName,
                AccountSlug = v.Page.Slug,
                ShortDescription = v.ShortDescription.Substring(0, Math.Min(v.ShortDescription.Length, 40)) + " ...",
            }).AsNoTracking().ToListAsync();

    public async ValueTask<List<PortfolioQueryModel>> GetLatestPortfoliosAsync() =>
        await _accountContext.Portfolios
            .Include(v => v.Page)
            .Where(v => !v.IsRemoved && v.IsConfirm && v.Page.IsActive && v.Page.IsConfirm)
            .Select(v => new PortfolioQueryModel
            {
                Id = v.Id,
                Name = v.Name,
                Slug = v.Slug,
                CreationDate = v.CreationDate.ToFarsi(),
                Picture = v.Picture,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
            }).AsNoTracking().OrderByDescending(v => v.Id).Take(4).ToListAsync();

    public async ValueTask<List<PortfolioQueryModel>> GetRelatedPortfoliosAsync(string tags)
    {
        List<string> tagList = tags.Split(",").ToList();

        var portfolios = _accountContext.Portfolios
            .Include(v => v.Page)
            .Where(v => !v.IsRemoved && v.IsConfirm && v.Page.IsActive && v.IsConfirm)
            .Select(v => new PortfolioQueryModel
            {
                Id = v.Id,
                Name = v.Name,
                Slug = v.Slug,
                CreationDate = v.CreationDate.ToFarsi(),
                Picture = v.Picture,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                PageId = v.PageId,
                Tags = v.Tags
            });

        var acceptedPortfolios = new List<PortfolioQueryModel>();

        //var portfolioTagList = new List<string>();
        //foreach (var portfolio in portfolios)
        //{
        //    portfolioTagList = portfolio.Tags.Split(",").ToList();
        //    foreach (var portfolioTag in portfolioTagList)
        //    {
        //        foreach (var tag in tagList)
        //        {
        //            if (portfolioTag == tag)
        //            {
        //                acceptedPortfolios.Add(portfolio);
        //            }
        //        }
        //    }
        //}


        var result = acceptedPortfolios.Distinct().OrderByDescending(v => v.Id).Take(6).ToList();

        foreach (var portfolioQueryModel in result)
        {
            var pageInfo = await _personalPageApplication.GetPageInfoByAsync(portfolioQueryModel.PageId);

            portfolioQueryModel.AccountName = pageInfo.FullName;
            portfolioQueryModel.AccountSlug = pageInfo.Slug;
            portfolioQueryModel.PagePicture = pageInfo.Picture;
            portfolioQueryModel.PagePictureAlt = pageInfo.PictureAlt;
            portfolioQueryModel.PagePictureTitle = pageInfo.PictureTitle;
        }

        return result;
    }

    public async ValueTask<PortfolioQueryModel> GetFileFromPortfolioAsync(long id)
    {
        var query = await _accountContext.Portfolios.Select(v => new PortfolioQueryModel
        {
            Id = v.Id,
            Name = v.Name,
            Video = v.Video,
            PageId = v.PageId
        }).FirstOrDefaultAsync(v => v.Id == id);

        var portfolio = await _personalPageApplication.GetPageInfoByAsync(query.PageId);
        query.AccountName = portfolio.FullName;

        return query;
    }

    public async ValueTask<List<PortfolioQueryModel>> GetTopPortfoliosAsync()
    {
        var query = await _accountContext.Portfolios
            .Include(v => v.Page)
            .Where(v => !v.IsRemoved && v.IsConfirm && v.Page.IsActive && v.Page.IsConfirm)
            .Select(v => new PortfolioQueryModel
            {
                Id = v.Id,
                CreationDate = v.CreationDate.ToFarsi(),
                Description = v.Description,
                Video = v.Video,
                Keywords = v.Keywords,
                MetaDescription = v.MetaDescription,
                Name = v.Name,
                PageId = v.PageId,
                Picture = v.Picture,
                Slug = v.Slug,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                PagePicture = v.Page.Picture,
                PagePictureAlt = v.Page.PictureAlt,
                PagePictureTitle = v.Page.PictureTitle,
                AccountName = v.Page.FullName,
                AccountSlug = v.Page.Slug,
                ShortDescription = v.ShortDescription,
                Tags = v.Tags,
                IsRemoved = v.IsRemoved,
                PortfoliosAndCategories = MapPortfolioAndCategory(v.Categories)
            }).AsNoTracking().OrderByDescending(v => v.Id).Take(8).ToListAsync();

        foreach (var portfolio in query)
        {
            var categories = await _portfolioCategoryQuery.GetCategoriesAsync();
            foreach (var category in portfolio.PortfoliosAndCategories.Select(portfoliosAndCategory =>
                         categories.FirstOrDefault(v => v.Id == portfoliosAndCategory.CategoryId)))
            {
                portfolio.PortfolioCategories.Add(category);
            }
        }

        foreach (var portfolioQueryModel in query)
        {
            var pageInfo = await _personalPageApplication.GetPageInfoByAsync(portfolioQueryModel.PageId);

            portfolioQueryModel.AccountName = pageInfo.FullName;
            portfolioQueryModel.AccountSlug = pageInfo.Slug;
            portfolioQueryModel.PagePicture = pageInfo.Picture;
            portfolioQueryModel.PagePictureAlt = pageInfo.PictureAlt;
            portfolioQueryModel.PagePictureTitle = pageInfo.PictureTitle;
        }

        return query;
    }
}