using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ChatManagement.Application.Contracts.ChatRoom;

namespace ChatManagement.Domain.ChatRoomAgg
{
    public interface IChatRoomRepository : IBaseRepository<long, ChatRoom>
    {
        ValueTask<List<ChatRoomViewModel>> GetChatRoomsAsync();
        ValueTask<ChatRoomViewModel> GetChatRoomAsync(long chatRoomId);
    }
}
