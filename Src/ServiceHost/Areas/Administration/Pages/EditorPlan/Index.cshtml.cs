using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Feature;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlanManagement.Application.Contracts.EditorPlan;

namespace ServiceHost.Areas.Administration.Pages.EditorPlan
{
    public class IndexModel : PageModel
    {

        private readonly IEditorPlanApplication _editorPlanApplication;

        public IndexModel(IEditorPlanApplication editorPlanApplication)
        {
            _editorPlanApplication = editorPlanApplication;
        }

        public List<EditorPlanViewModel> EditorPlans { get; set; }

        public async Task OnGet()
        {
            EditorPlans = await _editorPlanApplication.GetListAsync();
        }


        [NeedsPermission(AccountPermissions.CreateAndEditFeature)]
        public async Task<IActionResult> OnGetRemove(long id)
        {
            await _editorPlanApplication.RemoveAsync(id);
            return RedirectToPage("./Index");
        }
        [NeedsPermission(AccountPermissions.CreateAndEditFeature)]
        public async Task<IActionResult> OnGetRestore(long id)
        {
            await _editorPlanApplication.RestoreAsync(id);
            return RedirectToPage("./Index");
        }

    }
}
