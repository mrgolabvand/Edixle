using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Feature;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Feature
{
    public class IndexModel : PageModel
    {

        private readonly IFeatureApplication _featureApplication;

        public IndexModel(IFeatureApplication featureApplication)
        {
            _featureApplication = featureApplication;
        }

        public List<FeatureViewModel> Features { get; set; }

        public async Task OnGet(AccountSearchModel searchModel)
        {
            Features = await _featureApplication.GetListAsync();
        }

        [NeedsPermission(AccountPermissions.CreateAndEditFeature)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }

        [NeedsPermission(AccountPermissions.CreateAndEditFeature)]
        public async Task<IActionResult> OnPostCreate(CreateFeature command)
        {
            var result = await _featureApplication.CreateAsync(command);
            return new JsonResult(result);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditFeature)]
        public async Task<IActionResult> OnGetEdit(long id)
        {
            var account = await _featureApplication.GetDetailsAsync(id);
            return Partial("./Edit", account);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditFeature)]
        public async Task<IActionResult> OnPostEdit(EditFeature command)
        {
            var result = await _featureApplication.EditAsync(command);
            return new JsonResult(result);

        }

        [NeedsPermission(AccountPermissions.CreateAndEditFeature)]
        public async Task<IActionResult> OnGetRemove(long id)
        {
            await _featureApplication.RemoveAsync(id);
            return RedirectToPage("./Index");
        }
        [NeedsPermission(AccountPermissions.CreateAndEditFeature)]
        public async Task<IActionResult> OnGetRestore(long id)
        {
            await _featureApplication.RestoreAsync(id);
            return RedirectToPage("./Index");
        }

    }
}
