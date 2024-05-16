using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using ChatManagement.Domain.ChatRoomOrderAgg;
using Microsoft.EntityFrameworkCore;

namespace ChatManagement.Infrastructure.EFCore.Repository
{
    public class ChatRoomOrderRepository : BaseRepository<long, ChatRoomOrder>, IChatRoomOrderRepository
    {
        private readonly ChatContext _context;

        public ChatRoomOrderRepository(ChatContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<double> GetAmountByAsync(long id)
        {
           var order = await _context.ChatRoomOrders.Select(v => new { v.Id, v.PayAmount })
               .FirstOrDefaultAsync(v => v.Id == id);
           return order.PayAmount;
        }
    }
}
