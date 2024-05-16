using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.PortfolioBaseCategory;
using AccountManagement.Application.Contracts.PortfolioCategory;
using AccountManagement.Domain.PortfolioBaseCategoryAgg;
using AccountManagement.Domain.PortfolioCategoryAgg;

namespace AccountManagement.Application
{
    public class PortfolioBaseCategoryApplication : IPortfolioBaseCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IFileHostUploader _fileHostUploader;
        private readonly IPortfolioBaseCategoryRepository _portfolioBaseCategoryRepository;

        public PortfolioBaseCategoryApplication(IPortfolioBaseCategoryRepository portfolioBaseCategoryRepository, IFileUploader fileUploader, IFileHostUploader fileHostUploader)
        {
            _portfolioBaseCategoryRepository = portfolioBaseCategoryRepository;
            _fileUploader = fileUploader;
            _fileHostUploader = fileHostUploader;
        }

        public async ValueTask<OperationResult> CreateAsync(CreatePortfolioBaseCategory command)
        {
            var operation = new OperationResult();

            if (await _portfolioBaseCategoryRepository.ExistsAsync(v => v.Name == command.Name))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var slug = command.Slug.Slugify();
            //var picturePath = $"{slug}/Picture";
            var picturePath = $"{Tools.CodeGenerator(6)}/Picture";
            var picture = await _fileUploader.UploadAsync(command.Picture, picturePath);

            var category = new PortfolioBaseCategory(command.Name, slug, command.Keywords, command.MetaDescription, picture, command.PictureAlt, command.PictureTitle);
            await _portfolioBaseCategoryRepository.CreateAsync(category);
            await _portfolioBaseCategoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditPortfolioBaseCategory command)
        {
            var operation = new OperationResult();

            if (await _portfolioBaseCategoryRepository.ExistsAsync(v => v.Name == command.Name && v.Id != command.Id))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var category = await _portfolioBaseCategoryRepository.GetAsync(command.Id);

            if (category == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var slug = command.Slug.Slugify();

            //var picturePath = $"{slug}/Picture";
            var picturePath = $"{Tools.CodeGenerator(6)}/Picture";
            if (command.Picture != null)
                _fileUploader.DeleteFile(picturePath);
            var picture = await _fileUploader.UploadAsync(command.Picture, picturePath);
            
            category.Edit(command.Name, slug, command.Keywords, command.MetaDescription, picture, command.PictureAlt, command.PictureTitle);
            await _portfolioBaseCategoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditPortfolioBaseCategory> GetDetailsAsync(long id) =>
            await _portfolioBaseCategoryRepository.GetDetailsAsync(id);

        public async ValueTask<List<PortfolioBaseCategoryViewModel>> SearchAsync(PortfolioBaseCategorySearchModel searchModel) =>
            await _portfolioBaseCategoryRepository.SearchAsync(searchModel);

        public async ValueTask<OperationResult> RemoveAsync(long id)
        {
            var operation = new OperationResult();

            var category = await _portfolioBaseCategoryRepository.GetAsync(id);

            if (category == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            category.Remove();
            await _portfolioBaseCategoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> RestoreAsync(long id)
        {
            var operation = new OperationResult();

            var category = await _portfolioBaseCategoryRepository.GetAsync(id);

            if (category == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            category.Restore();
            await _portfolioBaseCategoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<List<PortfolioBaseCategoryViewModel>> GetListAsync() =>
            await _portfolioBaseCategoryRepository.GetListAsync();
    }
}
