using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Portfolio
{
    public interface IPortfolioApplication
    {
        ValueTask<(OperationResult, string)> CreateAsync(CreatePortfolio command);
        ValueTask<(OperationResult, string)> EditAsync(EditPortfolio command);
        ValueTask<EditPortfolio> GetDetailsAsync(long id, long pageId);
        ValueTask<EditPortfolio> GetDetailsAsync(long id);
        ValueTask<List<PortfolioViewModel>> SearchAsync(PortfolioSearchModel searchModel);
        ValueTask<PortfolioViewModel> GetPortfolioAsync(long id);
        ValueTask<OperationResult> RemoveAsync(long id);
        ValueTask<OperationResult> RestoreAsync(long id);
        ValueTask<OperationResult> ConfirmAsync(long id);
        ValueTask<OperationResult> CancelAsync(long id);
        ValueTask<PortfolioViewModel> GetFileFromPortfolioAsync(long id);
        ValueTask<long> GetDownloadCountAsync(long id);
        ValueTask<long> GetPortfolioIdByAsync(string portfolioSlug);
    }
}
 