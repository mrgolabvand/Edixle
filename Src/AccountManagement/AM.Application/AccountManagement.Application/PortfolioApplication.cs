using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Portfolio;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.PersonalPageAgg;
using AccountManagement.Domain.PortfolioAgg;
using AccountManagement.Domain.PortfolioCategoryAgg;
using Microsoft.AspNetCore.Hosting;

namespace AccountManagement.Application
{
    public class PortfolioApplication : IPortfolioApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IFileHostUploader _fileHostUploader;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IPersonalPageRepository _personalPageRepository;
        private readonly IFileUploader _fileUploader;
        public PortfolioApplication(IPortfolioRepository portfolioRepository, IPersonalPageRepository personalPageRepository, IAuthHelper authHelper, IFileHostUploader fileHostUploader, IFileUploader fileUploader)
        {
            _portfolioRepository = portfolioRepository;
            _personalPageRepository = personalPageRepository;
            _authHelper = authHelper;
            _fileHostUploader = fileHostUploader;
            _fileUploader = fileUploader;
        }

        public async ValueTask<(OperationResult, string)> CreateAsync(CreatePortfolio command)
        {
            var operation = new OperationResult();

            var accountId = _authHelper.CurrentAccountId();
            var pageId = await _personalPageRepository.GetPersonalPageIdByAsync(accountId);

            if (await _portfolioRepository.ExistsAsync(v => v.Name == command.Name))
                return (operation.Failed(ApplicationMessages.IsDuplicated), string.Empty/*, string.Empty*/);

            if (await _portfolioRepository.GetPortfoliosCountAsync(pageId) == 10)
                return (operation.Failed(ApplicationMessages.PortfoliosCountLimit), string.Empty/*, string.Empty*/);


            var slug = command.Name.Slugify();
            var name = Tools.CodeGenerator(20);

            var filePath = $"{name}/File";
            var video = await _fileUploader.UploadAsync(command.Video, filePath);
            //var (video, videoPath/*, videoPathUWM*/) = await _fileHostUploader.UploadFileAsync(command.Video, filePath);

            var picturePath = $"{name}/Picture";

            var picture = await _fileUploader.UploadAsync(command.Picture, picturePath);



            var portfolio = new Portfolio(command.Name, command.ShortDescription, command.Description, video, picture, command.PictureAlt, command.PictureTitle, command.Tags, command.Slug, command.Keywords, command.MetaDescription, pageId);

            await _portfolioRepository.CreateAsync(portfolio);
            await _portfolioRepository.SaveChangesAsync();

            return (operation.Succeeded(), video/*, videoPathUWM*/);
        }

        public async ValueTask<(OperationResult, string)> EditAsync(EditPortfolio command)
        {
            var operation = new OperationResult();


            if (await _portfolioRepository.ExistsAsync(v => v.Name == command.Name && v.Id != command.Id))
                return (operation.Failed(ApplicationMessages.IsDuplicated), string.Empty/*, string.Empty*/);


            var portfolio = await _portfolioRepository.GetAsync(command.Id);

            if (portfolio == null)
                return (operation.Failed(ApplicationMessages.RecordNotFound), string.Empty/*, string.Empty*/);

            //var slug = command.Slug.Slugify();
            //var slug = Tools.CodeGenerator(20);

            //var categorySlug = await _portfolioCategoryRepository.GetCategorySlugAsync(command.CategoryId);

            //var filePath = $"{slug}/File";
            var filePath = portfolio.Video;

            var video = string.Empty;
            var videoPath = string.Empty;
            if (command.Video != null)
            {

                //await _fileHostUploader.DeleteFileAsync(filePath);

                //var (video, videoPath, vieoPathUWM) = await _fileUploader.UploadByProgressAsync(command.Video, filePath);
                video = await _fileUploader.UploadAsync(command.Video, filePath);
            }


            //var picturePath = $"{slug}/Picture";
            var picturePath = portfolio.Picture;

            //if (command.Picture != null)
            //    await _fileHostUploader.DeleteFileAsync(picturePath);

            var picture = await _fileUploader.UploadAsync(command.Picture, picturePath);

            portfolio.Edit(command.Name, command.ShortDescription, command.Description, video, picture, command.PictureAlt, command.PictureTitle, command.Tags, command.Slug, command.Keywords, command.MetaDescription);

            await _portfolioRepository.SaveChangesAsync();

            return (operation.Succeeded(), videoPath/*, vieoPathUWM*/);
        }

        public async ValueTask<EditPortfolio> GetDetailsAsync(long id, long pageId) =>
            await _portfolioRepository.GetDetailsAsync(id, pageId);

        public async ValueTask<EditPortfolio> GetDetailsAsync(long id) =>
            await _portfolioRepository.GetDetailsAsync(id);

        public async ValueTask<List<PortfolioViewModel>> SearchAsync(PortfolioSearchModel searchModel) =>
            await _portfolioRepository.SearchAsync(searchModel);

        public async ValueTask<PortfolioViewModel> GetPortfolioAsync(long id) =>
            await _portfolioRepository.GetPortfolioAsync(id);

        public async ValueTask<OperationResult> RemoveAsync(long id)
        {
            var operation = new OperationResult();

            var portfolio = await _portfolioRepository.GetAsync(id);

            if (portfolio == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            portfolio.Remove();
            await _portfolioRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> RestoreAsync(long id)
        {
            var operation = new OperationResult();

            var portfolio = await _portfolioRepository.GetAsync(id);

            if (portfolio == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            portfolio.Restore();
            await _portfolioRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> ConfirmAsync(long id)
        {
            var operation = new OperationResult();

            var portfolio = await _portfolioRepository.GetAsync(id);

            if (portfolio == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            portfolio.Confirm();
            await _portfolioRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> CancelAsync(long id)
        {
            var operation = new OperationResult();

            var portfolio = await _portfolioRepository.GetAsync(id);

            if (portfolio == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            portfolio.Cancel();
            await _portfolioRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<PortfolioViewModel> GetFileFromPortfolioAsync(long id) =>
            await _portfolioRepository.GetFileFromPortfolioAsync(id);

        public async ValueTask<long> GetDownloadCountAsync(long id)
        {
            var portfolio = await _portfolioRepository.GetAsync(id);
            return portfolio.Id;
        }

        public async ValueTask<long> GetPortfolioIdByAsync(string portfolioSlug) =>
             await _portfolioRepository.GetPortfolioIdByAsync(portfolioSlug);
    }
}
