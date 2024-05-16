using _0_Framework.Domain;
using AccountManagement.Domain.PortfolioAgg;

namespace AccountManagement.Domain.PortfolioDownloadAgg
{
    public class PortfolioDownload : BaseEntity
    {
        public long AccountId { get; private set; }
        public long PortfolioId { get; private set; }
        public Portfolio Portfolio { get; private set; }

        public PortfolioDownload(long accountId, long portfolioId)
        {
            AccountId = accountId;
            PortfolioId = portfolioId;
        }
    }
}
