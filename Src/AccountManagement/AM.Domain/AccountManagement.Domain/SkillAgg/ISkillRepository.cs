using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Skill;

namespace AccountManagement.Domain.SkillAgg
{
    public interface ISkillRepository : IBaseRepository<long, Skill>
    {
        ValueTask<EditSkill> GetDetailsAsync(long id);
        ValueTask<List<SkillViewModel>> SearchAsync(SkillSearchModel searchModel);
        ValueTask<List<SkillViewModel>> GetListByAsync(long pageId);
        ValueTask<long> GetSkillIdByAsync(long pageId);
    }
}
