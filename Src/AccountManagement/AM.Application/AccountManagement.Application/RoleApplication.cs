using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async ValueTask<OperationResult> CreateAsync(CreateRole command)
        {
            var operation = new OperationResult();

            if (await _roleRepository.ExistsAsync(v => v.Name == command.Name))
                return operation.Failed(ApplicationMessages.IsDuplicated);
            
            var role = new Role(command.Name, new List<Permission>());
            await _roleRepository.CreateAsync(role);
            await _roleRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditRole command)
        {
            var operation = new OperationResult();

            var role =await _roleRepository.GetAsync(command.Id);

            if (role == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (await _roleRepository.ExistsAsync(v => v.Name == command.Name && v.Id != command.Id))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var permissions = new List<Permission>();
            command.Permissions.ForEach(code => permissions.Add(new Permission(code)));

            role.Edit(command.Name, permissions);
            await _roleRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditRole> GetDetailsAsync(long id) => await _roleRepository.GetDetailsAsync(id);

        public async ValueTask<List<RoleViewModel>> GetListAsync() => await _roleRepository.GetListAsync();
    }
}
