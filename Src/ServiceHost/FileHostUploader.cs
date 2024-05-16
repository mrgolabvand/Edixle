using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using _0_Framework.Application;
using FluentFTP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using ServiceHost.Hubs;

namespace ServiceHost
{
    public class FileHostUploader : IFileHostUploader
    {
        public const string Host = "255.255.255.255";
        public const string UserName = "UserName";
        public const string Password = "Password";
        private readonly IAuthHelper _authHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHubContext<UploaderHub> _uploaderHubContext;

        public FileHostUploader(IHubContext<UploaderHub> uploaderHubContext, IAuthHelper authHelper, IWebHostEnvironment webHostEnvironment)
        {
            _uploaderHubContext = uploaderHubContext;
            _authHelper = authHelper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async ValueTask<string> UploadAsync(IFormFile file, string path)
        {
            if (file == null) return "";
            var client = new FtpClient(Host, UserName, Password);
            await client.ConnectAsync();

            var directoryPath = $"/public_html/UploadedFiles/{path}";
            //var name = Tools.CodeGenerator(4);
            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
            var filePath = $"{directoryPath}/{fileName}";


            await client.UploadAsync(file.OpenReadStream(), filePath, FtpRemoteExists.Overwrite, true);
            await client.DisconnectAsync();

            return $"{path}/{fileName}";
        }

        public async ValueTask<string> UploadEditAsync(IFormFile file, string path)
        {
            if (file == null) return "";
            var client = new FtpClient(Host, UserName, Password);
            await client.ConnectAsync();

            var directoryPath = $"/public_html/UploadedFiles/{path}";
            //var name = Tools.CodeGenerator(4);


            await client.UploadAsync(file.OpenReadStream(), directoryPath, FtpRemoteExists.Overwrite, true);
            await client.DisconnectAsync();

            return path;
        }


        public async ValueTask<(string, string)> UploadFileAsync(IFormFile file, string path)
        {
            var token = new CancellationToken();
            using var client = new FtpClient(Host, UserName, Password);
            await client.ConnectAsync(token);
            var directoryPath = $"/public_html/UploadedFiles/{path}";
            //var name = Tools.CodeGenerator(4);
            var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
            //var filePathUWM = $"{directoryPath}/UWM-{fileName}";
            var filePath = $"{directoryPath}/{fileName}";

            // define the progress tracking callback
            var progress = new  Progress<FtpProgress>(async p  =>
            {
                // percent done = p.Progress
                var userId = _authHelper.CurrentAccountId().ToString();
                var progress = Math.Round(p.Progress, 1);
                await _uploaderHubContext.Clients.User(userId).SendAsync("ProgressBar", progress);
            });
            // upload a file with progress tracking
            await client.UploadAsync(file.OpenReadStream(),
                /*filePathUWM*/ filePath,
                FtpRemoteExists.Overwrite,
                true,
                progress,
                token);

            return ($"{path}/{fileName}", filePath/*, filePathUWM*/);
        }

        public async ValueTask<(string, string)> UploadEditFileAsync(IFormFile file, string path)
        {
            var token = new CancellationToken();
            using var client = new FtpClient(Host, UserName, Password);
            await client.ConnectAsync(token);
            var directoryPath = $"/public_html/UploadedFiles/{path}";
            //var name = Tools.CodeGenerator(4);
            //var fileName = $"{DateTime.Now.ToFileName()}-{file.FileName}";
            //var filePathUWM = $"{directoryPath}/UWM-{fileName}";
            //var filePath = $"{directoryPath}/{fileName}";

            // define the progress tracking callback
            var progress = new  Progress<FtpProgress>(async p  =>
            {
                // percent done = p.Progress
                var userId = _authHelper.CurrentAccountId().ToString();
                var progress = Math.Round(p.Progress, 1);
                await _uploaderHubContext.Clients.User(userId).SendAsync("ProgressBar", progress);
            });
            // upload a file with progress tracking
            await client.UploadAsync(file.OpenReadStream(),
                /*filePathUWM*/ directoryPath,
                FtpRemoteExists.Overwrite,
                true,
                progress,
                token);

            return (path, directoryPath/*, filePathUWM*/);
        }

        public async ValueTask<bool> SetWatermarkAsync(string UwmPath, string path)
        {
            // Add ffmpeg.exe & ffprobe.exe

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

                //await DeleteFileFullPath(UwmPath);
                //#endregion

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async ValueTask DeleteFileFullPath(string path)
        {
            try
            {
                var client = new FtpClient(Host, UserName, Password);
                await client.ConnectAsync();
                await client.DeleteFileAsync(path);
                await client.DisconnectAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async ValueTask DeleteAsync(string path)
        {
            var client = new FtpClient(Host, UserName, Password);
            await client.ConnectAsync();
            await client.DeleteDirectoryAsync($"/public_html/UploadedFiles/{path}");
            await client.DisconnectAsync();
        }

        public async ValueTask DeleteFileAsync(string path)
        {
            var client = new FtpClient(Host, UserName, Password);
            await client.ConnectAsync();
            await client.DeleteFileAsync($"/public_html/UploadedFiles/{path}");
            await client.DisconnectAsync();
        }
    }
}
