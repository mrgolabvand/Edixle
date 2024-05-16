using System.Collections.Generic;
using ChatManagement.Application.Contracts.Chat;

namespace ChatManagement.Application.Contracts.ChatRoom
{
    public class ChatRoomViewModel : AddChatRoom
    {
        public long Id { get; set; }
        public bool IsAskedForJudgment { get; set; }
        public string CreationDate { get; set; }
        public bool AdminJudgmentIsAcceptProject { get; set; }
        public bool AdminJudgmentIsRejectProject { get; set; }
        public List<ChatViewModel> Chats { get; set; }
    }
}