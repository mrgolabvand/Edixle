using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Portfolio;

namespace AccountManagement.Domain.PortfolioAgg
{
    public interface IPortfolioRepository : IBaseRepository<long, Portfolio>
    {
        ValueTask<PortfolioViewModel> GetFileFromPortfolioAsync(long id);
        ValueTask<EditPortfolio> GetDetailsAsync(long id, long pageId);
        ValueTask<EditPortfolio> GetDetailsAsync(long id);
        ValueTask<PortfolioViewModel> GetPortfolioAsync(long id);
        ValueTask<List<PortfolioViewModel>> SearchAsync(PortfolioSearchModel searchModel);
        ValueTask<long> GetPortfolioIdByAsync(string portfolioSlug);
        ValueTask<long> GetPortfoliosCountAsync(long pageId);
    }
}
