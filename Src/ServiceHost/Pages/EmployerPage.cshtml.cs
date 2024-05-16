using _0_Framework.Application;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountManagement.Application.Contracts.Account;

namespace ServiceHost.Pages
{
    public class EmployerPageModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IProjectApplication _projectApplication;
        private readonly IAccountApplication _accountApplication;
        private readonly IEmployerPageApplication _employerPageApplication;

        public List<ProjectViewModel> Projects { get; set; }
        [BindProperty] public EditEmployerPage EmployerPage { get; set; }
        public EmployerPageViewModel EmployerPageViewModel { get; set; }
        
        public EmployerPageModel(IProjectApplication projectApplication, IEmployerPageApplication employerPageApplication, IAccountApplication accountApplication, IAuthHelper authHelper)
        {
            _projectApplication = projectApplication;
            _employerPageApplication = employerPageApplication;
            _accountApplication = accountApplication;
            _authHelper = authHelper;
        }

        public async Task OnGet()
        {
            var employerPageId = await _employerPageApplication.GetEmployerPageIdByAccountIdAsync(_authHelper.CurrentAccountId());
            EmployerPage = await _employerPageApplication.GetDetailsAsync(employerPageId);
            EmployerPageViewModel =await _employerPageApplication.GetViewModelAsync(employerPageId);
            Projects = await _projectApplication.GetEmployerProjectsAsync(employerPageId);
        }
        public async Task<IActionResult> OnGetValidate()
        {
            var id = _authHelper.CurrentAccountId();
            return !await _accountApplication.IsEmployerAsync(id) ? RedirectToPage("./AddEmployerPage") : RedirectToPage("./AddProject");
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }
            EmployerPage.AccountId = _authHelper.CurrentAccountId();
            await _employerPageApplication.EditAsync(EmployerPage);
            return RedirectToPage();
        }
    }
}
