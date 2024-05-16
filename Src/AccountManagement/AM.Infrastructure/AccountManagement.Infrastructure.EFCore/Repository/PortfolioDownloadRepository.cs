using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Domain.PortfolioDownloadAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class PortfolioDownloadRepository : BaseRepository<long, PortfolioDownload>, IPortfolioDownloadRepository
    {
        private readonly AccountContext _accountContext;

        public PortfolioDownloadRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<double> GetDownloadCountAsync(long portfolioId) =>
            await _accountContext.PortfolioDownloads.CountAsync(v => v.PortfolioId == portfolioId);
    }
}
