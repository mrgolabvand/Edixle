using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Application.Contracts.Portfolio;
using AccountManagement.Application.Contracts.PortfolioAndCategoryLinked;
using AccountManagement.Application.Contracts.PortfolioBaseCategory;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using EdixleQuery.Contracts.Plan;
using EdixleQuery.Contracts.Portfolio;
using EdixleQuery.Contracts.PortfolioBaseCategory;
using ServiceHost.Hubs;

namespace ServiceHost.Pages
{
    [RequestSizeLimit(2147483648)]
    [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
    public class EditPortfolioModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IPortfolioQuery _portfolioQuery;
        private readonly IPortfolioApplication _portfolioApplication;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileUploader _fileUploader;
        private readonly IFileHostUploader _fileHostUploader;
        private readonly IPortfolioBaseCategoryQuery _portfolioBaseCategoryQuery;
        private readonly IPortfolioBaseCategoryApplication _portfolioBaseCategoryApplication;
        private readonly IPortfolioAndCategoryLinkedApplication _portfolioAndCategoryLinkedApplication;
        private readonly IPersonalPageApplication _personalPageApplication;
        private readonly IHubContext<UploaderHub> _uploaderHubContext;
        private readonly IEditorPlanQuery _editorPlanQuery;

        [BindProperty] public EditPortfolio Portfolio { get; set; }
        public List<PortfolioQueryModel> Portfolios { get; set; }
        public List<PortfolioBaseCategoryQueryModel> BaseCategories { get; set; }
        [TempData] public string Message { get; set; }

        public EditPortfolioModel(IAuthHelper authHelper, IPortfolioQuery portfolioQuery, IPortfolioApplication portfolioApplication, IPortfolioBaseCategoryQuery portfolioBaseCategoryQuery, IPortfolioBaseCategoryApplication portfolioBaseCategoryApplication, IPersonalPageApplication personalPageApplication, IPortfolioAndCategoryLinkedApplication portfolioAndCategoryLinkedApplication, IWebHostEnvironment webHostEnvironment, IFileUploader fileUploader, IHubContext<UploaderHub> uploaderHubContext, IEditorPlanQuery editorPlanQuery, IFileHostUploader fileHostUploader)
        {
            _authHelper = authHelper;
            _portfolioQuery = portfolioQuery;
            _portfolioApplication = portfolioApplication;
            _portfolioBaseCategoryQuery = portfolioBaseCategoryQuery;
            _portfolioBaseCategoryApplication = portfolioBaseCategoryApplication;
            _personalPageApplication = personalPageApplication;
            _portfolioAndCategoryLinkedApplication = portfolioAndCategoryLinkedApplication;
            _webHostEnvironment = webHostEnvironment;
            _fileUploader = fileUploader;
            _uploaderHubContext = uploaderHubContext;
            _editorPlanQuery = editorPlanQuery;
            _fileHostUploader = fileHostUploader;
        }

        public async Task OnGet(long id)
        {
            BaseCategories = await _portfolioBaseCategoryQuery.GetListAsync();
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            Portfolios = await _portfolioQuery.GetEditorPortfoliosAsync(pageId);
            var portfolio = await _portfolioApplication.GetDetailsAsync(id);
            Portfolio = new EditPortfolio();
            if (portfolio.PageId == pageId)
            {
                TempData["pageIdP"] = portfolio.PageId.ToString();
                Portfolio = portfolio;
            }
        }

        public async Task<IActionResult> OnPost(EditPortfolio Portfolio)
        {
            if (ModelState.IsValid)
            {

                var pageId = long.Parse(TempData["pageIdP"].ToString());
                var pagePlan = await _editorPlanQuery.GetEditorPagePlanAsync(pageId);


                if (pageId == Portfolio.PageId)
                {
                    Portfolio.PageId = pageId;
                    Portfolio.Slug = Portfolio.Name.Slugify();
                    if (!string.IsNullOrWhiteSpace(Portfolio.Description))
                    {
                        Portfolio.ShortDescription =
                            Portfolio.Description.Substring(0, Math.Min(Portfolio.Description.Length, 30));
                        Portfolio.MetaDescription =
                            Portfolio.Description.Substring(0, Math.Min(Portfolio.Description.Length, 20)) + " نمونه ";
                    }
                    else
                    {
                        Portfolio.MetaDescription = "example.com";
                        Portfolio.ShortDescription = Portfolio.Name;

                    }

                    if (!string.IsNullOrWhiteSpace(Portfolio.Tags))
                    {
                        Portfolio.Keywords = Portfolio.Name + Portfolio.Tags;
                    }
                    else
                    {
                        Portfolio.Keywords = Portfolio.Name;
                    }

                    if (string.IsNullOrWhiteSpace(Portfolio.Tags))
                    {
                        Portfolio.Tags = Portfolio.Name;
                    }
                    Portfolio.PictureAlt = Portfolio.Name + "ادیتور لند";
                    Portfolio.PictureTitle = Portfolio.Name + "ادیتور لند";
                    var videoPath = string.Empty;
                    var videoPathUWM = string.Empty;

                    if (Portfolio.Video != null)
                    {
                        if (pagePlan.IsPlanActive)
                        {
                            if (Portfolio.Video.Length > pagePlan.PortfolioUploadSizeLimit)
                            {
                                Portfolio.Video = null;
                            }
                        }
                        else
                        {
                            if (Portfolio.Video.Length > (200 * 1024 * 1024))
                            {
                                Portfolio.Video = null;
                            }
                        }
                    }

                    (var result, videoPath/*, videoPathUWM*/) = await _portfolioApplication.EditAsync(Portfolio);

                    if (!result.IsSucceeded)
                    {
                        Message = result.Message;
                    }
                    //if (Portfolio.Video != null)
                    //{
                    //var userId = _authHelper.CurrentAccountId().ToString();

                    //var isSuccess = false;
                    //await _uploaderHubContext.Clients.User(userId).SendAsync("ServerVideoProcess", isSuccess);
                    //isSuccess = await _fileUploader.SetWatermarkAsync(videoPathUWM, videoPath);
                    //isSuccess = await _fileHostUploader.SetWatermarkAsync(videoPathUWM, videoPath);
                    //await _uploaderHubContext.Clients.User(userId).SendAsync("ServerVideoProcess", isSuccess);
                    //}

                }
            }

            return RedirectToPage("./AddPortfolio");
        }

    }
}
