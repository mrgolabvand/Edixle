using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatManagement.Application.Contracts.ChatRoom
{
    public class AddChatRoom
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
    }
}
