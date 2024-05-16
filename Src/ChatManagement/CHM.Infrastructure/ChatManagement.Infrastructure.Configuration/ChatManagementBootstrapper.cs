using System;
using ChatManagement.Application;
using ChatManagement.Application.Contracts.Chat;
using ChatManagement.Application.Contracts.ChatRoom;
using ChatManagement.Application.Contracts.ChatRoomOrder;
using ChatManagement.Domain.ChatAgg;
using ChatManagement.Domain.ChatRoomAgg;
using ChatManagement.Domain.ChatRoomOrderAgg;
using ChatManagement.Infrastructure.EFCore;
using ChatManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EdixleQuery.Contracts.Chat;
using EdixleQuery.Query;

namespace ChatManagement.Infrastructure.Configuration
{
    public class ChatManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IChatApplication, ChatApplication>();
            services.AddTransient<IChatRepository, ChatRepository>();

            services.AddTransient<IChatRoomApplication, ChatRoomApplication>();
            services.AddTransient<IChatRoomRepository, ChatRoomRepository>();

            services.AddTransient<IChatRoomOrderApplication, ChatRoomOrderApplication>();
            services.AddTransient<IChatRoomOrderRepository, ChatRoomOrderRepository>();

            services.AddTransient<IChatRoomQuery, ChatRoomQuery>();

            services.AddDbContext<ChatContext>(v => v.UseSqlServer(connectionString));
        }
    }
}
