using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Portfolio;
using AccountManagement.Application.Contracts.PortfolioDownload;
using AccountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Portfolio
{
    public class IndexModel : PageModel
    {
        private readonly IPortfolioApplication _portfolioApplication;
        private readonly IPortfolioDownloadApplication _portfolioDownloadApplication;
        public IndexModel(IPortfolioApplication portfolioApplication, IPortfolioDownloadApplication portfolioDownloadApplication)
        {
            _portfolioApplication = portfolioApplication;
            _portfolioDownloadApplication = portfolioDownloadApplication;
        }

        public List<PortfolioViewModel> Portfolio { get; set; }
        public PortfolioSearchModel SearchModel { get; set; }
        public double DownloadCount { get; set; }

        public async Task OnGet()
        {
            Portfolio = await _portfolioApplication.SearchAsync(SearchModel);
        }


        public async Task<IActionResult> OnGetEdit(long id)
        {
            var account = await _portfolioApplication.GetDetailsAsync(id);
            return Partial("./Edit", account);
        }


        public async Task<IActionResult> OnPostEdit(EditPortfolio command)
        {
            var result = await _portfolioApplication.EditAsync(command);
            return new JsonResult(result);

        }

        [NeedsPermission(AccountPermissions.ConfirmAndCancelPortfolio)]
        public async Task<IActionResult> OnGetConfirm(long id)
        {
            await _portfolioApplication.ConfirmAsync(id);
            return RedirectToPage("./Index");
        }

        [NeedsPermission(AccountPermissions.ConfirmAndCancelPortfolio)]
        public async Task<IActionResult> OnGetCancel(long id)
        {
            await _portfolioApplication.CancelAsync(id);
            return RedirectToPage("./Index");
        }

    }
}
