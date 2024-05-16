using _0_Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contracts.Role
{
    public interface IRoleApplication
    {
        ValueTask<OperationResult> CreateAsync(CreateRole command);
        ValueTask<OperationResult> EditAsync(EditRole command);
        ValueTask<EditRole> GetDetailsAsync(long id);
        ValueTask<List<RoleViewModel>> GetListAsync();
    }
}
