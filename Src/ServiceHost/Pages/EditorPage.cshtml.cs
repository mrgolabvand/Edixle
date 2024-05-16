using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.Skill;

namespace ServiceHost.Pages
{
    public class EditorPageModel : PageModel
    {
        private readonly IPersonalPageQuery _personalPageQuery;
        private readonly IPersonalPageApplication _personalPageApplication;

        public EditorPageModel(IPersonalPageQuery personalPageQuery, IPersonalPageApplication personalPageApplication)
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
