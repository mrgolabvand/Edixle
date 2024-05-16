using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.PersonalPages
{
    public class PersonalPagePreviewModel : PageModel
    {
        private readonly IPersonalPageApplication _personalPageApplication;

        public PersonalPagePreviewModel(IPersonalPageApplication personalPageApplication)
        {
            _personalPageApplication = personalPageApplication;
        }

        public PersonalPageViewModel PersonalPage { get; set; }

        public async Task OnGet(long id)
        {
            PersonalPage = await _personalPageApplication.GetPageAsync(id);
        }
    }
}
