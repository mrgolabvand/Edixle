using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.PersonalPage;

namespace ServiceHost.Pages
{
    public class SearchEditorsModel : PageModel
    {
        private readonly IPersonalPageQuery _personalPageQuery;

        public List<PersonalPageQueryModel> PersonalPages { get; set; }
        public SearchEditorsModel(IPersonalPageQuery personalPageQuery)
        {
            _personalPageQuery = personalPageQuery;
        }

        public async Task OnGet(string searchWord, int orderId = 1, int minPay = 1000, int maxPay = 10000000, string isBusy = null, string payDate = null)
        {
            var IsBusy = false || !string.IsNullOrWhiteSpace(isBusy) && isBusy == "On";
            PersonalPages = await _personalPageQuery.SearchAsync(searchWord, orderId, minPay, maxPay, IsBusy, payDate);
        }
    }
}
