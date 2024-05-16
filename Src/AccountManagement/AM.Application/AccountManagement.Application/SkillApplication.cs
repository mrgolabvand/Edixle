using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Skill;
using AccountManagement.Domain.SkillAgg;

namespace AccountManagement.Application
{
    public class SkillApplication : ISkillApplication
    {
        private readonly ISkillRepository _skillRepository;

        public SkillApplication(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async ValueTask<OperationResult> AddAsync(AddSkill command)
        {
            var operation = new OperationResult();

            if (await _skillRepository.ExistsAsync(v => v.Name == command.Name && v.PageId == command.PageId))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var skill = new Skill(command.Name, command.Value, command.PageId);
            await _skillRepository.CreateAsync(skill);
            await _skillRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> AddAsync(List<AddSkill> commands)
        {
            var operation = new OperationResult();

            foreach (var command in commands)
            {
                if (await _skillRepository.ExistsAsync(v => v.Name == command.Name && v.PageId == command.PageId))
                    return operation.Failed(ApplicationMessages.IsDuplicated);

                var skill = new Skill(command.Name, command.Value, command.PageId);
                await _skillRepository.CreateAsync(skill);
                await _skillRepository.SaveChangesAsync();
            }


            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditSkill command)
        {
            var operation = new OperationResult();

            var skill = await _skillRepository.GetAsync(command.Id);

            if (skill == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            skill.Edit(command.Name, command.Value);
            await _skillRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> RemoveAsync(long id)
        {
            var operation = new OperationResult();

            var skill = await _skillRepository.GetAsync(id);

            if (skill == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            skill.Remove();
            await _skillRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> RestoreAsync(long id)
        {
            var operation = new OperationResult();

            var skill = await _skillRepository.GetAsync(id);

            if (skill == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            skill.Restore();
            await _skillRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditSkill> GetDetailsAsync(long id) =>
            await _skillRepository.GetDetailsAsync(id);

        public async ValueTask<long> GetSkillIdByAsync(long pageId) =>
            await _skillRepository.GetSkillIdByAsync(pageId);

        public async ValueTask<List<SkillViewModel>> SearchAsync(SkillSearchModel searchModel) =>
            await _skillRepository.SearchAsync(searchModel);

        public async ValueTask<List<SkillViewModel>> GetListByAsync(long pageId) =>
            await _skillRepository.GetListByAsync(pageId);
    }
}
