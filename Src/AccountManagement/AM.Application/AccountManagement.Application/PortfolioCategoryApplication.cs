using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.PortfolioCategory;
using AccountManagement.Domain.PortfolioCategoryAgg;

namespace AccountManagement.Application
{
    public class PortfolioCategoryApplication : IPortfolioCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IFileHostUploader _fileHostUploader;
        private readonly IPortfolioCategoryRepository _portfolioCategoryRepository;

        public PortfolioCategoryApplication(IPortfolioCategoryRepository portfolioCategoryRepository, IFileUploader fileUploader, IFileHostUploader fileHostUploader)
        {
            _portfolioCategoryRepository = portfolioCategoryRepository;
            _fileUploader = fileUploader;
            _fileHostUploader = fileHostUploader;
        }

        public async ValueTask<OperationResult> CreateAsync(CreatePortfolioCategory command)
        {
            var operation = new OperationResult();

            if (await _portfolioCategoryRepository.ExistsAsync(v => v.Name == command.Name))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var slug = command.Slug.Slugify();
            //var picturePath = $"{slug}/Picture";
            var picturePath = $"{Tools.CodeGenerator(6)}/Picture";
            var picture = await _fileUploader.UploadAsync(command.Picture, picturePath);

            var category = new PortfolioCategory(command.Name, command.BaseCategoryId, slug, command.Keywords, command.MetaDescription, picture, command.PictureAlt, command.PictureTitle);
            await _portfolioCategoryRepository.CreateAsync(category);
            await _portfolioCategoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditPortfolioCategory command)
        {
            var operation = new OperationResult();

            if (await _portfolioCategoryRepository.ExistsAsync(v => v.Name == command.Name && v.Id != command.Id))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var category = await _portfolioCategoryRepository.GetAsync(command.Id);

            if (category == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var slug = command.Slug.Slugify();

            var picturePath = category.Picture;
            if (command.Picture != null)
                _fileUploader.DeleteFile(picturePath);
            var picture = await _fileUploader.UploadAsync(command.Picture, picturePath);

            category.Edit(command.Name, command.BaseCategoryId, slug, command.Keywords, command.MetaDescription, picture, command.PictureAlt, command.PictureTitle);
            await _portfolioCategoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditPortfolioCategory> GetDetailsAsync(long id) =>
            await _portfolioCategoryRepository.GetDetailsAsync(id);

        public async ValueTask<List<PortfolioCategoryViewModel>> SearchAsync(PortfolioCategorySearchModel searchModel) =>
            await _portfolioCategoryRepository.SearchAsync(searchModel);

        public async ValueTask<OperationResult> RemoveAsync(long id)
        {
            var operation = new OperationResult();

            var category = await _portfolioCategoryRepository.GetAsync(id);

            if (category == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            category.Remove();
            await _portfolioCategoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> RestoreAsync(long id)
        {
            var operation = new OperationResult();

            var category = await _portfolioCategoryRepository.GetAsync(id);

            if (category == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            category.Restore();
            await _portfolioCategoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<List<PortfolioCategoryViewModel>> GetListAsync() =>
            await _portfolioCategoryRepository.GetListAsync();
    }
}
