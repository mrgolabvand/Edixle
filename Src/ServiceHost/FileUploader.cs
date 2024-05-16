using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ServiceHost.Hubs;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAuthHelper _authHelper;
        private readonly IHubContext<UploaderHub> _uploaderHubContext;
        public FileUploader(IWebHostEnvironment webHostEnvironment, IAuthHelper authHelper, IHubContext<UploaderHub> uploaderHubContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _authHelper = authHelper;
            _uploaderHubContext = uploaderHubContext;
        }

        public async ValueTask<string> UploadAsync(IFormFile file, string path)
        {
            if (file == null) return "";

            var directoryPath = $"{_webHostEnvironment.WebRootPath}//UploadedFiles//{path}";

            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
            var filePath = $"{directoryPath}//{fileName}";

            using var output = File.Create(filePath);
            await file.CopyToAsync(output);

            return $"{path}/{fileName}";
        }

        public async ValueTask<(string, string)> UploadByProgressAsync(IFormFile file, string path)
        {
            if (file == null) return (string.Empty, string.Empty/*, string.Empty*/);

            var directoryPath = $"{_webHostEnvironment.WebRootPath}/UploadedFiles/{path}";

            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
            //var filePathUWM = $"{directoryPath}/UWM-{fileName}";
            var filePath = $"{directoryPath}/{fileName}";

            await using var output = File.Create(/*filePathUWM*/ filePath);

            var buffer = new byte[16 * 1024];
            var totalBytes = file.Length;
            var totalReadBytes = 0;
            var readBytes = 0;

            await using var input = file.OpenReadStream();

            while ((readBytes = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await output.WriteAsync(buffer, 0, readBytes);
                totalReadBytes += readBytes;
                var progress = (int)((float)totalReadBytes / (float)totalBytes * 100.0);
                var userId = _authHelper.CurrentAccountId().ToString();
                await _uploaderHubContext.Clients.User(userId).SendAsync("ProgressBar", progress);
            }


            return ($"{path}/{fileName}", filePath/*, filePathUWM*/);
        }

        public void Delete(string path)
        {
            try
            {
                var files = Directory.GetFiles(_webHostEnvironment.WebRootPath + "//UploadedFiles//" + path);

                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
            catch (DirectoryNotFoundException)
            {

            }
        }

        public void DeleteFile(string path)
        {
            var filePath = _webHostEnvironment.WebRootPath + "//UploadedFiles//" + path;
            try
            {
                File.Delete(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void DeleteFileFullPath(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async  ValueTask<bool> SetWatermarkAsync(string UwmPath, string path)
        {
            try
            {
                //#region Add Watermark
                //var exPath = $@"{_webHostEnvironment.ContentRootPath}\FFMpeg";
                //FFmpeg.SetExecutablesPath(exPath);

                //var conversion = await FFmpeg.Conversions.FromSnippet.SetWatermark(
                //    UwmPath,
                //    path,
                //    $@"{_webHostEnvironment.WebRootPath}\icons\logo_edixle_watermark_100px.png",
                //    Position.Bottom);

                //var conversionResult = await conversion.Start();

                //await Task.Run(() => DeleteFileFullPath(UwmPath));
                //#endregion

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
