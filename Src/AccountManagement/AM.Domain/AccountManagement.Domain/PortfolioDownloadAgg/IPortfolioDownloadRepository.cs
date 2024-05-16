using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.PortfolioDownload;

namespace AccountManagement.Domain.PortfolioDownloadAgg
{
    public interface IPortfolioDownloadRepository : IBaseRepository<long, PortfolioDownload>
    {
        ValueTask<double> GetDownloadCountAsync(long portfolioId);
    }
}