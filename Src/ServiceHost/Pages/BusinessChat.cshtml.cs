using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.Sms;
using _0_Framework.Application.ZarinPal;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.PersonalPage;
using ChatManagement.Application.Contracts.Chat;
using ChatManagement.Application.Contracts.ChatRoom;
using ChatManagement.Application.Contracts.ChatRoomOrder;
using EdixleQuery.Contracts.Account;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using EdixleQuery.Contracts.Chat;
using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.Plan;
using ServiceHost.Hubs;
using ServiceHost.Utils;
using WalletManagement.Application.Contracts.Transaction;
using WalletManagement.Application.Contracts.Wallet;

namespace ServiceHost.Pages
{
    [RequestSizeLimit(ByteSizes.Ten_GB)]
    [RequestFormLimits(MultipartBodyLengthLimit = ByteSizes.Ten_GB)]
    public class BusinessChatModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly ISmsService _smsService;
        private readonly IAccountQuery _accountQuery;
        private readonly IChatRoomQuery _chatRoomQuery;
        private readonly IEditorPlanQuery _editorPlanQuery;
        private readonly IChatApplication _chatApplication;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IPersonalPageQuery _personalPageQuery;
        private readonly IWalletApplication _walletApplication;
        private readonly IChatRoomApplication _chatRoomApplication;
        private readonly ITransactionApplication _transactionApplication;
        private readonly IEmployerPageApplication _employerPageApplication;
        private readonly IPersonalPageApplication _personalPageApplication;
        private readonly IChatRoomOrderApplication _chatRoomOrderApplication;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IFileHostUploader _fileHostUploader;
        private readonly IFileUploader _fileUploader;
        private readonly IHubContext<UploaderHub> _uploaderHubContext;
        private readonly IHubContext<ChatHub> _chatHubContext;

        public BusinessChatModel(IAuthHelper authHelper, IChatRoomQuery chatRoomQuery, IChatApplication chatApplication, IPersonalPageQuery personalPageQuery, IChatRoomApplication chatRoomApplication, IEditorPlanQuery editorPlanQuery, IPersonalPageApplication personalPageApplication, IZarinPalFactory zarinPalFactory, IChatRoomOrderApplication chatRoomOrderApplication, IEmployerPageApplication employerPageApplication, IBackgroundJobClient backgroundJobClient, IFileUploader fileUploader, IHubContext<UploaderHub> uploaderHubContext, IFileHostUploader fileHostUploader, IHubContext<ChatHub> chatHubContext, ITransactionApplication transactionApplication, IWalletApplication walletApplication, ISmsService smsService, IAccountQuery accountQuery)
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
            _backgroundJobClient = backgroundJobClient;
            _fileUploader = fileUploader;
            _uploaderHubContext = uploaderHubContext;
            _fileHostUploader = fileHostUploader;
            _chatHubContext = chatHubContext;
            _transactionApplication = transactionApplication;
            _walletApplication = walletApplication;
            _smsService = smsService;
            _accountQuery = accountQuery;
        }

        [BindProperty] public AddChat AddChat { get; set; }
        public List<ChatRoomQueryModel> ChatRooms = new();
        public ChatRoomQueryModel ChatRoom { get; set; }
        public bool IsSender = false;

        public async Task OnGet(long id = 0)
        {
            var accountId = _authHelper.CurrentAccountId();
            IsSender = await _personalPageQuery.IsEditorAsync(accountId);
            ChatRooms = await _chatRoomQuery.GetAccountChatRoomsAsync(accountId, IsSender);
            if (id != 0)
            {
                var chatRoom = await _chatRoomQuery.GetAccountChatRoomAsync(accountId, id, IsSender);
                if (chatRoom.IsPayed && (IsSender ? chatRoom.SenderId == accountId : chatRoom.ReceiverId == accountId))
                {
                    ChatRoom = chatRoom;
                }
            }

        }

        public async Task<IActionResult> OnPost(AddChat AddChat)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(AddChat.Message) && AddChat.File == null)
                    return RedirectToPage(AddChat.ChatRoomId);

                var accountId = _authHelper.CurrentAccountId();
                IsSender = await _personalPageQuery.IsEditorAsync(accountId);

                AddChat.OwnerId = accountId;
                if (IsSender)
                {
                    var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(accountId);
                    var pagePlan = await _editorPlanQuery.GetEditorPagePlanAsync(pageId);
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
                            if (AddChat.File.Length > ByteSizes.Two_GB)
                            {
                                AddChat.File = null;
                            }
                        }

                    }
                }
                else
                {
                    if (AddChat.File is { Length: > ByteSizes.Five_GB })
                    {
                        AddChat.File = null;
                    }

                }

                var filePath = await _chatApplication.AddWithReturnFilePathAsync(AddChat);

                var chatRoom = await _chatRoomApplication.GetChatRoomAsync(AddChat.ChatRoomId);
                if (IsSender)
                {
                    await _chatHubContext.Clients.User(chatRoom.ReceiverId.ToString()).SendAsync("Refresh");
                    await _chatHubContext.Clients.User(chatRoom.SenderId.ToString()).SendAsync("Refresh");

                    var receiverAccount = await _accountQuery.GetAccountAsync(chatRoom.ReceiverId);
                    await Task.Run(() =>
                    {
                        _smsService.Send_NewProjectMessage_MeliPayamak_Com(receiverAccount.PhoneNumber, chatRoom.Title, "ادیتور");
                    });
                }
                else
                {
                    await _chatHubContext.Clients.User(chatRoom.SenderId.ToString()).SendAsync("Refresh");
                    await _chatHubContext.Clients.User(chatRoom.ReceiverId.ToString()).SendAsync("Refresh");

                    var senderAccount = await _accountQuery.GetAccountAsync(chatRoom.SenderId);
                    await Task.Run(() =>
                    {
                        _smsService.Send_NewProjectMessage_MeliPayamak_Com(senderAccount.PhoneNumber, chatRoom.Title, "کارفرما");
                    });
                }

                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    BackgroundJob.Schedule(() => _fileUploader.DeleteFile(filePath), TimeSpan.FromDays(7));
                }
            }

            return RedirectToPage(routeValues: AddChat.ChatRoomId);

        }

        public async Task<IActionResult> OnGetMakeFileProject(long chatId, long chatRoomId)
        {
            var accountId = _authHelper.CurrentAccountId();
            IsSender = await _personalPageQuery.IsEditorAsync(accountId);
            var chatRoom = await _chatRoomQuery.GetAccountChatRoomAsync(accountId, chatRoomId, IsSender);

            if (chatRoom.IsPayed && (IsSender ? chatRoom.SenderId == accountId : chatRoom.ReceiverId == accountId))
            {
                await _chatApplication.MakeProjectFileAsync(chatId);
            }
            return RedirectToPage(routeValues: chatRoomId);
        }

        public async Task<IActionResult> OnGetAcceptProject(long chatId, long chatRoomId)
        {
            var accountId = _authHelper.CurrentAccountId();
            
            IsSender = await _personalPageQuery.IsEditorAsync(accountId);
            
            var chatRoom = await _chatRoomQuery.GetAccountChatRoomAsync(accountId, chatRoomId, IsSender);
            
            var senderWalletId = await _walletApplication.GetWalletIdByAccountIdAsync(chatRoom.SenderId);
            var receiverWalletId = await _walletApplication.GetWalletIdByAccountIdAsync(chatRoom.ReceiverId);

            var addTransaction = new AddTransaction()
            {
                Amount = (decimal)chatRoom.Price,
                WalletId = receiverWalletId,
                ReceiverWalletId = senderWalletId,
                Description = $"تکمیل پروژه: {chatRoom.Title}"
            };

            if (chatRoom.IsPayed && (IsSender ? chatRoom.SenderId == accountId : chatRoom.ReceiverId == accountId))
            {
                await _chatApplication.AcceptProjectAsync(chatId);
                var (result, transactionId) = await _transactionApplication.AddAsync(addTransaction);
                if (result.IsSucceeded)
                {
                    await _transactionApplication.SuccessAsync(transactionId);
                }
            }

            return RedirectToPage(routeValues: chatRoomId);
        }

        public async Task<IActionResult> OnGetRejectProject(long chatId, long chatRoomId)
        {
            var accountId = _authHelper.CurrentAccountId();
            IsSender = await _personalPageQuery.IsEditorAsync(accountId);
            var chatRoom = await _chatRoomQuery.GetAccountChatRoomAsync(accountId, chatRoomId, IsSender);

            if (chatRoom.IsPayed && (IsSender ? chatRoom.SenderId == accountId : chatRoom.ReceiverId == accountId))
            {
                await _chatApplication.RejectProjectAsync(chatId);
            }
            return RedirectToPage(routeValues: chatRoomId);
        }

        public async Task<IActionResult> OnGetAskForJudgment(long chatRoomId)
        {
            var accountId = _authHelper.CurrentAccountId();
            IsSender = await _personalPageQuery.IsEditorAsync(accountId);
            var chatRoom = await _chatRoomQuery.GetAccountChatRoomAsync(accountId, chatRoomId, IsSender);

            if (chatRoom.IsPayed && (IsSender ? chatRoom.SenderId == accountId : chatRoom.ReceiverId == accountId))
            {
                await _chatRoomApplication.AskForJudgmentAsync(chatRoomId);
            }
            return RedirectToPage(routeValues: chatRoomId);
        }

        public async Task<IActionResult> OnPostPay(long chatRoomId)
        {
            var chatRoom = await _chatRoomApplication.GetChatRoomAsync(chatRoomId);
            //var pageId = await _employerPageApplication.GetEmployerPageIdByAccountIdAsync(_authHelper.CurrentAccountId());

            //var orderId = await _chatRoomOrderApplication.PlaceOrderAsync(pageId, chatRoom.Id, chatRoom.Price);
            //var paymentResponse = _zarinPalFactory.CreatePaymentRequest(chatRoom.Price.ToString(), "", "", $"پرداخت پروژه {chatRoom.Title} از example.com", orderId, "BusinessChat");
            //return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");

            TempData["chatRoomId"] = chatRoomId.ToString();
            var transaction = new AddTransaction();
            var walletId = await _walletApplication.GetWalletIdByAccountIdAsync(_authHelper.CurrentAccountId());
            transaction.Description = "افزایش موجودی";
            transaction.WalletId = walletId;
            transaction.ReceiverWalletId = walletId;
            transaction.Amount = (decimal)chatRoom.Price;
            var (_, orderId) = await _transactionApplication.AddAsync(transaction);


            var paymentResponse = _zarinPalFactory.CreatePaymentRequest(transaction.Amount.ToString(), "", "", $"افزایش موجودی کیف پول", orderId, "BusinessChat");
            return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }


        public async Task<IActionResult> OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] Guid oId)
        {
            //var orderAmount = await _chatRoomOrderApplication.GetAmountByAsync(oId);
            //var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString());

            //var result = new PaymentResult();
            //if (status == "OK" && verificationResponse.Status == 100)
            //{
            //    await _chatRoomOrderApplication.PaymentSucceededAsync(oId, verificationResponse.RefID);
            //    result.Succeeded("پرداخت مبلغ پروژه با موفقیت انجام شد.", "0");
            //    return RedirectToPage("./PaymentResult", result);
            //}
            //result = result.Failed("پرداخت ناموفق بود، در صورت کسر وجه تا 24 ساعت دیگر بازگشت داده میشود.");
            //return RedirectToPage("./PaymentResult", result);

            var orderAmount = await _transactionApplication.GetAmountByIdAsync(oId);
            var stringOrderAmount = orderAmount.ToString("F0");
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, stringOrderAmount);

            var result = new PaymentResult();
            var chatRoomId = long.Parse(TempData["chatRoomId"].ToString());
            if (status == "OK" && verificationResponse.Status == 100)
            {
                await _transactionApplication.SuccessAsync(oId);
                await _chatRoomApplication.PayAsync(chatRoomId);
                result.Succeeded("افزایش موجودی با موفقیت انجام شد.", "0");
                return RedirectToPage("./PaymentResult", result);
            }
            result = result.Failed("پرداخت ناموفق بود، در صورت کسر وجه تا 24 ساعت دیگر بازگشت داده میشود.");
            return RedirectToPage("./PaymentResult", result);
        }
    }
}
