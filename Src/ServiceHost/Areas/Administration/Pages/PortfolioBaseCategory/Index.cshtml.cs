using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PortfolioBaseCategory;
using AccountManagement.Application.Contracts.PortfolioCategory;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.PortfolioBaseCategory
{
    public class IndexModel : PageModel
    {

        private readonly IPortfolioBaseCategoryApplication _portfolioBaseCategoryApplication;

        public IndexModel(IPortfolioBaseCategoryApplication portfolioBaseCategoryApplication)
        {
            _portfolioBaseCategoryApplication = portfolioBaseCategoryApplication;
        }

        public List<PortfolioBaseCategoryViewModel> Categories { get; set; }

        public async Task OnGet(PortfolioBaseCategorySearchModel searchModel)
        {
            Categories = await _portfolioBaseCategoryApplication.SearchAsync(searchModel);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditCategory)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }

        [NeedsPermission(AccountPermissions.CreateAndEditCategory)]
        public async Task<IActionResult> OnPostCreate(CreatePortfolioBaseCategory command)
        {
            var result = await _portfolioBaseCategoryApplication.CreateAsync(command);
            return new JsonResult(result);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditCategory)]
        public async Task<IActionResult> OnGetEdit(long id)
        {
            var account = await _portfolioBaseCategoryApplication.GetDetailsAsync(id);
            return Partial("./Edit", account);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditCategory)]
        public async Task<IActionResult> OnPostEdit(EditPortfolioBaseCategory command)
        {
            var result = await _portfolioBaseCategoryApplication.EditAsync(command);
            return new JsonResult(result);

        }

    }
}
