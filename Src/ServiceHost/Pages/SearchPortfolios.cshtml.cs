using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Portfolio;
using EdixleQuery.Contracts.PortfolioCategory;

namespace ServiceHost.Pages
{
    public class SearchPortfoliosModel : PageModel
    {
        private readonly IPortfolioQuery _portfolioQuery;
        private readonly IPortfolioCategoryQuery _portfolioCategoryQuery;

        public SearchPortfoliosModel(IPortfolioQuery portfolioQuery, IPortfolioCategoryQuery portfolioCategoryQuery)
        {
            _portfolioQuery = portfolioQuery;
            _portfolioCategoryQuery = portfolioCategoryQuery;
        }

        public List<PortfolioQueryModel> Portfolios { get; set; }
        public List<PortfolioCategoryQueryModel> PortfolioCategories { get; set; }

        [BindProperty] public Dictionary<long, bool> SelectedCategoriesDictionary { get; set; }

        public async Task OnGet(string query, long orderId = 1, IDictionary<long, bool> selectedCategoriesDictionary = null)
        {
            PortfolioCategories = await _portfolioCategoryQuery.GetCategoriesAsync();
            if (!selectedCategoriesDictionary.Any())
            {
                SelectedCategoriesDictionary = new Dictionary<long, bool>();

                foreach (var category in PortfolioCategories)
                {
                    SelectedCategoriesDictionary.Add(category.Id, false);
                }
            }
            else
            {
                SelectedCategoriesDictionary = (Dictionary<long, bool>)selectedCategoriesDictionary;
            }

            var searchQuery = new PortfolioUserSearchQuery
            {
                Name = query,
                OrderId = orderId,
                CategoriesDictionary = SelectedCategoriesDictionary
            };
            Portfolios =await _portfolioQuery.UserSearchAsync(searchQuery);
        }
    }
}
