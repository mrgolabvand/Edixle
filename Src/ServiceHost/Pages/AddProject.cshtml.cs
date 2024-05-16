using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.Sms;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AddProjectModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly ISmsService _smsService;
        private readonly IProjectApplication _projectApplication;
        private readonly IAccountApplication _accountApplication;
        private readonly IEmployerPageApplication _employerPageApplication;
        public AddProjectModel(IProjectApplication projectApplication, IAuthHelper authHelper, IEmployerPageApplication employerPageApplication, IAccountApplication accountApplication, ISmsService smsService)
        {
            _projectApplication = projectApplication;
            _authHelper = authHelper;
            _employerPageApplication = employerPageApplication;
            _accountApplication = accountApplication;
            _smsService = smsService;
        }

        [BindProperty]
        public AddProject Project { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var accountId = _authHelper.CurrentAccountId();
            if (await _accountApplication.IsEditorAsync(accountId))
                return RedirectToPage("./Index");

            Project.EmployerPageId = await _employerPageApplication.GetEmployerPageIdByAccountIdAsync(_authHelper.CurrentAccountId());
            var result = await _projectApplication.AddAsync(Project);

            if (result.IsSucceeded)
            {
                //var numbers = await _accountApplication.GetEditorsPhoneNumbers();
                //_smsService.Send_NewProjectAddedNotif_ToEditors_MeliPayamak_Com(numbers);
                return RedirectToPage("/EmployerPageProjects");
            }

            return RedirectToPage();
        }
    }
}
