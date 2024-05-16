using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Project;
using AccountManagement.Application.Contracts.ProjectOffer;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EdixleQuery.Contracts.Chat;

namespace ServiceHost.Areas.Administration.Pages.Chats
{
    public class IndexModel : PageModel
    {
        private readonly IChatRoomQuery _chatRoomQuery;

        public IndexModel(IChatRoomQuery reviewApplication)
        {
            _chatRoomQuery = reviewApplication;
        }

        [TempData]
        public string Message { get; set; }

        public List<ChatRoomQueryModel> ChatRooms = new();


        public async Task OnGet()
        {
            ChatRooms = await _chatRoomQuery.GetAccountChatRoomsForAdminAsync();
        }

    }
}
