using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatManagement.Application.Contracts.ChatRoom
{
    public interface IChatRoomApplication
    {
        ValueTask AddChatRoomAsync(AddChatRoom command);
        ValueTask AskForJudgmentAsync(long chatRoomId);
        ValueTask AdminAcceptProjectAsync(long chatRoomId);
        ValueTask AdminRejectProjectAsync(long chatRoomId);
        ValueTask PayAsync(long chatRoomId);
        ValueTask<List<ChatRoomViewModel>> GetChatRoomsAsync();
        ValueTask<ChatRoomViewModel> GetChatRoomAsync(long chatRoomId);
    }
}