using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Project;

namespace ServiceHost.Pages
{
    public class SearchProjectsModel : PageModel
    {
        private readonly IProjectQuery _projectQuery;

        public List<ProjectQueryModel> Projects { get; set; }

        public SearchProjectsModel(IProjectQuery projectQuery)
        {
            _projectQuery = projectQuery;
        }

        public async Task OnGet(string searchWord, int minPay = 0, int maxPay = 10000000, int orderId = 1, string isOpen = null)
        {
            var IsOpen = false || !string.IsNullOrWhiteSpace(isOpen) && isOpen == "on";

            var searchModel = new ProjectQuerySearchModel
            {
                OrderId = orderId,
                MaxPay = maxPay,
                MinPay = minPay,
                SearchWord = searchWord,
                IsOpen = IsOpen
            };
            Projects =await _projectQuery.SearchProjectsAsync(searchModel);
        }
    }
}
