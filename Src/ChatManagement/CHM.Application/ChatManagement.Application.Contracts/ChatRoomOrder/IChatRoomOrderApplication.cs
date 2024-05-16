using System.Threading.Tasks;

namespace ChatManagement.Application.Contracts.ChatRoomOrder
{
    public interface IChatRoomOrderApplication
    {
        ValueTask<long> PlaceOrderAsync(long pageId, long chatRoomId, double payAmount);
        ValueTask PaymentSucceededAsync(long orderId, long refId);
        ValueTask<double> GetAmountByAsync(long id);
    }
}
