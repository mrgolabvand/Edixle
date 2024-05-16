using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.Sms;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.Project;
using AccountManagement.Application.Contracts.ProjectOffer;
using ChatManagement.Application.Contracts.Chat;
using ChatManagement.Application.Contracts.ChatRoom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.ProjectOffer;

namespace ServiceHost.Pages
{
    public class EmployerPageProjectOffersModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IProjectApplication _projectApplication;
        private readonly IAccountApplication _accountApplication;
        private readonly IEmployerPageApplication _employerPageApplication;
        private readonly IProjectOfferQuery _projectOfferQuery;
        private readonly ISmsService _smsService;
        private readonly IProjectOfferApplication _projectOfferApplication;
        private readonly IChatRoomApplication _chatRoomApplication;

        public EmployerPageViewModel EmployerPageViewModel { get; set; }
        public List<ProjectOfferQueryModel> ProjectOffers { get; set; }
        public EmployerPageProjectOffersModel(IAuthHelper authHelper, IProjectApplication projectApplication, IAccountApplication accountApplication, IEmployerPageApplication employerPageApplication, IProjectOfferQuery projectOfferQuery, IProjectOfferApplication projectOfferApplication, IChatRoomApplication chatRoomApplication, ISmsService smsService)
        {
            _authHelper = authHelper;
            _projectApplication = projectApplication;
            _accountApplication = accountApplication;
            _employerPageApplication = employerPageApplication;
            _projectOfferQuery = projectOfferQuery;
            _projectOfferApplication = projectOfferApplication;
            _chatRoomApplication = chatRoomApplication;
            _smsService = smsService;
        }


        public async Task OnGet(long id)
        {
            var employerPageId = await _employerPageApplication.GetEmployerPageIdByAccountIdAsync(_authHelper.CurrentAccountId());
            EmployerPageViewModel = await _employerPageApplication.GetViewModelAsync(employerPageId);

            ProjectOffers = new List<ProjectOfferQueryModel>();

            if (await _projectApplication.IsEmployerOwnerOfProject(employerPageId, id))
                ProjectOffers = await _projectOfferQuery.GetProjectOffersAsync(id);
        }

        public async Task<IActionResult> OnGetAcceptOffer(long offerId, long projectId, long senderId)
        {
            var AddChatRoom = new AddChatRoom();
            var project = await _projectApplication.GetDetailsAsync(projectId);
            var offer = await _projectOfferApplication.GetOfferAsync(offerId);
            AddChatRoom.SenderId = senderId;
            AddChatRoom.ReceiverId = _authHelper.CurrentAccountId();
            AddChatRoom.Title = project.Title;
            AddChatRoom.Price = offer.DoublePrice;
            await _chatRoomApplication.AddChatRoomAsync(AddChatRoom);
            await _projectOfferApplication.AcceptAsync(offerId);
            await _projectApplication.CloseAsync(projectId);
            var number =  await _accountApplication.GetPhoneNumber(senderId);
            _smsService.Send_YourProjectOfferIsAccepted_ToEditor_MeliPayamak_Com(number);
            return RedirectToPage(routeValues: projectId);
        }
    }
}
