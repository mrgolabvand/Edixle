using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Application.Contracts.Portfolio;
using AccountManagement.Application.Contracts.PortfolioAndCategoryLinked;
using AccountManagement.Application.Contracts.PortfolioBaseCategory;
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
    public class AddPortfolioModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IPortfolioQuery _portfolioQuery;
        private readonly IPortfolioApplication _portfolioApplication;
        private readonly IPortfolioBaseCategoryQuery _portfolioBaseCategoryQuery;
        private readonly IPortfolioBaseCategoryApplication _portfolioBaseCategoryApplication;
        private readonly IPortfolioAndCategoryLinkedApplication _portfolioAndCategoryLinkedApplication;
        private readonly IPersonalPageApplication _personalPageApplication;
        private readonly IAccountApplication _accountApplication;
        private readonly IEditorPlanQuery _editorPlanQuery;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHubContext<UploaderHub> _uploaderHubContext;
        private readonly IFileUploader _fileUploader;


        [BindProperty] public CreatePortfolio Portfolio { get; set; }
        public List<PortfolioQueryModel> Portfolios { get; set; }
        public List<PortfolioBaseCategoryQueryModel> BaseCategories { get; set; }
        [BindProperty] public Dictionary<long, long> Categories { get; set; }
        [TempData] public string Message { get; set; }

        public AddPortfolioModel(IPortfolioApplication portfolioApplication, IPortfolioBaseCategoryApplication portfolioBaseCategoryApplication, IPortfolioBaseCategoryQuery portfolioBaseCategoryQuery, IPersonalPageApplication personalPageApplication, IAuthHelper authHelper, IPortfolioQuery portfolioQuery, IPortfolioAndCategoryLinkedApplication portfolioAndCategoryLinkedApplication, IEditorPlanQuery editorPlanQuery, IWebHostEnvironment webHostEnvironment, IFileUploader fileUploader, IHubContext<UploaderHub> uploaderHubContext, IAccountApplication accountApplication)
        {
            _portfolioApplication = portfolioApplication;
            _portfolioBaseCategoryApplication = portfolioBaseCategoryApplication;
            _portfolioBaseCategoryQuery = portfolioBaseCategoryQuery;
            _personalPageApplication = personalPageApplication;
            _authHelper = authHelper;
            _portfolioQuery = portfolioQuery;
            _portfolioAndCategoryLinkedApplication = portfolioAndCategoryLinkedApplication;
            _editorPlanQuery = editorPlanQuery;
            _webHostEnvironment = webHostEnvironment;
            _fileUploader = fileUploader;
            _uploaderHubContext = uploaderHubContext;
            _accountApplication = accountApplication;
        }


        public async Task OnGet()
        {
            BaseCategories = await _portfolioBaseCategoryQuery.GetListAsync();
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            Portfolios = await _portfolioQuery.GetEditorPortfoliosAsync(pageId);
            Categories = new Dictionary<long, long>();
            foreach (var baseCategory in BaseCategories)
            {
                Categories.Add(baseCategory.Id, 0);
            }
        }

        public async Task<IActionResult> OnPost(CreatePortfolio Portfolio, Dictionary<long, long> Categories)
        {

            if (ModelState.IsValid)
            {

                var accountId = _authHelper.CurrentAccountId();
                if (await _accountApplication.IsEmployerAsync(accountId))
                    return RedirectToPage("./Index");
                
                var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(accountId);
                var pagePlan = await _editorPlanQuery.GetEditorPagePlanAsync(pageId);
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
                Portfolio.PageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
                Portfolio.PictureAlt = Portfolio.Name + " نمونه ";
                Portfolio.PictureTitle = Portfolio.Name + " نمونه ";

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

                var (result, videoPath/*, videoPathUWM*/) = await _portfolioApplication.CreateAsync(Portfolio);

                if (!result.IsSucceeded)
                {
                    Message = result.Message;
                    return RedirectToPage();
                }

                //await _fileUploader.SetWatermarkAsync(videoPathUWM, videoPath);

                var categoryIds = Categories.Values.ToList();

                var portfolioId = await _portfolioApplication.GetPortfolioIdByAsync(Portfolio.Slug);

                var addPortfolioAndCategoryList = categoryIds.Select(categoryId => new AddPortfolioAndCategoryLink() { PortfolioId = portfolioId, CategoryId = categoryId }).ToList();

                await _portfolioAndCategoryLinkedApplication.AddAsync(addPortfolioAndCategoryList);
            }

            return RedirectToPage();
        }
    }
}
