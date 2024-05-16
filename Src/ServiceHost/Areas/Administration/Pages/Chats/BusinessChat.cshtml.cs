using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.PersonalPage;
using ChatManagement.Application.Contracts.Chat;
using ChatManagement.Application.Contracts.ChatRoom;
using ChatManagement.Application.Contracts.ChatRoomOrder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Chat;
using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.Plan;

namespace ServiceHost.Areas.Administration.Pages.Chats
{
    [RequestSizeLimit(10737418240)]
    [RequestFormLimits(MultipartBodyLengthLimit = 10737418240)]
    public class BusinessChatModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IChatRoomQuery _chatRoomQuery;
        private readonly IEditorPlanQuery _editorPlanQuery;
        private readonly IChatApplication _chatApplication;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IPersonalPageQuery _personalPageQuery;
        private readonly IChatRoomApplication _chatRoomApplication;
        private readonly IEmployerPageApplication _employerPageApplication;
        private readonly IPersonalPageApplication _personalPageApplication;
        private readonly IChatRoomOrderApplication _chatRoomOrderApplication;
        public BusinessChatModel(IAuthHelper authHelper, IChatRoomQuery chatRoomQuery, IChatApplication chatApplication, IPersonalPageQuery personalPageQuery, IChatRoomApplication chatRoomApplication, IEditorPlanQuery editorPlanQuery, IPersonalPageApplication personalPageApplication, IZarinPalFactory zarinPalFactory, IChatRoomOrderApplication chatRoomOrderApplication, IEmployerPageApplication employerPageApplication)
        {
            _authHelper = authHelper;
            _chatRoomQuery = chatRoomQuery;
            _chatApplication = chatApplication;
            _personalPageQuery = personalPageQuery;
            _chatRoomApplication = chatRoomApplication;
            _editorPlanQuery = editorPlanQuery;
            _personalPageApplication = personalPageApplication;
            _zarinPalFactory = zarinPalFactory;
            _chatRoomOrderApplication = chatRoomOrderApplication;
            _employerPageApplication = employerPageApplication;
        }

        [BindProperty] public AddChat AddChat { get; set; }
        public List<ChatRoomQueryModel> ChatRooms = new();
        public ChatRoomQueryModel ChatRoom { get; set; }
        public bool IsSender = true;

        public async Task OnGet(long id, long editorId)
        {
            var chatRoom = await _chatRoomQuery.GetAccountChatRoomAsync(editorId, id, true);
            ChatRoom = chatRoom;
        }

        public async Task<IActionResult> OnPost(long accountId)
        {
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(accountId);
            var pagePlan = await _editorPlanQuery.GetEditorPagePlanAsync(pageId);

            AddChat.OwnerId = accountId;

            if (AddChat.File != null)
            {
                if (pagePlan.IsPlanActive)
                {
                    if (AddChat.File.Length > pagePlan.ChatUploadSizeLimit)
                    {
                        AddChat.File = null;
                    }
                }
                else
                {
                    if (AddChat.File.Length > 2147483648)
                    {
                        AddChat.File = null;
                    }
                }

            }

            await _chatApplication.AddAsync(AddChat);
            return RedirectToPage(routeValues: AddChat.ChatRoomId);
        }

        public async Task<IActionResult> OnGetMakeFileProject(long chatId, long chatRoomId, long editorId)
        {
            await _chatApplication.MakeProjectFileAsync(chatId);
            return RedirectToPage(routeValues: new { id = chatRoomId, editorId });

        }

        public async Task<IActionResult> OnGetAcceptProject(long chatId, long chatRoomId, long editorId)
        {
            await _chatApplication.AcceptProjectAsync(chatId);
            return RedirectToPage(routeValues: new { id = chatRoomId, editorId });

        }

        public async Task<IActionResult> OnGetRejectProject(long chatId, long chatRoomId, long editorId)
        {
            await _chatApplication.RejectProjectAsync(chatId);
            return RedirectToPage(routeValues: new { id = chatRoomId, editorId });

        }
        public async Task<IActionResult> OnGetAdminAcceptProject(long chatRoomId, long editorId)
        {
            await _chatRoomApplication.AdminAcceptProjectAsync(chatRoomId);
            return RedirectToPage(routeValues: new { id = chatRoomId, editorId });
        }

        public async Task<IActionResult> OnGetAdminRejectProject(long chatRoomId, long editorId)
        {
            await _chatRoomApplication.AdminRejectProjectAsync(chatRoomId);
            return RedirectToPage(routeValues: new { id = chatRoomId, editorId });
        }

        public async Task<IActionResult> OnGetAskForJudgment(long chatRoomId, long editorId)
        {
            await _chatRoomApplication.AskForJudgmentAsync(chatRoomId);
            return RedirectToPage(routeValues: new { id = chatRoomId, editorId });

        }

        public async Task<IActionResult> OnPostPay(long chatRoomId, long editorId)
        {
            var chatRoom = await _chatRoomApplication.GetChatRoomAsync(chatRoomId);
            var pageId = await _employerPageApplication.GetEmployerPageIdByAccountIdAsync(_authHelper.CurrentAccountId());

            var orderId = await _chatRoomOrderApplication.PlaceOrderAsync(pageId, chatRoom.Id, chatRoom.Price);
            var paymentResponse = _zarinPalFactory.CreatePaymentRequest(chatRoom.Price.ToString(), "", "", $"پرداخت پروژه {chatRoom.Title} از example.com", orderId, "BusinessChat");
            return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }


        public async Task<IActionResult> OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
        {
            var orderAmount = await _chatRoomOrderApplication.GetAmountByAsync(oId);
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString());

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status == 100)
            {
                await _chatRoomOrderApplication.PaymentSucceededAsync(oId, verificationResponse.RefID);
                result.Succeeded("پرداخت مبلغ پروژه با موفقیت انجام شد.", "0");
                return RedirectToPage("./BusinessChat", result);
            }
            result = result.Failed("پرداخت ناموفق بود، در صورت کسر وجه تا 24 ساعت دیگر بازگشت داده میشود.");
            return RedirectToPage("./Index", result);
        }
    }
}
