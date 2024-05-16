using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.PortfolioCategory
{
    public interface IPortfolioCategoryApplication
    {
        ValueTask<OperationResult> CreateAsync(CreatePortfolioCategory command);
        ValueTask<OperationResult> EditAsync(EditPortfolioCategory command);
        ValueTask<EditPortfolioCategory> GetDetailsAsync(long id);
        ValueTask<List<PortfolioCategoryViewModel>> SearchAsync(PortfolioCategorySearchModel searchModel);
        ValueTask<OperationResult> RemoveAsync(long id);
        ValueTask<OperationResult> RestoreAsync(long id);
        ValueTask<List<PortfolioCategoryViewModel>> GetListAsync();
    }
}
