using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using AccountManagement.Application.Contracts.PersonalPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Account;
using EdixleQuery.Contracts.ProjectOffer;
using PlanManagement.Application.Contracts.EditorPlan;
using PlanManagement.Application.Contracts.EditorPlanOrder;

namespace ServiceHost.Pages
{
    public class EditorProfilePlansModel : PageModel
    {
        private readonly IAuthHelper _authHelper;
        private readonly IAccountQuery _accountQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IPersonalPageApplication _personalPageApplication;
        private readonly IEditorPlanApplication _editorPlanApplication;
        private readonly IEditorPlanOrderApplication _editorPlanOrderApplication;

        public EditorProfilePlansModel(IAuthHelper authHelper, IAccountQuery accountQuery, IPersonalPageApplication personalPageApplication, IEditorPlanApplication editorPlanApplication, IEditorPlanOrderApplication editorPlanOrderApplication, IZarinPalFactory zarinPalFactory)
        {
            _authHelper = authHelper;
            _accountQuery = accountQuery;
            _personalPageApplication = personalPageApplication;
            _editorPlanApplication = editorPlanApplication;
            _editorPlanOrderApplication = editorPlanOrderApplication;
            _zarinPalFactory = zarinPalFactory;
        }

        public PersonalPageViewModel PersonalPageViewModel { get; set; }
        public AccountQueryModel AccountQueryModel { get; set; }
        public List<EditorPlanViewModel> EditorPlans { get; set; }
        public async Task OnGet()
        {
            var accountId = _authHelper.CurrentAccountId();
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(accountId);
            PersonalPageViewModel = await _personalPageApplication.GetPageInfoWithPlanByAsync(pageId);
            AccountQueryModel = await _accountQuery.GetAccountAsync(accountId);
            EditorPlans = await _editorPlanApplication.GetListAsync();
        }

        public async Task<IActionResult> OnPostPay(long planId)
        {
            var plan = await _editorPlanApplication.GetPlanAsync(planId);
            var pageId = await _personalPageApplication.GetPersonalPageIdByAsync(_authHelper.CurrentAccountId());

            var orderId = await _editorPlanOrderApplication.PlaceOrderAsync(pageId, plan.Id, plan.DoublePrice);
            var paymentResponse = _zarinPalFactory.CreatePaymentRequest(plan.DoublePrice.ToString(), "", "", $"خرید پلن {plan.Title} از example.com", orderId, "EditorProfilePlans");
            return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }


        public async Task<IActionResult> OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
        {
            var orderAmount = await _editorPlanOrderApplication.GetAmountByAsync(oId);
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString());

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status == 100)
            {
                await _editorPlanOrderApplication.PaymentSucceededAsync(oId, verificationResponse.RefID);
                result.Succeeded("خرید پلن با موفقیت انجام شد.", "0");
                return RedirectToPage("./PaymentResult", result);
            }
            result = result.Failed("پرداخت ناموفق بود، در صورت کسر وجه تا 24 ساعت دیگر بازگشت داده میشود.");
            return RedirectToPage("./PaymentResult", result);
        }
    }
}
