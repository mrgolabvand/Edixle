using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Role
{
    public class EditModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _exposers;
        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
        {
            _roleApplication = roleApplication;
            _exposers = exposers;
        }

        [BindProperty]
        public EditRole Command { get; set; }

        public List<SelectListItem> Permissions = new List<SelectListItem>();

        [NeedsPermission(AccountPermissions.CreateAndEditRole)]
        public async Task OnGet(long id)
        {
            Command = await _roleApplication.GetDetailsAsync(id);
            var permissions = new List<PermissionDto>();

            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var (key, value) in exposedPermissions)
                {
                    permissions.AddRange(value);

                    var gruop = new SelectListGroup {Name = key};
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = gruop
                        };

                        if (Command.MappedPermissions.Any(v => v.Code == permission.Code))
                            item.Selected = true;

                        Permissions.Add(item);
                    }
                }
            }
        }


        [NeedsPermission(AccountPermissions.CreateAndEditRole)]
        public async Task<IActionResult> OnPost()
        {
            await _roleApplication.EditAsync(Command);

            return RedirectToPage("Index");
        }
    }
}
