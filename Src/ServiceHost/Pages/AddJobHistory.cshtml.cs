using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.JobHistory;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AddJobHistoryModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IJobHistoryApplication _jobHistoryApplication;
        private readonly IPersonalPageApplication _personalPageApplication;
        [BindProperty] public AddJobHistory JobHistory { get; set; }
        public List<JobHistoryViewModel> JobHistories { get; set; }
        public AddJobHistoryModel(IJobHistoryApplication jobHistoryApplication, IPersonalPageApplication personalPageApplication, IAuthHelper authHelper)
        {
            _jobHistoryApplication = jobHistoryApplication;
            _personalPageApplication = personalPageApplication;
            _authHelper = authHelper;
        }

        public async Task OnGet()
        {
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            JobHistories = await _jobHistoryApplication.GetListByAsync(pageId);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();
            JobHistory.PageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            await _jobHistoryApplication.AddAsync(JobHistory);
            return RedirectToPage();
        }
    }
}
