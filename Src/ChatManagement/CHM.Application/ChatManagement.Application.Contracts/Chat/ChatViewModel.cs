namespace ChatManagement.Application.Contracts.Chat
{
    public class ChatViewModel : AddChat
    {
        public long Id { get; set; }
        public string File { get; set; }
        public string CreationDate { get; set; }
        public bool FileIsProject { get; set; }
        public bool ProjectIsAccepted { get; set; }
        public bool ProjectIsNotAccepted { get; set; }
    }
}