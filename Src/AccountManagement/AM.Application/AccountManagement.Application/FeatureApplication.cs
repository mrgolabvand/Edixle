using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Feature;
using AccountManagement.Domain.FeatureAgg;

namespace AccountManagement.Application
{
    public class FeatureApplication : IFeatureApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IFileHostUploader _fileHostUploader;
        private readonly IFeatureRepository _featureRepository;
        public FeatureApplication(IFeatureRepository featureRepository, IFileUploader fileUploader, IFileHostUploader fileHostUploader)
        {
            _featureRepository = featureRepository;
            _fileUploader = fileUploader;
            _fileHostUploader = fileHostUploader;
        }

        public async ValueTask<List<FeatureViewModel>> GetListAsync() => await _featureRepository.GetListAsync();

        public async ValueTask<OperationResult> CreateAsync(CreateFeature command)
        {
            var operation = new OperationResult();

            if (await _featureRepository.ExistsAsync(v => v.Title == command.Title))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var slug = command.Title.Slugify();
            var picture = await _fileUploader.UploadAsync(command.Picture, $"Features/{slug}");


            var feature = new Feature(command.Title, command.Description, picture, command.PictureAlt, command.PictureTitle);

            await _featureRepository.CreateAsync(feature);
            await _featureRepository.SaveChangesAsync();
            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditFeature command)
        {
            var operation = new OperationResult();

            if (await _featureRepository.ExistsAsync(v => v.Title == command.Title && v.Id != command.Id))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var feature = await _featureRepository.GetAsync(command.Id);

            if (feature == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            var slug = command.Title.Slugify();
            if (command.Picture != null)
                _fileUploader.Delete($"Features/{slug}");

            var picture = await _fileUploader.UploadAsync(command.Picture, $"Features/{slug}");

            feature.Edit(command.Title, command.Description, picture, command.PictureAlt, command.PictureTitle);
            await _featureRepository.SaveChangesAsync();
            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> RemoveAsync(long id)
        {
            var operation = new OperationResult();

            var feature = await _featureRepository.GetAsync(id);

            if (feature == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            feature.Remove();

            await _featureRepository.SaveChangesAsync();
            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> RestoreAsync(long id)
        {
            var operation = new OperationResult();

            var feature = await _featureRepository.GetAsync(id);

            if (feature == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            feature.Restore();

            await _featureRepository.SaveChangesAsync();
            return operation.Succeeded();
        }

        public async ValueTask<EditFeature> GetDetailsAsync(long id) => await _featureRepository.GetDetailsAsync(id);
    }
}
