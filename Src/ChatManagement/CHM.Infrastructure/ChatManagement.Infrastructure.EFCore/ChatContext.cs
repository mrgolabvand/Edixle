using ChatManagement.Domain.ChatAgg;
using ChatManagement.Domain.ChatRoomAgg;
using ChatManagement.Domain.ChatRoomOrderAgg;
using ChatManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ChatManagement.Infrastructure.EFCore
{
    public class ChatContext : DbContext
    {
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomOrder> ChatRoomOrders { get; set; }
   
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ChatMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
