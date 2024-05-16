using System.Threading.Tasks;

namespace ChatManagement.Application.Contracts.Chat
{
    public interface IChatApplication
    {
        ValueTask AddAsync(AddChat command);
        ValueTask<string> AddWithReturnFileFolderPathAsync(AddChat command);
        ValueTask<string> AddWithReturnFilePathAsync(AddChat command);
        ValueTask MakeProjectFileAsync(long chatId);
        ValueTask AcceptProjectAsync(long chatId);
        ValueTask RejectProjectAsync(long chatId);
    }
}