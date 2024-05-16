using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Feature
{
    public interface IFeatureApplication
    {
        ValueTask<List<FeatureViewModel>> GetListAsync();
        ValueTask<OperationResult> CreateAsync(CreateFeature command);
        ValueTask<OperationResult> EditAsync(EditFeature command);
        ValueTask<OperationResult> RemoveAsync(long id);
        ValueTask<OperationResult> RestoreAsync(long id);
        ValueTask<EditFeature> GetDetailsAsync(long id);
    }
}