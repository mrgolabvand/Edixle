using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.Plan;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class EditorPlanQuery : IEditorPlanQuery
    {
        private readonly AccountContext _accountContext;

        public EditorPlanQuery(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<EditorPlanQueryModel> GetEditorPagePlanAsync(long pageId)
        {
            var pagePlan = await _accountContext.PersonalPages
                .Select(v => new EditorPlanQueryModel
            {
                Id = v.Id,
                VipProjectOfferCount = v.VipProjectOfferCount,
                PortfolioUploadSizeLimit = v.PortfolioUploadSizeLimit,
                ChatUploadSizeLimit = v.ChatUploadSizeLimit,
                ExpireDate = v.ExpireDate
            }).FirstOrDefaultAsync(v => v.Id == pageId);

            var remainingDays = (pagePlan.ExpireDate - DateTime.Now).Days;
            if (remainingDays <= 0)
            {
                remainingDays = 0;
            }
            pagePlan.RemainingDays = remainingDays;
            if (pagePlan.RemainingDays > 0)
            {
                pagePlan.IsPlanActive = true;
            }

            return pagePlan;
        }
    }
}
