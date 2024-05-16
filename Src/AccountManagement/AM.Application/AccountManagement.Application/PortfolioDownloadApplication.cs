using System.Threading.Tasks;
using AccountManagement.Application.Contracts.PortfolioDownload;
using AccountManagement.Domain.PortfolioDownloadAgg;

namespace AccountManagement.Application
{
    public class PortfolioDownloadApplication : IPortfolioDownloadApplication
    {
        private readonly IPortfolioDownloadRepository _portfolioDownloadRepository;

        public PortfolioDownloadApplication(IPortfolioDownloadRepository portfolioDownloadRepository)
        {
            _portfolioDownloadRepository = portfolioDownloadRepository;
        }

        public async ValueTask IncreaseAsync(IncreasePortfolioDownload command)
        {
            if(await _portfolioDownloadRepository.ExistsAsync(v => v.AccountId == command.AccountId && v.PortfolioId == command.PortfolioId))
                return;

            var download = new PortfolioDownload(command.AccountId, command.PortfolioId);

            await _portfolioDownloadRepository.CreateAsync(download);
            await _portfolioDownloadRepository.SaveChangesAsync();
        }

        public async ValueTask<double> GetDownloadCountAsync(long portfolioId) =>
            await _portfolioDownloadRepository.GetDownloadCountAsync(portfolioId);
    }
}
