using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanManagement.Application.Contracts.EditorPlan;

namespace ServiceHost.Areas.Administration.Pages.EditorPlan
{
    public class EditModel : PageModel
    {
        private readonly IEditorPlanApplication _editorPlanApplication;

        public EditModel(IEditorPlanApplication editorPlanApplication)
        {
            _editorPlanApplication = editorPlanApplication;
        }
        [BindProperty] public EditEditorPlan EditEditorPlan { get; set; }
        public async Task OnGet(long id)
        {
            EditEditorPlan = await _editorPlanApplication.GetDetailsAsync(id);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditFeature)]
        public async Task<IActionResult> OnPost()
        {
            var result = await _editorPlanApplication.EditAsync(EditEditorPlan);
            return RedirectToPage("./Index");
        }
    }
}
