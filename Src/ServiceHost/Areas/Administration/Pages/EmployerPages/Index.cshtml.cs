using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.JobOffer;
using AccountManagement.Application.Contracts.Project;
using AccountManagement.Application.Contracts.ProjectOffer;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.EmployerPages
{
    public class IndexModel : PageModel
    {
        private readonly IEmployerPageApplication _employerPageApplication;

        public IndexModel(IEmployerPageApplication reviewApplication)
        {
            _employerPageApplication = reviewApplication;
        }

        [TempData]
        public string Message { get; set; }

        public List<EmployerPageViewModel> EmployerPages { get; set; }


        public async Task OnGet(string title)
        {
            EmployerPages = await _employerPageApplication.SearchAsync(title);
        }

    }
}
