using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Skill;
using AccountManagement.Domain.SkillAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class SkillRepository : BaseRepository<long, Skill>, ISkillRepository
    {
        private readonly AccountContext _accountContext;

        public SkillRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<EditSkill> GetDetailsAsync(long id) =>
            await _accountContext.Skills.Select(v => new EditSkill
            {
                Id = v.Id,
                PageId = v.PageId,
                Name = v.Name,
                Value = v.Value
            }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<SkillViewModel>> SearchAsync(SkillSearchModel searchModel)
        {
            var query = _accountContext.Skills.Select(v => new SkillViewModel
            {
                Id = v.Id,
                Name = v.Name,
                Value = v.Value,
                PageId = v.PageId
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(v => v.Name.Contains(searchModel.Name));

            var result = await query.ToListAsync();

            foreach (var skillViewModel in result)
            {
                var page = await _accountContext.PersonalPages
                    .FirstOrDefaultAsync(v => v.Id == skillViewModel.PageId);
                
                skillViewModel.FullName = page?.FullName;
                skillViewModel.UserName = page?.Account.UserName;
            }

            return result;
        }

        public async ValueTask<List<SkillViewModel>> GetListByAsync(long pageId) =>
            await _accountContext.Skills.Where(v => v.PageId == pageId).Select(v => new SkillViewModel
            {
                Id = v.Id,
                Name = v.Name,
                PageId = v.PageId,
                Value = v.Value
            }).AsNoTracking().ToListAsync();

        public async ValueTask<long> GetSkillIdByAsync(long pageId)
        {
            var skill =  await _accountContext.Skills.FirstOrDefaultAsync(v => v.PageId == pageId);
            return skill.Id;
        }
    }
}
