using System.Threading.Tasks;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.PersonalPage;

namespace ServiceHost.Pages
{
    public class EditorPagePortfoliosModel : PageModel
    {
        private readonly IPersonalPageQuery _personalPageQuery;
        private readonly IPersonalPageApplication _personalPageApplication;

        public EditorPagePortfoliosModel(IPersonalPageQuery personalPageQuery, IPersonalPageApplication personalPageApplication)
        {
            _personalPageQuery = personalPageQuery;
            _personalPageApplication = personalPageApplication;
        }

        public PersonalPageQueryModel PersonalPage = new();
        public async Task OnGet(string id)
        {
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(id);
            PersonalPage = await _personalPageQuery.GetPageAsync(id, pageId);
        }
    }
}
