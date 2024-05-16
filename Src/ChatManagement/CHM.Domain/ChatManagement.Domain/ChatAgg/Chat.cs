using _0_Framework.Domain;
using ChatManagement.Domain.ChatRoomAgg;

namespace ChatManagement.Domain.ChatAgg
{
    public class Chat : BaseEntity
    {
        public long OwnerId { get; private set; }
        public long ChatRoomId { get; private set; }
        public string Message { get; private set; }
        public string File { get; private set; }
        public bool FileIsProject { get; private set; }
        public bool ProjectIsAccepted { get; private set; }
        public bool ProjectIsNotAccepted { get; private set; }
        public ChatRoom ChatRoom { get; private set; }
        
        public Chat(long ownerId, long chatRoomId, string message, string file)
        {
            OwnerId = ownerId;
            ChatRoomId = chatRoomId;
            Message = message;
            File = file;
            FileIsProject = false;
            ProjectIsAccepted = false;
            ProjectIsNotAccepted = false;
        }

        public void MakeFileProject()
        {
            FileIsProject = true;
        }

        public void AcceptProject()
        {
            ProjectIsAccepted = true;
            ProjectIsNotAccepted = false;
        }

        public void RejectProject()
        {
            ProjectIsAccepted = false;
            ProjectIsNotAccepted = true;
        }
    }
}
