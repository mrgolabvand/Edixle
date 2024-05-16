using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ChatManagement.Domain.ChatAgg;

namespace ChatManagement.Domain.ChatRoomAgg
{
    public class ChatRoom : BaseEntity
    {
        public string Title { get; private set; }
        public double Price { get; private set; }
        public long SenderId { get; private set; }
        public long ReceiverId { get; private set; }
        public bool IsAskedForJudgment { get; private set; }
        public bool AdminJudgmentIsAcceptProject { get; private set; }
        public bool AdminJudgmentIsRejectProject { get; private set; }
        public bool IsPayed { get; private set; }
        public List<Chat> Chats { get; private set; }

        public ChatRoom(long senderId, long receiverId, string title, double price)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            IsAskedForJudgment = false;
            AdminJudgmentIsAcceptProject = false;
            AdminJudgmentIsRejectProject = false;
            IsPayed = false;
            Title = title;
            Price = price;
        }

        public void AskForJudgment()
        {
            IsAskedForJudgment = true;
        }

        public void AdminAcceptProject()
        {
            AdminJudgmentIsAcceptProject = true;
            AdminJudgmentIsRejectProject = false;
        }

        public void AdminRejectProject()
        {
            AdminJudgmentIsAcceptProject = false;
            AdminJudgmentIsRejectProject = true;
        }

        public void Pay()
        {
            IsPayed = true;
        }
    }
}
