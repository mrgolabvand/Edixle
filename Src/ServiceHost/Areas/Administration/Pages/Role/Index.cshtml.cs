using System.Collections.Generic;
using System.Threading.Tasks;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Role
{
    public class IndexModel : PageModel
    {

        private readonly IRoleApplication _roleApplication;

        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        [TempData]
        public string Message { get; set; }

        public List<RoleViewModel> Roles { get; set; }

        public async Task OnGet()
        {
            Roles = await _roleApplication.GetListAsync();
        }
    }
}
