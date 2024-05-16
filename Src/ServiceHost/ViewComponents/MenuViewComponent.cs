using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using EdixleQuery.Contracts.Category;
using EdixleQuery.Contracts.EmployerPage;
using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.PortfolioCategory;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IAuthHelper _authHelper;
        private readonly ICategoryQuery _categoryQuery;
        private readonly IEmployerPageQuery _employerPageQuery;
        private readonly IPersonalPageQuery _personalPageQuery;
        private readonly IAccountApplication _accountApplication;
        private readonly IEmployerPageApplication _employerPageApplication;
        private readonly IPersonalPageApplication _personalPageApplication;
        public MenuViewComponent(ICategoryQuery categoryQuery, IAuthHelper authHelper, IAccountApplication accountApplication, IPersonalPageQuery personalPageQuery, IPersonalPageApplication personalPageApplication, IEmployerPageQuery employerPageQuery, IEmployerPageApplication employerPageApplication)
        {
            _categoryQuery = categoryQuery;
            _authHelper = authHelper;
            _accountApplication = accountApplication;
            _personalPageQuery = personalPageQuery;
            _personalPageApplication = personalPageApplication;
            _employerPageQuery = employerPageQuery;
            _employerPageApplication = employerPageApplication;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var accountId = _authHelper.CurrentAccountId();
            var categories = await _categoryQuery.GetCategoriesAsync();
            categories.BaseCategories = await _categoryQuery.GetBaseCategoriesAsync();
            categories.IsEmployer = await _accountApplication.IsEmployerAsync(accountId);
            categories.IsEditor = await _accountApplication.IsEditorAsync(accountId);
            categories.IsAuth = _authHelper.IsAuthenticated();
            categories.AccountId = accountId;
            var personalPageId = await _personalPageApplication.GetPersonalPageIdByAsync(accountId);
            var employerPageId = await _employerPageApplication.GetEmployerPageIdByAccountIdAsync(accountId);
            if (personalPageId != 0)
            {
                categories.PersonalPage = await _personalPageQuery.GetPageAsync(personalPageId);
            }

            if (employerPageId != 0)
            {
                categories.EmployerPage = await _employerPageQuery.GetPageAsync(employerPageId);
            }
            return View("Menu", categories);
        }
    }
}
