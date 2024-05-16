using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IFileHostUploader _fileHostUploader;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository, IFileHostUploader fileHostUploader)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
            _fileHostUploader = fileHostUploader;
        }

        public async ValueTask<OperationResult> CreateAsync(CreateArticle command)
        {
            var operation = new OperationResult();

            if (await _articleRepository.ExistsAsync(v => v.Title == command.Title))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            //var slug = command.Slug.Slugify();
            var slug = Tools.CodeGenerator(20);
            //var categorySlug = _articleCategoryRepository.GetCategorySlugByAsync(command.CategoryId);
            var categorySlug = Tools.CodeGenerator(20);
            var path = $"{categorySlug}/{slug}";
            //var picturePath = await _fileHostUploader.UploadAsync(command.Picture, path);
            var picturePath = await _fileUploader.UploadAsync(command.Picture, path);
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var article = new Article(command.Title, command.ShortDescription, command.Description, picturePath, command.PictureTitle, command.PictureAlt, command.Keywords, command.MetaDescription, slug, command.CanonicalAddress, publishDate
                , command.CategoryId);

            await _articleRepository.CreateAsync(article);
            await _articleRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditArticle command)
        {
            var operation = new OperationResult();

            var article = await _articleRepository.GetWithCategoryAsync(command.Id);

            if (article == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (await _articleRepository.ExistsAsync(v => v.Title == command.Title && v.Id != command.Id))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var slug = command.Slug.Slugify();
            //var categorySlug = _articleCategoryRepository.GetCategorySlugByAsync(command.CategoryId);
            //var path = $"{categorySlug}/{slug}";
            var path = article.Picture;
            if (command.Picture != null)
                 _fileUploader.DeleteFile(path);
            var picturePath = await _fileUploader.UploadAsync(command.Picture, path);
            //var picturePath = _fileHostUploader.Upload(command.Picture, path);

            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title, command.ShortDescription, command.Description, picturePath, command.PictureTitle, command.PictureAlt, command.Keywords, command.MetaDescription, slug, command.CanonicalAddress, publishDate
                , command.CategoryId);

            await _articleRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditArticle> GetDetailsAsync(long id) =>
            await _articleRepository.GetDetailsAsync(id);

        public async ValueTask<List<ArticleViewModel>> SearchAsync(ArticleSearchModel searchModel) =>
            await _articleRepository.SearchAsync(searchModel);
    }
}
