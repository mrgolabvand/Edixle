using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using EdixleQuery.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using EdixleQuery.Contracts.Article;
using EdixleQuery.Contracts.Feature;
using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.Portfolio;
using EdixleQuery.Contracts.PortfolioCategory;
using EdixleQuery.Contracts.Project;

namespace ServiceHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFeatureQuery _featureQuery;
        private readonly IProjectQuery _projectQuery;
        private readonly IPortfolioQuery _portfolioQuery;
        private readonly IPortfolioCategoryQuery _portfolioCategoryQuery;
        private readonly IArticleQuery _articleQuery;
        private readonly IPersonalPageQuery _personalPageQuery;
        private readonly IAccountQuery _accountQuery;
        public IndexModel(IPortfolioQuery portfolioQuery, IPortfolioCategoryQuery portfolioCategoryQuery, IFeatureQuery featureQuery, IProjectQuery projectQuery, IArticleQuery articleQuery, IPersonalPageQuery personalPageQuery, IAccountQuery accountQuery)
        {
            _portfolioQuery = portfolioQuery;
            _portfolioCategoryQuery = portfolioCategoryQuery;
            _featureQuery = featureQuery;
            _projectQuery = projectQuery;
            _articleQuery = articleQuery;
            _personalPageQuery = personalPageQuery;
            _accountQuery = accountQuery;
        }

        public List<PortfolioQueryModel> Portfolios { get; set; }
        public List<PortfolioCategoryQueryModel> PortfolioCategories { get; set; }
        public List<FeatureQueryModel> FeatureQueryModels { get; set; }
        public List<ProjectQueryModel> LatestProjects { get; set; }
        public List<ArticleQueryModel> Articles { get; set; }
        public List<PersonalPageQueryModel> EditorPages { get; set; }
        public ProjectsCountViewModel ProjectsCount { get; set; }
        public PersonalPagesCountViewModel EditorsCount { get; set; }
        [BindProperty] public Dictionary<long, bool> SelectedCategoriesDictionary { get; set; }

        public async Task OnGet()
        {
            Portfolios = await _portfolioQuery.GetTopPortfoliosAsync();
            PortfolioCategories = await _portfolioCategoryQuery.GetCategoriesAsync();

            SelectedCategoriesDictionary = new Dictionary<long, bool>();

            foreach (var category in PortfolioCategories)
            {
                SelectedCategoriesDictionary.Add(category.Id, false);
            }

            ProjectsCount = await _projectQuery.GetProjectsCountAsync();
            EditorsCount = await _personalPageQuery.GetPersonalPagesCountAsync();
            EditorPages = await _personalPageQuery.GetBestEditorsAsync();
            FeatureQueryModels = await _featureQuery.GetFeaturesAsync();
            LatestProjects = await _projectQuery.GetLatestProjectsAsync();
            Articles = await _articleQuery.GetLatestArticlesAsync();

        }

    }
}
