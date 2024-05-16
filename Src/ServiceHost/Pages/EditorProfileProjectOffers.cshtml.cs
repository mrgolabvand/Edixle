using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Account;
using EdixleQuery.Contracts.ProjectOffer;

namespace ServiceHost.Pages
{
    public class EditorProfileProjectOffersModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IProjectOfferQuery _projectOfferQuery;
        private readonly IAccountQuery _accountQuery;
        private readonly IPersonalPageApplication _personalPageApplication;

        public EditorProfileProjectOffersModel(IAuthHelper authHelper, IAccountQuery accountQuery, IPersonalPageApplication personalPageApplication, IProjectOfferQuery projectOfferQuery)
        {
            _authHelper = authHelper;
            _accountQuery = accountQuery;
            _personalPageApplication = personalPageApplication;
            _projectOfferQuery = projectOfferQuery;
        }

        public PersonalPageViewModel PersonalPageViewModel { get; set; }
        public AccountQueryModel AccountQueryModel { get; set; }
        public List<ProjectOfferQueryModel> ProjectOffers { get; set; }

        public async Task OnGet()
        {
            var accountId = _authHelper.CurrentAccountId();
            var pageId =await _personalPageApplication.GetPersonalPageIdByAsync(accountId);
            PersonalPageViewModel =await _personalPageApplication.GetPageInfoByAsync(pageId);
            AccountQueryModel =await _accountQuery.GetAccountAsync(accountId);
            ProjectOffers = await _projectOfferQuery.GetEditorProjectOffers(pageId);
        }
    }
}
