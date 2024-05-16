using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Skill
{
    public interface ISkillApplication
    {
        ValueTask<OperationResult> AddAsync(AddSkill command);
        ValueTask<OperationResult> AddAsync(List<AddSkill> commands);
        ValueTask<OperationResult> EditAsync(EditSkill command);
        ValueTask<OperationResult> RemoveAsync(long id);
        ValueTask<OperationResult> RestoreAsync(long id);
        ValueTask<EditSkill> GetDetailsAsync(long id);
        ValueTask<long> GetSkillIdByAsync(long pageId);
        ValueTask<List<SkillViewModel>> SearchAsync(SkillSearchModel searchModel);
        ValueTask<List<SkillViewModel>> GetListByAsync(long pageId);
    }
}
