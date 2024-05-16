using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.Portfolio;
using EdixleQuery.Contracts.PortfolioCategory;
using EdixleQuery.Contracts.Review;
using ReviewManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using EdixleQuery.Contracts;
using Microsoft.EntityFrameworkCore;
using EdixleQuery.Contracts.PersonalPage;
using PayamakPanel.Core.Models;
using AccountManagement.Domain.PortfolioAgg;

namespace EdixleQuery.Query;

public class SharedQuery : ISharedQuery
{
    private readonly AccountContext _accountContext;
    private readonly ReviewContext _reviewContext;
    private readonly IPersonalPageApplication _personalPageApplication;
    private readonly IPortfolioCategoryQuery _portfolioCategoryQuery;

    public SharedQuery(AccountContext accountContext, ReviewContext reviewContext, IPersonalPageApplication personalPageApplication, IPortfolioCategoryQuery portfolioCategoryQuery)
    {
        _accountContext = accountContext;
        _reviewContext = reviewContext;
        _personalPageApplication = personalPageApplication;
        _portfolioCategoryQuery = portfolioCategoryQuery;
    }

    public async ValueTask<List<ReviewQueryModel>> GetReviewsAsync() =>
    await _reviewContext.Reviews
        .Where(v => v.IsConfirmed)
        .Select(v => new ReviewQueryModel
        {
            Id = v.Id,
            EditSoundQuality = v.EditSoundQuality,
            EditVideoQuality = v.EditVideoQuality,
            IsConfirmed = v.IsConfirmed,
            IsHarmful = v.IsHarmful,
            IsHelpful = v.IsHelpful,
            Message = v.Message,
            Name = v.Name,
            OwnerRecordId = v.OwnerRecordId,
            UseProperFontAndColors = v.UseProperFontAndColors,
            UseProperMemes = v.UseProperMemes,
            UseProperSoundEffects = v.UseProperSoundEffects,
            UseProperVideoEffects = v.UseProperVideoEffects,
            CreationDate = v.CreationDate.ToFarsi().ToPersianNumber()
        }).AsNoTracking().OrderByDescending(v => v.Id).ToListAsync();

    public PortfolioQueryModel CalculatePortfolioReviewsTotalRates(PortfolioQueryModel portfolio)
    {
        if (portfolio.Reviews.Any())
        {
            portfolio.EditVideoQualityTotalRate =
                Math.Round(portfolio.Reviews.Select(v => v.EditVideoQuality).ToList().Average(), 1);
            portfolio.UseProperVideoEffectsTotalRate =
                Math.Round(portfolio.Reviews.Select(v => v.UseProperVideoEffects).ToList().Average(), 1);

            portfolio.EditSoundQualityTotalRate =
                Math.Round(portfolio.Reviews.Select(v => v.EditSoundQuality).ToList().Average(), 1);
            portfolio.UseProperSoundEffectsTotalRate =
                Math.Round(portfolio.Reviews.Select(v => v.UseProperSoundEffects).ToList().Average(), 1);

            portfolio.UseProperFontAndColorsTotalRate =
                Math.Round(portfolio.Reviews.Select(v => v.UseProperFontAndColors).ToList().Average(), 1);

            portfolio.UseProperMemesTotalRate =
                Math.Round(portfolio.Reviews.Select(v => v.UseProperMemes).ToList().Average(), 1);

            var totalRate = new List<double>
            {
                portfolio.EditSoundQualityTotalRate,
                portfolio.EditVideoQualityTotalRate,
                portfolio.UseProperSoundEffectsTotalRate,
                portfolio.UseProperVideoEffectsTotalRate,
                portfolio.UseProperFontAndColorsTotalRate,
                portfolio.UseProperMemesTotalRate
            };

            portfolio.ReviewsTotalRate = Math.Round(totalRate.Average(), 1);
        }

        return portfolio;
    }
    public async ValueTask<PersonalPageQueryModel> CalculatePageReviewsTotalRate(PersonalPageQueryModel page)
    {
        var reviews = await GetReviewsAsync();

        page.Portfolios.ForEach(portfolio => portfolio.Reviews = reviews.Where(review => review.OwnerRecordId == portfolio.Id).ToList());
        page.Portfolios.ForEach(portfolio => page.Reviews.AddRange(portfolio.Reviews));
        page.Portfolios.ForEach(portfolio => CalculatePortfolioReviewsTotalRates(portfolio));

        page.EditVideoQualityTotalRate =
            Math.Round(page.Portfolios.Select(v => v.EditVideoQualityTotalRate).Average(), 1);
        page.EditSoundQualityTotalRate =
            Math.Round(page.Portfolios.Select(v => v.EditSoundQualityTotalRate).Average(), 1);
        page.UseProperVideoEffectsTotalRate =
            Math.Round(page.Portfolios.Select(v => v.UseProperVideoEffectsTotalRate).Average(), 1);
        page.UseProperSoundEffectsTotalRate =
            Math.Round(page.Portfolios.Select(v => v.UseProperSoundEffectsTotalRate).Average(), 1);
        page.UseProperFontAndColorsTotalRate =
            Math.Round(page.Portfolios.Select(v => v.UseProperFontAndColorsTotalRate).Average(), 1);
        page.UseProperMemesTotalRate =
            Math.Round(page.Portfolios.Select(v => v.UseProperMemesTotalRate).Average(), 1);
        page.ReviewsTotalRate = Math.Round(page.Portfolios.Select(v => v.ReviewsTotalRate).Average(), 1);

        return page;
    }

    public async ValueTask<PersonalPageQueryModel> ConfigurePagePortfolios(PersonalPageQueryModel page)
    {
        var portfolios = await _accountContext.Portfolios
            .Where(v => v.PageId == page.Id && !v.IsRemoved && v.IsConfirm)
            .Select(v => new PortfolioQueryModel
            {
                Id = v.Id,
                PageId = v.PageId,
                Description = v.Description,
                Video = v.Video,
                IsRemoved = v.IsRemoved,
                Keywords = v.Keywords,
                MetaDescription = v.MetaDescription,
                Name = v.Name,
                Picture = v.Picture,
                Slug = v.Slug,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                ShortDescription =
                    v.ShortDescription.Substring(0, Math.Min(v.ShortDescription.Length, 50)) + " ...",
                Tags = v.Tags,
            }).AsNoTracking().ToListAsync();

        page.Portfolios.AddRange(portfolios);

        if (page.Portfolios.Any())
        {
            page.Portfolios.ForEach(v => v.TagList = v.Tags.TagSplitter());
            page = await CalculatePageReviewsTotalRate(page);
        }

        return page;
    }


    public async ValueTask<PortfolioQueryModel> ConfigurePortfolio(PortfolioQueryModel portfolio)
    {
        portfolio.TagList = portfolio.Tags.TagSplitter();
        var reviews = await GetReviewsAsync();
        portfolio.Reviews = reviews.Where(v => v.OwnerRecordId == portfolio.Id).ToList();
        portfolio = CalculatePortfolioReviewsTotalRates(portfolio);
        var categories = await _portfolioCategoryQuery.GetCategoriesAsync();
        foreach (var category in portfolio.PortfoliosAndCategories.Select(portfoliosAndCategory =>
                     categories.FirstOrDefault(v => v.Id == portfoliosAndCategory.CategoryId)))
        {
            portfolio.PortfolioCategories.Add(category);
        }

        return portfolio;
    }
}