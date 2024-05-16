using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public interface IFileHostUploader
    {
        ValueTask<string> UploadAsync(IFormFile file, string path);
        ValueTask<string> UploadEditAsync(IFormFile file, string path);
        ValueTask DeleteAsync(string path);
        ValueTask DeleteFileAsync(string path);
        ValueTask<(string, string)> UploadFileAsync(IFormFile file, string path);
        ValueTask<(string, string)> UploadEditFileAsync(IFormFile file, string path);
        ValueTask<bool> SetWatermarkAsync(string UwmPath, string path);
        ValueTask DeleteFileFullPath(string path);
    }
}
