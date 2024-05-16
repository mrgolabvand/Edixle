using System;
using _0_Framework.Domain;

namespace ChatManagement.Domain.ChatRoomOrderAgg
{
    public class ChatRoomOrder : BaseEntity
    {
        public long EmployerPageId { get; private set; }
        public long ChatRoomId { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public long RefId { get; private set; }

        public ChatRoomOrder(long employerPageId, long chatRoomId, double payAmount)
        {
            EmployerPageId = employerPageId;
            ChatRoomId = chatRoomId;
            PayAmount = payAmount;
            IsPaid = false;
            IsCanceled = false;
            RefId = 0;
        }

        public void PaymentSucceeded(long refId)
        {
            IsPaid = true;
            IsCanceled = false;
            if (refId != 0)
                RefId = refId;
        }

        public void Cancel()
        {
            IsCanceled = true;
            IsPaid = false;
        }
    }
}
