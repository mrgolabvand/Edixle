using System;
using ChatManagement.Domain.ChatRoomAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatManagement.Infrastructure.EFCore.Mapping
{
    public class ChatRoomMapping : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.ToTable("ChatRooms");
            builder.HasKey(x => x.Id);
            builder.Property(v => v.Title).HasMaxLength(100).IsRequired();

            builder.HasMany(v => v.Chats)
                .WithOne(v => v.ChatRoom)
                .HasForeignKey(v => v.ChatRoomId);

        }
    }
}