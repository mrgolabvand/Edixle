using System.Threading.Tasks;
using _0_Framework.Domain;

namespace ChatManagement.Domain.ChatRoomOrderAgg
{
    public interface IChatRoomOrderRepository : IBaseRepository<long, ChatRoomOrder>
    {
        ValueTask<double> GetAmountByAsync(long id);
    }
}
