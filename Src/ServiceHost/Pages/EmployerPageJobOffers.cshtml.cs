using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.JobOffer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class EmployerPageJobOffersModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IJobOfferApplication _jobOfferApplication;
        private readonly IEmployerPageApplication _employerPageApplication;

        public EmployerPageJobOffersModel(IJobOfferApplication jobOfferApplication, IAuthHelper authHelper, IEmployerPageApplication employerPageApplication)
        {
            _jobOfferApplication = jobOfferApplication;
            _authHelper = authHelper;
            _employerPageApplication = employerPageApplication;
        }
        public List<JobOfferViewModel> JobOffers = new();
        public EmployerPageViewModel EmployerPageViewModel { get; set; }
        public async Task OnGet()
        {
            var accountId = _authHelper.CurrentAccountId();
            var pageId = await _employerPageApplication.GetEmployerPageIdByAccountIdAsync(accountId);
            EmployerPageViewModel = await _employerPageApplication.GetViewModelAsync(pageId);
            JobOffers = await _jobOfferApplication.GetEmployerJobOffersAsync(pageId);
        }
    }
}
