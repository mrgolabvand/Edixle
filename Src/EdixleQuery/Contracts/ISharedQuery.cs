using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.Portfolio;
using EdixleQuery.Contracts.Review;

namespace EdixleQuery.Contracts;

public interface ISharedQuery
{
    ValueTask<List<ReviewQueryModel>> GetReviewsAsync();
    PortfolioQueryModel CalculatePortfolioReviewsTotalRates(PortfolioQueryModel portfolio);
    ValueTask<PersonalPageQueryModel> CalculatePageReviewsTotalRate(PersonalPageQueryModel page);
    ValueTask<PersonalPageQueryModel> ConfigurePagePortfolios(PersonalPageQueryModel page);
    ValueTask<PortfolioQueryModel> ConfigurePortfolio(PortfolioQueryModel portfolio);
}