using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.Sms;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Application.Contracts.ProjectOffer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.Plan;
using EdixleQuery.Contracts.Project;

namespace ServiceHost.Pages
{
    public class ProjectDetailsModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IProjectQuery _projectQuery;
        private readonly IProjectOfferApplication _projectOfferApplication;
        private readonly IAccountApplication _accountApplication;
        private readonly ISmsService _smsService;
        private readonly IPersonalPageApplication _personalPageApplication;
        private readonly IEmployerPageApplication _employerPageApplication;
        private readonly IPersonalPageQuery _personalPageQuery;
        private readonly IEditorPlanQuery _editorPlanQuery;

        public ProjectDetailsModel(IProjectOfferApplication projectOfferApplication, IProjectQuery projectQuery, IPersonalPageApplication personalPageApplication, IAuthHelper authHelper, IEditorPlanQuery editorPlanQuery, IPersonalPageQuery personalPageQuery, IAccountApplication accountApplication, ISmsService smsService, IEmployerPageApplication employerPageApplication)
        {
            _projectOfferApplication = projectOfferApplication;
            _projectQuery = projectQuery;
            _personalPageApplication = personalPageApplication;
            _authHelper = authHelper;
            _editorPlanQuery = editorPlanQuery;
            _personalPageQuery = personalPageQuery;
            _accountApplication = accountApplication;
            _smsService = smsService;
            _employerPageApplication = employerPageApplication;
        }

        public bool IsEditor { get; set; }
        public ProjectQueryModel Project { get; set; }
        public EditorPlanQueryModel EditorPlan = new();
        [BindProperty] public AddProjectOffer ProjectOffer { get; set; }
        [BindProperty] public long EmployerPageId { get; set; }

        public async Task OnGet(long id)
        {
            var accountId = _authHelper.CurrentAccountId();
            var editorPageId = await _personalPageApplication.GetPersonalPageIdByAsync(accountId);
            IsEditor = await _personalPageQuery.IsEditorAsync(accountId);
            if (IsEditor)
            {
                EditorPlan = await _editorPlanQuery.GetEditorPagePlanAsync(editorPageId);
            }
            Project = await _projectQuery.GetProjectAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var editorPageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            ProjectOffer.EditorPageId = editorPageId;
            var result = await _projectOfferApplication.AddAsync(ProjectOffer);
            if (result.IsSucceeded)
            {
                var employerPhoneNumber = await _employerPageApplication
                    .GetEmployerPhoneNumberByEmployerPageIdAsync(EmployerPageId);
                _smsService.Send_NewProjectOfferNotif_ToEmployer_MeliPayamak_Com(employerPhoneNumber);
                return RedirectToPage("./EditorProfileProjectOffers");
            }
            return RedirectToPage("./Index");
        }

    }
}
