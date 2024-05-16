using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.Sms;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.JobOffer;
using AccountManagement.Application.Contracts.PersonalPage;
using ChatManagement.Application.Contracts.ChatRoom;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Account;

namespace ServiceHost.Pages
{
    public class EditorProfileJobOffersModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IAccountQuery _accountQuery;
        private readonly ISmsService _smsService;
        private readonly IChatRoomApplication _chatRoomApplication;
        private readonly IAccountApplication _accountApplication;
        private readonly IJobOfferApplication _jobOfferApplication;
        private readonly IPersonalPageApplication _personalPageApplication;

        public EditorProfileJobOffersModel(IJobOfferApplication jobOfferApplication, IAuthHelper authHelper, IPersonalPageApplication personalPageApplication, IChatRoomApplication chatRoomApplication, IAccountQuery accountQuery, ISmsService smsService, IAccountApplication accountApplication)
        {
            _jobOfferApplication = jobOfferApplication;
            _authHelper = authHelper;
            _personalPageApplication = personalPageApplication;
            _chatRoomApplication = chatRoomApplication;
            _accountQuery = accountQuery;
            _smsService = smsService;
            _accountApplication = accountApplication;
        }

        public List<JobOfferViewModel> JobOffers = new ();
        public PersonalPageViewModel PersonalPageViewModel { get; set; }
        public AccountQueryModel AccountQueryModel { get; set; }
        public async Task OnGet()
        {
            var accountId = _authHelper.CurrentAccountId();
            var pageId =await _personalPageApplication.GetPersonalPageIdByAsync(accountId);
            PersonalPageViewModel =await _personalPageApplication.GetPageInfoByAsync(pageId);
            AccountQueryModel =await _accountQuery.GetAccountAsync(accountId);
            JobOffers = await _jobOfferApplication.GetEditorJobOffersAsync(pageId);
        }

        public async Task<IActionResult> OnGetAcceptOffer(long offerId, long receiverId)
        {
            var AddChatRoom = new AddChatRoom();
            var offer =await _jobOfferApplication.GetOfferAsync(offerId);
            AddChatRoom.SenderId = _authHelper.CurrentAccountId(); ;
            AddChatRoom.ReceiverId = receiverId;
            AddChatRoom.Title = offer.Title;
            AddChatRoom.Price = offer.DoublePrice;
            await _chatRoomApplication.AddChatRoomAsync(AddChatRoom);
            await _jobOfferApplication.AcceptAsync(offerId);
          
            var employerPhoneNumber = 
                await _accountApplication.GetPhoneNumber(receiverId);
            _smsService.Send_YourJobOfferIsAccepted_ToEmployer_MeliPayamak_Com(employerPhoneNumber);

            return RedirectToPage();
        }
    }
}
