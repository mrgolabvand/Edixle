using System.Threading.Tasks;

namespace AccountManagement.Application.Contracts.PortfolioDownload
{
    public interface IPortfolioDownloadApplication
    {
        ValueTask IncreaseAsync(IncreasePortfolioDownload command);
        ValueTask<double> GetDownloadCountAsync(long portfolioId);
    }
}