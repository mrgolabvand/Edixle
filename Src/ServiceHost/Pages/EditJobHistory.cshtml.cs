using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.JobHistory;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class EditJobHistoryModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IJobHistoryApplication _jobHistoryApplication;
        private readonly IPersonalPageApplication _personalPageApplication;

        public EditJobHistoryModel(IAuthHelper authHelper, IJobHistoryApplication jobHistoryApplication, IPersonalPageApplication personalPageApplication)
        {
            _authHelper = authHelper;
            _jobHistoryApplication = jobHistoryApplication;
            _personalPageApplication = personalPageApplication;
        }

        [BindProperty] public EditJobHistory JobHistory { get; set; }
        public List<JobHistoryViewModel> JobHistories { get; set; }
        public async Task OnGet(long id)
        {
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            JobHistories =await _jobHistoryApplication.GetListByAsync(pageId);
            var jobHistory = await _jobHistoryApplication.GetDetailsAsync(id);
            JobHistory = new EditJobHistory();
            if (jobHistory.PageId == pageId)
            {
                TempData["pageIdJH"] = jobHistory.PageId.ToString();
                JobHistory = jobHistory;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();
            var pageId = long.Parse(TempData["pageIdJH"].ToString());

            if (pageId == JobHistory.PageId)
            {
                JobHistory.PageId = pageId;
                await _jobHistoryApplication.EditAsync(JobHistory);
            }
            return RedirectToPage("./AddJobHistory");
        }
    }
}
