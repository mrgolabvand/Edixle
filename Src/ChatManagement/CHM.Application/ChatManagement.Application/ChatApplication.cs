using System.Threading.Tasks;
using _0_Framework.Application;
using ChatManagement.Application.Contracts.Chat;
using ChatManagement.Domain.ChatAgg;

namespace ChatManagement.Application
{
    public class ChatApplication : IChatApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IChatRepository _chatRepository;
        private readonly IFileHostUploader _fileHostUploader;

        public ChatApplication(IChatRepository chatRepository, IFileUploader fileUploader, IFileHostUploader fileHostUploader)
        {
            _chatRepository = chatRepository;
            _fileUploader = fileUploader;
            _fileHostUploader = fileHostUploader;
        }

        public async ValueTask AddAsync(AddChat command)
        {
            var file = string.Empty;
            if (command.File != null)
            {
                var path = $"ChatRooms/{command.ChatRoomId}";
                //file = await _fileHostUploader.UploadAsync(command.File, path);
                file = await _fileUploader.UploadAsync(command.File, path);
            }
            var chat = new Chat(command.OwnerId, command.ChatRoomId, command.Message, file);
            await _chatRepository.CreateAsync(chat);
            await _chatRepository.SaveChangesAsync();
        }

        public async ValueTask<string> AddWithReturnFileFolderPathAsync(AddChat command)
        {
            var file = string.Empty;
            var path = string.Empty;
            if (command.File != null)
            {
                path = $"ChatRooms/{command.ChatRoomId}";
                //file = await _fileHostUploader.UploadAsync(command.File, path);
                file = await _fileUploader.UploadAsync(command.File, path);
            }
            var chat = new Chat(command.OwnerId, command.ChatRoomId, command.Message, file);
            await _chatRepository.CreateAsync(chat);
            await _chatRepository.SaveChangesAsync();
            return path;
        }
        public async ValueTask<string> AddWithReturnFilePathAsync(AddChat command)
        {
            var file = string.Empty;
            var path = string.Empty;
            var _ = string.Empty;
            if (command.File != null)
            {
                path = $"ChatRooms/{command.ChatRoomId}";
                file = await _fileUploader.UploadAsync(command.File, path);
                //(file, _) = await _fileHostUploader.UploadFileAsync(command.File, path);
            }
            var chat = new Chat(command.OwnerId, command.ChatRoomId, command.Message, file);
            await _chatRepository.CreateAsync(chat);
            await _chatRepository.SaveChangesAsync();
            return file;
        }

        public async ValueTask MakeProjectFileAsync(long chatId)
        {
            var chat = await _chatRepository.GetAsync(chatId);
            chat.MakeFileProject();
            await _chatRepository.SaveChangesAsync();
        }

        public async ValueTask AcceptProjectAsync(long chatId)
        {
            var chat = await _chatRepository.GetAsync(chatId);
            chat.AcceptProject();
            await _chatRepository.SaveChangesAsync();
        }

        public async ValueTask RejectProjectAsync(long chatId)
        {
            var chat = await _chatRepository.GetAsync(chatId);
            chat.RejectProject();
            await _chatRepository.SaveChangesAsync();
        }
    }
}
