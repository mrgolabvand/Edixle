using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class EmployerPageProjectsModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IProjectApplication _projectApplication;
        private readonly IEmployerPageApplication _employerPageApplication;

        public EmployerPageViewModel EmployerPageViewModel { get; set; }
        public List<ProjectViewModel> Projects { get; set; }

        public EmployerPageProjectsModel(IEmployerPageApplication employerPageApplication, IAuthHelper authHelper, IProjectApplication projectApplication)
        {
            _employerPageApplication = employerPageApplication;
            _authHelper = authHelper;
            _projectApplication = projectApplication;
        }

        public async Task OnGet()
        {
            var employerPageId = await _employerPageApplication.GetEmployerPageIdByAccountIdAsync(_authHelper.CurrentAccountId());
            EmployerPageViewModel = await _employerPageApplication.GetViewModelAsync(employerPageId);
            Projects = await _projectApplication.GetEmployerProjectsAsync(employerPageId);
        }

        public async Task<IActionResult> OnGetClose(long id)
        {
            await _projectApplication.CloseAsync(id);
            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetOpen(long id)
        {
            await _projectApplication.OpenAsync(id);
            return RedirectToPage();
        }
    }
}
