namespace EdixleQuery.Contracts.Chat
{
    public interface IChatRoomQuery
    {
        ValueTask<List<ChatRoomQueryModel>> GetAccountChatRoomsAsync(long accountId, bool isSender);
        ValueTask<List<ChatRoomQueryModel>> GetAccountChatRoomsForAdminAsync();
        ValueTask<ChatRoomQueryModel> GetAccountChatRoomAsync(long accountId,long? chatRoomId, bool isSender);

    }
}