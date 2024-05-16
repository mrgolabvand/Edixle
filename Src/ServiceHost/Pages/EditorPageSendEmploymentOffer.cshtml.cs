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
    public class EditorPageSendEmploymentOfferModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IPersonalPageQuery _personalPageQuery;
        private readonly IEmployerPageQuery _employerPageQuery;
        private readonly IJobOfferApplication _jobOfferApplication;
        private readonly IPersonalPageApplication _personalPageApplication;
        private readonly ISmsService _smsService;
        private readonly IEmployerPageApplication _employerPageApplication;

        public EditorPageSendEmploymentOfferModel(IPersonalPageQuery personalPageQuery, IPersonalPageApplication personalPageApplication, IJobOfferApplication jobOfferApplication, IEmployerPageApplication employerPageApplication, IAuthHelper authHelper, IEmployerPageQuery employerPageQuery, ISmsService smsService)
        {
            _personalPageQuery = personalPageQuery;
            _personalPageApplication = personalPageApplication;
            _jobOfferApplication = jobOfferApplication;
            _employerPageApplication = employerPageApplication;
            _authHelper = authHelper;
            _employerPageQuery = employerPageQuery;
            _smsService = smsService;
        }

        public PersonalPageQueryModel PersonalPage = new();
        [BindProperty] public AddJobOffer JobOffer { get; set; }
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

        public async Task<IActionResult> OnPost()
        {
            JobOffer.EmployerPageId =
                await _employerPageApplication.GetEmployerPageIdByAccountIdAsync(_authHelper.CurrentAccountId());
            JobOffer.Price = 20_000;
            JobOffer.Title += " - استخدام";
            await _jobOfferApplication.AddAsync(JobOffer);

            var editorPhoneNumber =
                await _personalPageApplication.GetEditorPhoneNumberByEditorPageIdAsync(JobOffer.EditorPageId);
            _smsService.Send_NewJobOfferNotif_ToEditor_MeliPayamak_Com(editorPhoneNumber);
            return RedirectToPage("./EmployerPageJobOffers");
        }
    }
}
