using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.JobHistory;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Application.Contracts.Skill;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ResumeReviewModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly ISkillApplication _skillApplication;
        private readonly IJobHistoryApplication _jobHistoryApplication;
        private readonly IPersonalPageApplication _personalPageApplication;

        [BindProperty]
        [FileExtensionLimitation(new string[] { ".png", ".jpg", ".jpeg" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.MaxFileSize)]
        public IFormFile Picture { get; set; }
        public EditPersonalPage PersonalPage { get; set; }
        public List<JobHistoryViewModel> JobHistories { get; set; }
        public List<SkillViewModel> Skills { get; set; }

        public ResumeReviewModel(IAuthHelper authHelper, ISkillApplication skillApplication, IJobHistoryApplication jobHistoryApplication, IPersonalPageApplication personalPageApplication)
        {
            _authHelper = authHelper;
            _skillApplication = skillApplication;
            _jobHistoryApplication = jobHistoryApplication;
            _personalPageApplication = personalPageApplication;
        }

        public async Task OnGet()
        {
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            PersonalPage = await _personalPageApplication.GetDetailsAsync(pageId);
            JobHistories = await _jobHistoryApplication.GetListByAsync(pageId);
            Skills = await _skillApplication.GetListByAsync(pageId);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            PersonalPage = await _personalPageApplication.GetDetailsAsync(pageId);
            PersonalPage.PictureAlt = PersonalPage.FullName;
            PersonalPage.PictureTitle = PersonalPage.FullName;
            PersonalPage.Picture = Picture;
            await _personalPageApplication.EditAsync(PersonalPage);
            return RedirectToPage();
        }
    }
}
