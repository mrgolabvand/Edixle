using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;

namespace ServiceHost
{
    public class GoogleRecaptchaTagHelper : TagHelper
    {
        private readonly IConfiguration _configuration;

        public GoogleRecaptchaTagHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var siteKey = _configuration.GetSection("GoogleRecaptcha")["SiteKey"];
            output.TagName = "div";
            output.AddClass("g-recaptcha", HtmlEncoder.Default);
            output.Attributes.Add("data-sitekey", siteKey);
            output.Attributes.Add("data-theme", "dark");

            base.Process(context, output);
        }
    }
}
