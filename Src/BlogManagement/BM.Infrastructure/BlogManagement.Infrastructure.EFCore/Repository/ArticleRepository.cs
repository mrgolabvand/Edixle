using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : BaseRepository<long, Article>, IArticleRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public async ValueTask<EditArticle> GetDetailsAsync(long id) =>
             await _blogContext.Articles.Select(v => new EditArticle
             {
                 Id = v.Id,
                 CanonicalAddress = v.CanonicalAddress,
                 CategoryId = v.CategoryId,
                 Description = v.Description,
                 Keywords = v.Keywords,
                 MetaDescription = v.MetaDescription,
                 PictureAlt = v.PictureAlt,
                 PictureTitle = v.PictureTitle,
                 PublishDate = v.PublishDate.ToFarsi(),
                 ShortDescription = v.ShortDescription,
                 Slug = v.Slug,
                 Title = v.Title
             }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<Article> GetWithCategoryAsync(long id)=>
            await _blogContext.Articles.Include(v => v.Category)
                .FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<ArticleViewModel>> SearchAsync(ArticleSearchModel searchModel)
        {
            var query = _blogContext.Articles.Include(v => v.Category).Select(v => new ArticleViewModel
            {
                Title = v.Title,
                Category = v.Category.Name,
                CategoryId = v.CategoryId,
                CreationDate = v.CreationDate.ToFarsi(),
                Id = v.Id,
                Picture = v.Picture,
                PublishDate = v.PublishDate.ToFarsi(),
                ShortDescription = v.ShortDescription
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(v => v.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId != 0)
                query = query.Where(v => v.CategoryId == searchModel.CategoryId);

            return await query.OrderByDescending(v => v.Id).ToListAsync();
        }
    }
}
