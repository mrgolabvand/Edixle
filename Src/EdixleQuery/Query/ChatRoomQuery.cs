using _0_Framework.Application;
using AccountManagement.Infrastructure.EFCore;
using ChatManagement.Application.Contracts.Chat;
using ChatManagement.Domain.ChatAgg;
using ChatManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.Chat;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class ChatRoomQuery : IChatRoomQuery
    {
        private readonly ChatContext _chatContext;
        private readonly AccountContext _accountContext;
        public ChatRoomQuery(ChatContext chatContext, AccountContext accountContext)
        {
            _chatContext = chatContext;
            _accountContext = accountContext;
        }

        public async ValueTask<List<ChatRoomQueryModel>> GetAccountChatRoomsAsync(long accountId, bool isSender)
        {
            var employerPages = await _accountContext.EmployerPages.Select(v => new
            {
                v.Id,
                v.AccountId,
                v.FullName,
                v.Picture
            }).AsNoTracking().ToListAsync();

            var editorPages = await _accountContext.PersonalPages.Select(v => new
            {
                v.Id,
                v.AccountId,
                v.FullName,
                v.Picture
            }).AsNoTracking().ToListAsync();


            var chatRooms = await _chatContext.ChatRooms
                .Where(v => isSender ? v.SenderId == accountId : v.ReceiverId == accountId)
                .Select(v => new ChatRoomQueryModel
                {
                    Id = v.Id,
                    SenderId = v.SenderId,
                    ReceiverId = v.ReceiverId,
                    IsAskedForJudgment = v.IsAskedForJudgment,
                    CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                    Chats = MapChats(v.Chats),
                    IsPayed = v.IsPayed,
                    Price = v.Price,
                    Title = v.Title,
                    AdminJudgmentIsAcceptProject = v.AdminJudgmentIsAcceptProject,
                    AdminJudgmentIsRejectProject = v.AdminJudgmentIsRejectProject
                }).AsNoTracking().OrderByDescending(v => v.Id).ToListAsync();

            foreach (var chatRoom in chatRooms)
            {
                var employerPage = employerPages.FirstOrDefault(v => v.AccountId == chatRoom.ReceiverId);
                var editorPage = editorPages.FirstOrDefault(v => v.AccountId == chatRoom.SenderId);

                chatRoom.ReceiverName = employerPage?.FullName;
                chatRoom.ReceiverPicture = employerPage?.Picture;
                chatRoom.SenderName = editorPage?.FullName;
                chatRoom.SenderPicture = editorPage?.Picture;

            }

            return chatRooms;
        }

        public async ValueTask<List<ChatRoomQueryModel>> GetAccountChatRoomsForAdminAsync()
        {
            var employerPages = await _accountContext.EmployerPages.Select(v => new
            {
                v.Id,
                v.AccountId,
                v.FullName,
                v.Picture,
                v.Card
            }).AsNoTracking().ToListAsync();

            var editorPages = await _accountContext.PersonalPages.Select(v => new
            {
                v.Id,
                v.AccountId,
                v.FullName,
                v.Picture,
                v.Card
            }).AsNoTracking().ToListAsync();


            var chatRooms = await _chatContext.ChatRooms
                .Select(v => new ChatRoomQueryModel
                {
                    Id = v.Id,
                    SenderId = v.SenderId,
                    ReceiverId = v.ReceiverId,
                    IsAskedForJudgment = v.IsAskedForJudgment,
                    CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                    Chats = MapChats(v.Chats),
                    IsPayed = v.IsPayed,
                    Price = v.Price,
                    Title = v.Title,
                    AdminJudgmentIsAcceptProject = v.AdminJudgmentIsAcceptProject,
                    AdminJudgmentIsRejectProject = v.AdminJudgmentIsRejectProject
                }).AsNoTracking().ToListAsync();

            foreach (var chatRoom in chatRooms)
            {
                var employerPage = employerPages.FirstOrDefault(v => v.AccountId == chatRoom.ReceiverId);
                var editorPage = editorPages.FirstOrDefault(v => v.AccountId == chatRoom.SenderId);

                chatRoom.ReceiverName = employerPage?.FullName;
                chatRoom.ReceiverPicture = employerPage?.Picture;
                chatRoom.ReceiverCard = employerPage?.Card;

                chatRoom.SenderName = editorPage?.FullName;
                chatRoom.SenderPicture = editorPage?.Picture;
                chatRoom.SenderCard = editorPage?.Card;

            }

            return chatRooms;
        }

        public async ValueTask<ChatRoomQueryModel> GetAccountChatRoomAsync(long accountId, long? chatRoomId, bool isSender)
        {
            var employerPages = await _accountContext.EmployerPages.Select(v => new
            {
                v.Id,
                v.AccountId,
                v.FullName,
                v.Picture,
            }).AsNoTracking().ToListAsync();

            var editorPages = await _accountContext.PersonalPages.Select(v => new
            {
                v.Id,
                v.AccountId,
                v.FullName,
                v.Picture,
            }).AsNoTracking().ToListAsync();


            var chatRoom = await _chatContext.ChatRooms
                .Where(v => isSender ? v.SenderId == accountId : v.ReceiverId == accountId)
                .Select(v => new ChatRoomQueryModel
                {
                    Id = v.Id,
                    SenderId = v.SenderId,
                    ReceiverId = v.ReceiverId,
                    IsAskedForJudgment = v.IsAskedForJudgment,
                    CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                    Chats = MapChats(v.Chats),
                    IsPayed = v.IsPayed,
                    Price = v.Price,
                    Title = v.Title,
                    AdminJudgmentIsAcceptProject = v.AdminJudgmentIsAcceptProject,
                    AdminJudgmentIsRejectProject = v.AdminJudgmentIsRejectProject
                }).FirstOrDefaultAsync(v => v.Id == chatRoomId);

            if (chatRoom != null)
            {
                var employerPage = employerPages.FirstOrDefault(v => v.AccountId == chatRoom.ReceiverId);
                var editorPage = editorPages.FirstOrDefault(v => v.AccountId == chatRoom.SenderId);

                chatRoom.ReceiverName = employerPage?.FullName;
                chatRoom.ReceiverPicture = employerPage?.Picture;

                chatRoom.SenderName = editorPage?.FullName;
                chatRoom.SenderPicture = editorPage?.Picture;
            }
            else
            {
                chatRoom = new ChatRoomQueryModel();
            }

            return chatRoom;
        }


        private static List<ChatViewModel> MapChats(List<Chat> chats)
        {
            return chats.Select(v => new ChatViewModel
            {
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                ChatRoomId = v.ChatRoomId,
                File = v.File,
                Id = v.Id,
                Message = v.Message,
                OwnerId = v.OwnerId,
                FileIsProject = v.FileIsProject,
                ProjectIsAccepted = v.ProjectIsAccepted,
                ProjectIsNotAccepted = v.ProjectIsNotAccepted
            }).ToList();
        }
    }
}
