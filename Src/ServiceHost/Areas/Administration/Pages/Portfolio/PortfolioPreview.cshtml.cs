using System.Net.Mime;
using System.Threading.Tasks;
using AccountManagement.Application.Contracts.Portfolio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Portfolio
{
    public class PortfolioPreviewModel : PageModel
    {
        private readonly IPortfolioApplication _portfolioApplication;

        public PortfolioPreviewModel(IPortfolioApplication portfolioApplication)
        {
            _portfolioApplication = portfolioApplication;
        }

        public PortfolioViewModel Portfolio { get; set; }

        public async Task OnGet(long id)
        {
            Portfolio = await _portfolioApplication.GetPortfolioAsync(id);
        }

        public async Task<IActionResult> OnGetDownload(long id)
        {
            var portfolio = await _portfolioApplication.GetFileFromPortfolioAsync(id);
            return File($"http://dl.example.com/UploadedFiles/{portfolio.File}", MediaTypeNames.Application.Zip, $"{portfolio.PortfolioName}.zip");

            //return Redirect($"http://dl.persiandevelopers.net/UploadedFiles/{portfolio.File}");
        }
    }
}
