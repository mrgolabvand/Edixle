using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ChatManagement.Application.Contracts.Chat;
using ChatManagement.Application.Contracts.ChatRoom;
using ChatManagement.Domain.ChatAgg;
using ChatManagement.Domain.ChatRoomAgg;
using Microsoft.EntityFrameworkCore;

namespace ChatManagement.Infrastructure.EFCore.Repository
{
    public class ChatRoomRepository : BaseRepository<long, ChatRoom>, IChatRoomRepository
    {
        private readonly ChatContext _context;

        public ChatRoomRepository(ChatContext context) : base(context)
        {
            _context = context;
        }


        public async ValueTask<List<ChatRoomViewModel>> GetChatRoomsAsync() =>
            await _context.ChatRooms.Select(v => new ChatRoomViewModel
            {
                Id = v.Id,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                IsAskedForJudgment = v.IsAskedForJudgment,
                ReceiverId = v.ReceiverId,
                SenderId = v.SenderId
            }).AsNoTracking().ToListAsync();

        public async ValueTask<ChatRoomViewModel> GetChatRoomAsync(long chatRoomId) =>
            await _context.ChatRooms.Select(v => new ChatRoomViewModel
            {
                Id = v.Id,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                IsAskedForJudgment = v.IsAskedForJudgment,
                ReceiverId = v.ReceiverId,
                SenderId = v.SenderId,
                Price = v.Price,
                Title = v.Title,
                Chats = MapChats(v.Chats),
                AdminJudgmentIsAcceptProject = v.AdminJudgmentIsAcceptProject,
                AdminJudgmentIsRejectProject = v.AdminJudgmentIsRejectProject,
            }).AsNoTracking().FirstOrDefaultAsync(v => v.Id == chatRoomId);

        private static List<ChatViewModel> MapChats(List<Chat> chats)
        {
            return chats.Select(v => new ChatViewModel
            {
                Id = v.Id,
                ChatRoomId = v.ChatRoomId,
                Message = v.Message,
                File = v.File,
                OwnerId = v.OwnerId,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber()
            }).ToList();
        }
    }
}