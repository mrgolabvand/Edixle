using System.Collections.Generic;
using System.Threading.Tasks;
using ChatManagement.Application.Contracts.ChatRoom;
using ChatManagement.Domain.ChatRoomAgg;
using WalletManagement.Domain.WalletAgg;

namespace ChatManagement.Application
{
    public class ChatRoomApplication : IChatRoomApplication
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IChatRoomRepository _chatRoomRepository;
        public ChatRoomApplication(IChatRoomRepository chatRoomRepository, IWalletRepository walletRepository)
        {
            _chatRoomRepository = chatRoomRepository;
            _walletRepository = walletRepository;
        }

        public async ValueTask AddChatRoomAsync(AddChatRoom command)
        {
            var chatRoom = new ChatRoom(command.SenderId, command.ReceiverId, command.Title, command.Price);
            var walletId = await _walletRepository.GetWalletIdByAccountIdAsync(command.ReceiverId);

            if (await _walletRepository.HasCredit(walletId, (decimal)command.Price))
                chatRoom.Pay();
            
            await _chatRoomRepository.CreateAsync(chatRoom);
            await _chatRoomRepository.SaveChangesAsync();
        }

        public async ValueTask AskForJudgmentAsync(long chatRoomId)
        {
            var chatRoom = await _chatRoomRepository.GetAsync(chatRoomId);
            chatRoom.AskForJudgment();
            await _chatRoomRepository.SaveChangesAsync();
        }

        public async ValueTask AdminAcceptProjectAsync(long chatRoomId)
        {
            var chatRoom = await _chatRoomRepository.GetAsync(chatRoomId);
            chatRoom.AdminAcceptProject();
            await _chatRoomRepository.SaveChangesAsync();
        }

        public async ValueTask AdminRejectProjectAsync(long chatRoomId)
        {
            var chatRoom = await _chatRoomRepository.GetAsync(chatRoomId);
            chatRoom.AdminRejectProject();
            await _chatRoomRepository.SaveChangesAsync();
        }

        public async ValueTask PayAsync(long chatRoomId)
        {
            var chatRoom = await _chatRoomRepository.GetAsync(chatRoomId);
            chatRoom.Pay();
            await _chatRoomRepository.SaveChangesAsync();
        }

        public async ValueTask<List<ChatRoomViewModel>> GetChatRoomsAsync() =>
            await _chatRoomRepository.GetChatRoomsAsync();

        public async ValueTask<ChatRoomViewModel> GetChatRoomAsync(long chatRoomId) =>
            await _chatRoomRepository.GetChatRoomAsync(chatRoomId);

    }
}
