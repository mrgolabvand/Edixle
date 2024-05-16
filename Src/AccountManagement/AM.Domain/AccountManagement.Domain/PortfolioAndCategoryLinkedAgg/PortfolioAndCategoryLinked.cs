using _0_Framework.Domain;
using AccountManagement.Domain.PortfolioAgg;
using AccountManagement.Domain.PortfolioCategoryAgg;

namespace AccountManagement.Domain.PortfolioAndCategoryLinkedAgg
{
    public class PortfolioAndCategoryLinked
    {
        public long PortfolioId { get; private set; }
        public long PortfolioCategoryId { get; private set; }
        public Portfolio Portfolio { get; private set; }
        public PortfolioCategory PortfolioCategory { get; private set; }

        public PortfolioAndCategoryLinked(long portfolioId, long portfolioCategoryId)
        {
            PortfolioId = portfolioId;
            PortfolioCategoryId = portfolioCategoryId;
        }
    }
}
