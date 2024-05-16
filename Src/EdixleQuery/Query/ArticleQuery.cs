using _0_Framework.Application;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.Article;
using EdixleQuery.Contracts.Comment;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext blogContext, CommentContext commentContext)
        {
            _blogContext = blogContext;
            _commentContext = commentContext;
        }

        public async ValueTask<List<ArticleQueryModel>> GetArticlesAsync() =>
            await _blogContext.Articles.Where(v => v.PublishDate <= DateTime.Now)
                .Include(v => v.Category)
                .Select(v => new ArticleQueryModel
                {
                    Id = v.Id,
                    Slug = v.Slug,
                    Picture = v.Picture,
                    PictureAlt = v.PictureAlt,
                    PictureTitle = v.PictureTitle,
                    CategoryName = v.Category.Name,
                    CategorySlug = v.Category.Slug,
                    ShortDescription = v.ShortDescription.Substring(0, Math.Min(v.ShortDescription.Length, 30)) + " ...",
                    PublishDate = v.PublishDate.ToFarsi().ToPersianNumber(),
                    Day = v.PublishDate.GetDay().ToPersianNumber(),
                    Month = v.PublishDate.ToPersianMonth(),
                    Title = v.Title
                }).OrderByDescending(v => v.Id).ToListAsync();

        public async ValueTask<ArticleQueryModel> GetArticleAsync(string slug)
        {
            var article = await _blogContext.Articles.Where(v => v.PublishDate <= DateTime.Now)
            .Include(v => v.Category)
            .Select(v => new ArticleQueryModel
            {
                Slug = v.Slug,
                Description = v.Description,
                MetaDescription = v.MetaDescription,
                CanonicalAddress = v.CanonicalAddress,
                Keywords = v.Keywords,
                CategoryId = v.CategoryId,
                CategoryName = v.Category.Name,
                PublishDate = v.PublishDate.ToFarsi().ToPersianNumber(),
                CategorySlug = v.Category.Slug,
                Id = v.Id,
                Picture = v.Picture,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                ShortDescription = v.ShortDescription,
                Day = v.PublishDate.GetDay().ToPersianNumber(),
                Month = v.PublishDate.ToPersianMonth(),
                Title = v.Title
            }).FirstOrDefaultAsync(v => v.Slug == slug);

            article.KeywordList = article.Keywords.Split("،").ToList();

            var comments = await _commentContext.Comments
                .Where(v => v.Type == CommentType.Article)
                .Where(v => v.OwnerRecordId == article.Id)
                .Where(v => !v.IsCanceled && v.IsConfirmed)
                .Select(v => new CommentQueryModel
                {
                    Id = v.Id,
                    CommentDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                    Message = v.Message,
                    ParentId = v.ParentId,
                    Name = v.Name
                }).OrderByDescending(v => v.Id).ToListAsync();

            foreach (var comment in comments)
            {
                if (comment.ParentId > 0)
                {
                    var getComment = await _commentContext.Comments.FirstOrDefaultAsync(v => v.Id == comment.ParentId);
                    comment.ParentName = getComment?.Name;
                }
            }

            article.Comments = comments;

            return article;
        }

        public async ValueTask<List<ArticleQueryModel>> GetLatestArticlesAsync() =>
            await _blogContext.Articles.Where(v => v.PublishDate <= DateTime.Now)
                .Include(v => v.Category)
                .Select(v => new ArticleQueryModel
                {
                    Id = v.Id,
                    Slug = v.Slug,
                    Picture = v.Picture,
                    PictureAlt = v.PictureAlt,
                    PictureTitle = v.PictureTitle,
                    ShortDescription = v.ShortDescription,
                    PublishDate = v.PublishDate.ToFarsi().ToPersianNumber(),
                    Day = v.PublishDate.GetDay().ToPersianNumber(),
                    Month = v.PublishDate.ToPersianMonth(),
                    Title = v.Title,
                }).OrderByDescending(v => v.Id).Take(5).ToListAsync();
    }
}
