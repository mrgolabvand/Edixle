using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Application.Contracts.PortfolioDownload;
using AccountManagement.Domain.PortfolioDownloadAgg;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Comment;
using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.Portfolio;
using EdixleQuery.Contracts.PortfolioCategory;
using ReviewManagement.Application.Contracts.Review;
using ReviewManagement.Infrastructure.EFCore.Migrations;

namespace ServiceHost.Pages
{
    public class PortfolioDetailsModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IPortfolioQuery _portfolioQuery;
        private readonly ICommentApplication _commentApplication;
        private readonly IReviewApplication _reviewApplication;
        private readonly IPortfolioCategoryQuery _portfolioCategoryQuery;
        private readonly IPersonalPageQuery _personalPageQuery;
        private readonly IPortfolioDownloadApplication _portfolioDownloadApplication;

        public PortfolioDetailsModel(IPortfolioQuery portfolioQuery, IPortfolioCategoryQuery portfolioCategoryQuery, ICommentApplication commentApplication, IPortfolioDownloadApplication portfolioDownloadApplication, IAuthHelper authHelper, IReviewApplication reviewApplication, IPersonalPageQuery personalPageQuery)
        {
            _portfolioQuery = portfolioQuery;
            _portfolioCategoryQuery = portfolioCategoryQuery;
            _commentApplication = commentApplication;
            _portfolioDownloadApplication = portfolioDownloadApplication;
            _authHelper = authHelper;
            _reviewApplication = reviewApplication;
            _personalPageQuery = personalPageQuery;
        }

        public PortfolioQueryModel Portfolio = new();
        public PersonalPageQueryModel PersonalPage = new();
        public List<PortfolioQueryModel> LatestPortfolios = new();
        public List<PortfolioQueryModel> RelatedPortfolios = new();
        public List<PortfolioCategoryQueryModel> Categories = new();
        [BindProperty] public AddComment AddComment { get; set; }
        [BindProperty] public CreateReview AddReview { get; set; }
        public string DownloadCount { get; set; }

        public async Task OnGet(string id)
        {
            Portfolio = await _portfolioQuery.GetPortfolioByAsync(id);
            PersonalPage = await _personalPageQuery.GetPageAsync(Portfolio.PageId);
            Categories = await _portfolioCategoryQuery.GetCategoriesAsync();
            LatestPortfolios = await _portfolioQuery.GetTopPortfoliosAsync();
            if (Portfolio != null)
                RelatedPortfolios = await _portfolioQuery.GetRelatedPortfoliosAsync(Portfolio.Tags);
            //var portfolioId = _portfolioQuery.GetPortfolioBy(id).Id;
            //DownloadCount = _portfolioDownloadApplication.GetDownloadCount(portfolioId).ToMoney();
        }

        public async Task<IActionResult> OnGetDownload(long id)
        {

            var portfolioDownload = new IncreasePortfolioDownload
            {
                AccountId = _authHelper.CurrentAccountId(),
                PortfolioId = id
            };
            await _portfolioDownloadApplication.IncreaseAsync(portfolioDownload);

            var portfolio = await _portfolioQuery.GetFileFromPortfolioAsync(id);

            return File($"http://dl.example.com/UploadedFiles/{portfolio.Video}", MediaTypeNames.Application.Zip, $"persiandevelopers.net_{portfolio.AccountName}_{portfolio.Name}.zip");
            //return Redirect($"http://dl.persiandevelopers.net/UploadedFiles/{portfolio.File}");
        }

        public async Task<IActionResult> OnPostComment()
        {
            AddComment.Type = CommentType.Portfolio;
            await _commentApplication.AddAsync(AddComment);
            return RedirectToPage("./PortfolioDetails");
        }

        public async Task<IActionResult> OnPostReview(string id)
        {
            AddReview.Name = _authHelper.CurrentAccountInfo().UserName;
            await _reviewApplication.CreateAsync(AddReview);
            return RedirectToPage(pageName: "./PortfolioDetails", routeValues: id);
        }

        //public IActionResult OnGetReviewIsHelpful(long reviewId, long portfolioId, string slug)
        //{
        //    if (_authHelper.IsAuthenticated())
        //    {
        //        var userName = _authHelper.CurrentAccountInfo().UserName;
        //        _reviewApplication.IsHelpful(reviewId, portfolioId, userName);
        //    }
        //    return RedirectToPage(routeValues: slug);
        //}
        //public IActionResult OnGetReviewIsHarmful(long reviewId, long portfolioId, string slug)
        //{
        //    if (_authHelper.IsAuthenticated())
        //    {
        //        var userName = _authHelper.CurrentAccountInfo().UserName;
        //        _reviewApplication.IsHarmful(reviewId, portfolioId, userName);
        //    }
        //    return RedirectToPage(routeValues: slug);
        //}

    }
}
