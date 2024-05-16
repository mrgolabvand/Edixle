using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using ChatManagement.Domain.ChatAgg;

namespace ChatManagement.Infrastructure.EFCore.Repository
{
    public class ChatRepository : BaseRepository<long, Chat>, IChatRepository
    {
        private readonly ChatContext _context;

        public ChatRepository(ChatContext context) : base(context)
        {
            _context = context;
        }
    }
}
