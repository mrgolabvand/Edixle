using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : BaseRepository<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public async ValueTask<List<ArticleCategoryViewModel>> GetArticleCategoriesAsync() =>
            await _blogContext.ArticleCategories.Select(v => new ArticleCategoryViewModel
            {
                Id = v.Id,
                Name = v.Name,
            }).ToListAsync();

        public async ValueTask<string> GetCategorySlugByAsync(long id)
        {
            var category = await _blogContext.ArticleCategories.Select(v => new { v.Id, v.Slug }).FirstOrDefaultAsync(v => v.Id == id);
            return category.Slug;
        }

        public async ValueTask<EditArticleCategory> GetDetailsAsync(long id) =>
            await _blogContext.ArticleCategories.Select(v => new EditArticleCategory
            {
                Id = v.Id,
                CanonicalAddress = v.CanonicalAddress,
                Description = v.Description,
                Keyeords = v.Keyeords,
                MetaDescription = v.MetaDescription,
                Name = v.Name,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                ShowOrder = v.ShowOrder,
                Slug = v.Slug
            }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<ArticleCategoryViewModel>> SearchAsync(ArticleCategorySearchModel searchModel)
        {
            var query = _blogContext.ArticleCategories
                .Include(v => v.Articles)
                .Select(v => new ArticleCategoryViewModel
            {
                Id = v.Id,
                ShowOrder = v.ShowOrder,
                CreationDate = v.CreationDate.ToFarsi(),
                Description = v.Description,
                Name = v.Name,
                Picture = v.Picture,
                ArticlesCount = v.Articles.Count
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(v => v.Name.Contains(searchModel.Name));

            return await query.OrderByDescending(v => v.ShowOrder).ToListAsync();
        }
    }
}
