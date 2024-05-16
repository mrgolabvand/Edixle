using ChatManagement.Application.Contracts.ChatRoom;

namespace EdixleQuery.Contracts.Chat
{
    public class ChatRoomQueryModel : ChatRoomViewModel
    {
        public string ReceiverName { get; set; }
        public string ReceiverCard { get; set; }
        public string ReceiverPicture { get; set; }
        public string SenderName { get; set; }
        public string SenderCard { get; set; }
        public string SenderPicture { get; set; }
        public bool IsPayed { get; set; }

    }
}
