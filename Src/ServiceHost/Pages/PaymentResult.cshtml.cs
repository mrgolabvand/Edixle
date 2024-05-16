using _0_Framework.Application.ZarinPal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class PaymentResultModel : PageModel
    {
        public PaymentResult Result { get; set; }
        public void OnGet(PaymentResult result)
        {
            Result = result ?? new PaymentResult().Failed("خطای نامشخص رخ داده است. با پشتیبانی سایت تماس بگیرید.");
        }
    }
}
