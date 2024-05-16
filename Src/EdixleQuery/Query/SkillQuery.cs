using _0_Framework.Application;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.Skill;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class SkillQuery : ISkillQuery
    {
        private readonly IAuthHelper _authHelper;
        private readonly AccountContext _accountContext;
        private readonly IPersonalPageApplication _personalPageApplication;
        public SkillQuery(AccountContext accountContext, IAuthHelper authHelper, IPersonalPageApplication personalPageApplication)
        {
            _accountContext = accountContext;
            _authHelper = authHelper;
            _personalPageApplication = personalPageApplication;
        }

        public async ValueTask<List<SkillQueryModel>> GetSettingListAsync()
        {
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            return  await _accountContext.Skills.Where(v => v.PageId == pageId).Select(v => new SkillQueryModel
            {
                Id = v.Id,
                Name = v.Name,
                PageId = v.PageId,
                Value = v.Value,
                IsRemoved = v.IsRemoved
            }).AsNoTracking().ToListAsync();
        }
        public async ValueTask<List<SkillQueryModel>> GetListAsync()
        {
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            return await _accountContext.Skills.Where(v => v.PageId == pageId && !v.IsRemoved)
                .Select(v => new SkillQueryModel
            {
                Id = v.Id,
                Name = v.Name,
                PageId = v.PageId,
                Value = v.Value,
                IsRemoved = v.IsRemoved
            }).AsNoTracking().ToListAsync();
        }

        public async ValueTask<List<SkillQueryModel>> GetListAsync(string slug)
        {
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(slug);
            return await _accountContext.Skills.Where(v => v.PageId == pageId && !v.IsRemoved).Select(v => new SkillQueryModel
            {
                Id = v.Id,
                Name = v.Name,
                PageId = v.PageId,
                Value = v.Value,
                IsRemoved = v.IsRemoved
            }).AsNoTracking().ToListAsync();
        }
    }
}
