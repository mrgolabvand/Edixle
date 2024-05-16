using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace ServiceHost.Hubs
{
    public class ChatHub : Hub
    {
        //public async Task SendMessage(string senderId, string receiverId, string message, IFormFile file)
        //{
        //    await Clients.Users(senderId, receiverId).SendAsync(message, file);
        //}
    }
}
