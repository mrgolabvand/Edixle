using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Plan;
using EdixleQuery.Contracts.ProjectOffer;

namespace ServiceHost.Pages
{
    public class ShowOtherOffersModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IEditorPlanQuery _editorPlanQuery;
        private readonly IProjectOfferQuery _projectOfferQuery;
        private readonly IPersonalPageApplication _personalPageApplication;
        public ShowOtherOffersModel(IAuthHelper authHelper, IPersonalPageApplication personalPageApplication, IEditorPlanQuery editorPlanQuery, IProjectOfferQuery projectOfferQuery)
        {
            _authHelper = authHelper;
            _personalPageApplication = personalPageApplication;
            _editorPlanQuery = editorPlanQuery;
            _projectOfferQuery = projectOfferQuery;
        }

        public List<ProjectOfferQueryModel> ProjectOffers { get; set; }

        public async Task OnGet(long projectId)
        {
            var accountId = _authHelper.CurrentAccountId();
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(accountId);
            var pagePlan = await _editorPlanQuery.GetEditorPagePlanAsync(pageId);

            if (pagePlan.IsPlanActive && pagePlan.VipProjectOfferCount > 0)
            {
                    await _personalPageApplication.UseVipOfferAsync(pageId);
                    ProjectOffers = await _projectOfferQuery.GetProjectOffersAsync(projectId);
            }
            else
            {
                ProjectOffers = new List<ProjectOfferQueryModel>();
            }

        }
    }
}
