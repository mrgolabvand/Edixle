using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IFileHostUploader _fileHostUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader, IFileHostUploader fileHostUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
            _fileHostUploader = fileHostUploader;
        }

        public async ValueTask<OperationResult> CreateAsync(CreateArticelCategory command)
        {
            var operation = new OperationResult();

            if (await _articleCategoryRepository.ExistsAsync(v => v.Name == command.Name))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            //var slug = command.Slug.Slugify();
            var slug = Tools.CodeGenerator(20);
            var pictureName = await _fileUploader.UploadAsync(command.Picture, slug);
            //var pictureName = _fileHostUploader.Upload(command.Picture, slug);
            var articleCategory = new ArticleCategory(command.Name, command.Description, command.ShowOrder, pictureName,command.PictureAlt,command.PictureTitle, slug, command.Keyeords, command.MetaDescription, command.CanonicalAddress);

            await _articleCategoryRepository.CreateAsync(articleCategory);
            await _articleCategoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditArticleCategory command)
        {
            var operation = new OperationResult();

            var articleCategory = await _articleCategoryRepository.GetAsync(command.Id);

            if (articleCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);
            
            if (await _articleCategoryRepository.ExistsAsync(v => v.Name == command.Name))
                return operation.Failed(ApplicationMessages.IsDuplicated);
            var slug = command.Slug.Slugify();
            var filePath = articleCategory.Picture;
            if (command.Picture != null)
                 _fileUploader.DeleteFile(filePath);
            var pictureName = await _fileUploader.UploadAsync(command.Picture, filePath);
            //var pictureName = _fileHostUploader.Upload(command.Picture, slug);
            articleCategory.Edit(command.Name, command.Description, command.ShowOrder, pictureName, command.PictureAlt, command.PictureTitle, slug, command.Keyeords, command.MetaDescription, command.CanonicalAddress);
            await _articleCategoryRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<List<ArticleCategoryViewModel>> GetArticleCategoriesAsync() =>
            await _articleCategoryRepository.GetArticleCategoriesAsync();

        public async ValueTask<EditArticleCategory> GetDetailsAsync(long id) =>
            await _articleCategoryRepository.GetDetailsAsync(id);

        public async ValueTask<List<ArticleCategoryViewModel>> SearchAsync(ArticleCategorySearchModel searchModel) =>
            await _articleCategoryRepository.SearchAsync(searchModel);
    }
}
