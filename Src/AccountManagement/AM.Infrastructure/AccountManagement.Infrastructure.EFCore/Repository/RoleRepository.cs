using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : BaseRepository<long, Role>, IRoleRepository
    {
        private readonly AccountContext _accountContext;

        public RoleRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<EditRole> GetDetailsAsync(long id)
        {
            var role = await _accountContext.Roles.Select(v => new EditRole
            {
                Id = v.Id,
                Name = v.Name,
                MappedPermissions = MapPermissions(v.Permissions)
            }).AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            role.Permissions = role.MappedPermissions.Select(v => v.Code).ToList();

            return role;
        }

        private static List<PermissionDto> MapPermissions(List<Permission> permissions) =>
            permissions.Select(v => new PermissionDto (v.Code, v.Name)).ToList();

        public async ValueTask<List<RoleViewModel>> GetListAsync() => await _accountContext.Roles.Select(v => new RoleViewModel
            {
                Id = v.Id,
                Name = v.Name,
                CreationDate = v.CreationDate.ToFarsi()
            }).ToListAsync();
    }
}
