using _0_Framework.Application;
using _0_Framework.Application.Sms;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.JobOffer;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.EmployerPage;
using EdixleQuery.Contracts.PersonalPage;

namespace ServiceHost.Pages
{
    public class EditorPageChoiceOfferModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IPersonalPageQuery _personalPageQuery;
        private readonly IEmployerPageQuery _employerPageQuery;
        private readonly IPersonalPageApplication _personalPageApplication;

        public EditorPageChoiceOfferModel(IPersonalPageQuery personalPageQuery, IPersonalPageApplication personalPageApplication, IAuthHelper authHelper, IEmployerPageQuery employerPageQuery)
        {
            _personalPageQuery = personalPageQuery;
            _personalPageApplication = personalPageApplication;
            _authHelper = authHelper;
            _employerPageQuery = employerPageQuery;
        }

        public PersonalPageQueryModel PersonalPage = new();
        public bool IsEditor { get; set; }
        public bool IsEmployer { get; set; }

        public async Task OnGet(string id)
        {
            var accountId = _authHelper.CurrentAccountId();
            IsEditor = await _personalPageQuery.IsEditorAsync(accountId);
            IsEmployer = await _employerPageQuery.IsEmployerAsync(accountId);
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(id);
            PersonalPage = await _personalPageQuery.GetPageAsync(id, pageId);
        }
    }
}
