using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Application.Contracts.Skill;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AddSkillModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly ISkillApplication _skillApplication;
        private readonly IAccountApplication _accountApplication;
        private readonly IPersonalPageApplication _personalPageApplication;

        [BindProperty] public AddSkill Skill { get; set; }
        public List<SkillViewModel> Skills { get; set; }
        public AddSkillModel(IAuthHelper authHelper, ISkillApplication skillApplication, IPersonalPageApplication personalPageApplication, IAccountApplication accountApplication)
        {
            _authHelper = authHelper;
            _skillApplication = skillApplication;
            _personalPageApplication = personalPageApplication;
            _accountApplication = accountApplication;
        }

        public async Task OnGet()
        {
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            Skills = await _skillApplication.GetListByAsync(pageId);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();
            var accountId = _authHelper.CurrentAccountId();
            if (await _accountApplication.IsEmployerAsync(accountId))
                return RedirectToPage("./Index");
            Skill.PageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());
            await _skillApplication.AddAsync(Skill);
            return RedirectToPage();
        }
    }
}
