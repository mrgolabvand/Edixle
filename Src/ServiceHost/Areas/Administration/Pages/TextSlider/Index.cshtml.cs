using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.TextSlider;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.TextSlider
{
    public class IndexModel : PageModel
    {

        private readonly ITextSliderApplication _textSliderApplication;

        public IndexModel(ITextSliderApplication textSliderApplication)
        {
            _textSliderApplication = textSliderApplication;
        }

        public List<TextSliderViewModel> TextSliders { get; set; }

        [NeedsPermission(AccountPermissions.CreateAndEditSlider)]
        public async Task OnGet(TextSliderSearchModel searchModel)
        {
            TextSliders = await _textSliderApplication.SearchAsync(searchModel);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditSlider)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }

        [NeedsPermission(AccountPermissions.CreateAndEditSlider)]
        public async Task<IActionResult> OnPostCreate(CreateTextSlider command)
        {
            var result = await _textSliderApplication.CreateAsync(command);
            return new JsonResult(result);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditSlider)]
        public async Task<IActionResult> OnGetEdit(long id)
        {
            var account = await _textSliderApplication.GetDetailsAsync(id);
            return Partial("./Edit", account);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditSlider)]
        public async Task<IActionResult> OnPostEdit(EditTextSlider command)
        {
            var result = await _textSliderApplication.EditAsync(command);
            return new JsonResult(result);

        }

        [NeedsPermission(AccountPermissions.CreateAndEditSlider)]
        public async Task<IActionResult> OnGetRemove(long id)
        {
            await _textSliderApplication.RemoveAsync(id);
            return RedirectToPage("./Index");
        }
        [NeedsPermission(AccountPermissions.CreateAndEditSlider)]
        public async Task<IActionResult> OnGetRestore(long id)
        {
            await _textSliderApplication.RestoreAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
