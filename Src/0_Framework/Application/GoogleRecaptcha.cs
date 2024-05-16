using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace _0_Framework.Application
{
    public class GoogleRecaptcha : IGoogleRecaptcha
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GoogleRecaptcha(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async ValueTask<bool> IsVerified()
        {
            var http = new HttpClient();
            var secretKey = _configuration.GetSection("GoogleRecaptcha")["SecretKey"];
            var response = _httpContextAccessor.HttpContext.Request.Form["g-recaptcha-response"];
            var result = await http.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={response}", null);

            if (!result.IsSuccessStatusCode) return false;
            var recaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(await result.Content.ReadAsStringAsync());
            return recaptchaResponse.Success;

        }

    }
}