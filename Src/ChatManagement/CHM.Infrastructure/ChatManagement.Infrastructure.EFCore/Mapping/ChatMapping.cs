using System;
using ChatManagement.Domain.ChatAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatManagement.Infrastructure.EFCore.Mapping
{
    public class ChatMapping : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");
            builder.HasKey(x => x.Id);

            builder.Property(v => v.Message).HasMaxLength(250);
            builder.Property(v => v.File).HasMaxLength(250);

            builder.HasOne(v => v.ChatRoom)
                .WithMany(v => v.Chats)
                .HasForeignKey(v => v.ChatRoomId);

        }
    }
}
