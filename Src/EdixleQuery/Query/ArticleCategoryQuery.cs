using _0_Framework.Application;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.Article;
using EdixleQuery.Contracts.ArticleCategory;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;
        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async ValueTask<List<ArticleCategoryQueryModel>> GetArticleCategoriesAsync() =>
            await _blogContext.ArticleCategories.Include(v => v.Articles).Select(v => new ArticleCategoryQueryModel
            {
                ArticlesCount = v.Articles.Where(v => v.PublishDate <= DateTime.Now).ToList().Count,
                Name = v.Name,
                Slug = v.Slug,
                ShowOrder = v.ShowOrder
            }).ToListAsync();
        public async ValueTask<ArticleCategoryQueryModel> GetArticleCategoryAsync(string slug) =>
            await _blogContext.ArticleCategories.Include(v => v.Articles)
                .Select(v => new ArticleCategoryQueryModel
                {
                    Slug = v.Slug,
                    ShowOrder = v.ShowOrder,
                    Articles = MapArticles(v.Articles),
                    CanonicalAddress = v.CanonicalAddress,
                    Description = v.Description,
                    Picture = v.Picture,
                    PictureAlt = v.PictureAlt,
                    PictureTitle = v.PictureTitle,
                    Keyeords = v.Keyeords,
                    Name = v.Name
                }).FirstOrDefaultAsync(v => v.Slug == slug);

        public async ValueTask<List<ArticleCategoryQueryModel>> GetCategoriesWithArticlesAsync() =>
            await _blogContext.ArticleCategories.Include(v => v.Articles)
                .Select(v => new ArticleCategoryQueryModel
                {
                    Slug = v.Slug,
                    ShowOrder = v.ShowOrder,
                    Articles = MapArticles(v.Articles),
                    CanonicalAddress = v.CanonicalAddress,
                    Description = v.Description,
                    Picture = v.Picture,
                    PictureAlt = v.PictureAlt,
                    PictureTitle = v.PictureTitle,
                    Keyeords = v.Keyeords,
                    Name = v.Name
                }).ToListAsync();


        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles.Where(V => V.PublishDate <= DateTime.Now)
                .Select(v => new ArticleQueryModel
                {
                    Id = v.Id,
                    PublishDate = v.PublishDate.ToFarsi().ToPersianNumber(),
                    ShortDescription = v.ShortDescription.Substring(0, Math.Min(v.ShortDescription.Length, 30)) + " ...",
                    Picture = v.Picture,
                    Slug = v.Slug,
                    PictureAlt = v.PictureAlt,
                    PictureTitle = v.PictureTitle,
                    Title = v.Title
                }).OrderByDescending(v => v.Id).ToList();
        }
    }
}
