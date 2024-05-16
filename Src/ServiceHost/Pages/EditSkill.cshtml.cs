using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Application.Contracts.Skill;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class EditSkillModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly ISkillApplication _skillApplication;
        private readonly IPersonalPageApplication _personalPageApplication;

        public EditSkillModel(IAuthHelper authHelper, ISkillApplication skillApplication, IPersonalPageApplication personalPageApplication)
        {
            _authHelper = authHelper;
            _skillApplication = skillApplication;
            _personalPageApplication = personalPageApplication;
        }

        [BindProperty] public EditSkill Skill { get; set; }
        public List<SkillViewModel> Skills { get; set; }
        public async Task OnGet(long id)
        {
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            Skills = await _skillApplication.GetListByAsync(pageId);
            var skill = await _skillApplication.GetDetailsAsync(id);
            Skill = new EditSkill();
            if (skill.PageId == pageId)
            {
                TempData["pageIdS"] = skill.PageId.ToString();
                Skill = skill;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();
            var pageId = long.Parse(TempData["pageIdS"].ToString());
            if (pageId == Skill.PageId)
            {
                Skill.PageId = pageId;
                await _skillApplication.EditAsync(Skill);
            }
            return RedirectToPage("./AddSkill");
        }
    }
}
