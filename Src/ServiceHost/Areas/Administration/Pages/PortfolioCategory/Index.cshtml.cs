using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PortfolioBaseCategory;
using AccountManagement.Application.Contracts.PortfolioCategory;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.PortfolioCategory
{
    public class IndexModel : PageModel
    {

        private readonly IPortfolioCategoryApplication _portfolioCategoryApplication;
        private readonly IPortfolioBaseCategoryApplication _portfolioBaseCategoryApplication;
        public IndexModel(IPortfolioCategoryApplication portfolioCategoryApplication, IPortfolioBaseCategoryApplication portfolioBaseCategoryApplication)
        {
            _portfolioCategoryApplication = portfolioCategoryApplication;
            _portfolioBaseCategoryApplication = portfolioBaseCategoryApplication;
        }

        public List<PortfolioCategoryViewModel> Categories { get; set; }

        public async Task OnGet(PortfolioCategorySearchModel searchModel)
        {
            Categories = await _portfolioCategoryApplication.SearchAsync(searchModel);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditCategory)]
        public async Task<IActionResult> OnGetCreate()
        {
            return Partial("./Create", new CreatePortfolioCategory { BaseCateogries = await _portfolioBaseCategoryApplication.GetListAsync()});
        }

        [NeedsPermission(AccountPermissions.CreateAndEditCategory)]
        public async Task<IActionResult> OnPostCreate(CreatePortfolioCategory command)
        {
            var result = await _portfolioCategoryApplication.CreateAsync(command);
            return new JsonResult(result);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditCategory)]
        public async Task<IActionResult> OnGetEdit(long id)
        {
            var account = await _portfolioCategoryApplication.GetDetailsAsync(id);
            account.BaseCateogries = await _portfolioBaseCategoryApplication.GetListAsync();
            return Partial("./Edit", account);
        }

        [NeedsPermission(AccountPermissions.CreateAndEditCategory)]
        public async Task<IActionResult> OnPostEdit(EditPortfolioCategory command)
        {
            var result = await _portfolioCategoryApplication.EditAsync(command);
            return new JsonResult(result);

        }

    }
}
