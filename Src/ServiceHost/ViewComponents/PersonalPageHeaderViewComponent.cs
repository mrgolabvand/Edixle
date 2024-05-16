using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using EdixleQuery.Contracts.PersonalPage;

namespace ServiceHost.ViewComponents
{
    public class PersonalPageHeaderViewComponent : ViewComponent
    {

        private readonly IAuthHelper _authHelper;
        private readonly IPersonalPageQuery _personalPageQuery;
        private readonly IPersonalPageApplication _personalPageApplication;
        public PersonalPageHeaderViewComponent(IPersonalPageQuery personalPageQuery, IAuthHelper authHelper, IPersonalPageApplication personalPageApplication)
        {
            _personalPageQuery = personalPageQuery;
            _authHelper = authHelper;
            _personalPageApplication = personalPageApplication;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {

            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            var page = await _personalPageQuery.GetPageAsync(pageId);
            return View("PersonalPageHeader", page);
        }
    }
}
