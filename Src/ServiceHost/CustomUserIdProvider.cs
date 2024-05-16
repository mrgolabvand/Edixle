using _0_Framework.Application;
using Microsoft.AspNetCore.SignalR;

namespace ServiceHost
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        private readonly IAuthHelper _authHelper;

        public CustomUserIdProvider(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public string GetUserId(HubConnectionContext connection)
        {
            return _authHelper.CurrentAccountId().ToString();
        }
    }
}
