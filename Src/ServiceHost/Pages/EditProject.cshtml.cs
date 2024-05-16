using System.Globalization;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class EditProjectModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IProjectApplication _projectApplication;
        private readonly IEmployerPageApplication _employerPageApplication;
        public EditProjectModel(IProjectApplication projectApplication, IAuthHelper authHelper, IEmployerPageApplication employerPageApplication)
        {
            _projectApplication = projectApplication;
            _authHelper = authHelper;
            _employerPageApplication = employerPageApplication;
        }

        [BindProperty]
        public EditProject Project { get; set; }

        public async Task OnGet(long id)
        {
            var pageId = 
                await _employerPageApplication.GetEmployerPageIdByAccountIdAsync(_authHelper.CurrentAccountId());

            var project = await _projectApplication.GetDetailsAsync(id);
            Project = new EditProject();

            if (project.EmployerPageId == pageId)
            {
                TempData["employerPageIdP"] = project.EmployerPageId.ToString();
                Project = project;
            }
        }
        public async Task<IActionResult> OnPost()
        {
            var result = new OperationResult();

            var pageId = long.Parse(TempData["employerPageIdP"].ToString());

            if (pageId == Project.EmployerPageId)
            {
                Project.EmployerPageId = pageId;
                result = await _projectApplication.EditAsync(Project);
            }
            if (result.IsSucceeded)
                return RedirectToPage("/EmployerPageProjects");

            return RedirectToPage();
        }
    }
}
