using System.Linq;
using System.Security.Claims;
using _0_Framework.Infrastructure;
using Hangfire.Dashboard;

namespace ServiceHost
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            if (httpContext.User.Identity.IsAuthenticated)
            {
                var role = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == Roles.Admin)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
