using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.PortfolioBaseCategory
{
    public interface IPortfolioBaseCategoryApplication
    {
        ValueTask<OperationResult> CreateAsync(CreatePortfolioBaseCategory command);
        ValueTask<OperationResult> EditAsync(EditPortfolioBaseCategory command);
        ValueTask<EditPortfolioBaseCategory> GetDetailsAsync(long id);
        ValueTask<List<PortfolioBaseCategoryViewModel>> SearchAsync(PortfolioBaseCategorySearchModel searchModel);
        ValueTask<OperationResult> RemoveAsync(long id);
        ValueTask<OperationResult> RestoreAsync(long id);
        ValueTask<List<PortfolioBaseCategoryViewModel>> GetListAsync();
    }
}
