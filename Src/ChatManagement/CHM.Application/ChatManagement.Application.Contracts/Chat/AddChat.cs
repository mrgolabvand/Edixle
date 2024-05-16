using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ChatManagement.Application.Contracts.Chat
{
    public class AddChat
    {
        public long OwnerId { get; set; }
        public long ChatRoomId { get; set; }
        public string Message { get; set; }

        [FileExtensionLimitation(new string[] { ".jpeg", ".jpg", ".png", ".mp4", ".mkv", ".wav" }, ErrorMessage = ValidationMessage.InvalidFileFormat)]
        public IFormFile File { get; set; }
    }
}
