using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IBaseRepository<long, Role>
    {
        ValueTask<EditRole> GetDetailsAsync(long id);
        ValueTask<List<RoleViewModel>> GetListAsync();
    }
}
