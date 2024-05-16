
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public interface IFileUploader
    {
        ValueTask<string> UploadAsync(IFormFile file, string path);
        ValueTask<(string, string)> UploadByProgressAsync(IFormFile file, string path);
        void Delete(string path);
        void DeleteFile(string path);
        void DeleteFileFullPath(string path);
        ValueTask<bool> SetWatermarkAsync(string UwmPath, string path);
    }
}
