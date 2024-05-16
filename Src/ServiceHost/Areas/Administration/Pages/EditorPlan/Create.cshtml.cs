using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanManagement.Application.Contracts.EditorPlan;

namespace ServiceHost.Areas.Administration.Pages.EditorPlan
{
    public class CreateModel : PageModel
    {
        private readonly IEditorPlanApplication _editorPlanApplication;

        public CreateModel(IEditorPlanApplication editorPlanApplication)
        {
            _editorPlanApplication = editorPlanApplication;
        }
        [BindProperty] public AddEditorPlan AddEditorPlan { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _editorPlanApplication.CreateAsync(AddEditorPlan);
            return RedirectToPage("./Index");
        }
    }
}
