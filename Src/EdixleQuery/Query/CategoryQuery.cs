using AccountManagement.Domain.PortfolioCategoryAgg;
using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.Category;
using EdixleQuery.Contracts.PortfolioCategory;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class CategoryQuery : ICategoryQuery
    {
        private readonly AccountContext _accountContext;

        public CategoryQuery(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<string> GetCategoryNameAsync(string slug)
        {

            var category = await _accountContext.PortfolioCategories.Select(v =>
                new PortfolioCategoryQueryModel
                {
                    Name = v.Name,
                    Slug = v.Slug
                }).FirstOrDefaultAsync(v => v.Slug == slug);
            return category?.Name;
        }

        public async ValueTask<CategoryQueryModel> GetCategoriesAsync()
        {
            var categories = await _accountContext.PortfolioCategories.Select(v => new PortfolioCategoryQueryModel
            {
                Id = v.Id,
                Name = v.Name,
                Slug = v.Slug
            }).ToListAsync();

            return new CategoryQueryModel
            {
                PortfolioCategory = categories
            };
        }

        public async ValueTask<List<BaseCategoryQueryModel>> GetBaseCategoriesAsync() =>
            await _accountContext.PortfolioBaseCategories.Select(v => new BaseCategoryQueryModel
            {
                BaseCategoryId = v.Id,
                BaseCategoryName = v.Name,
                BaseCategorySlug = v.Slug,
                PortfolioCategory = MapPortfolioCategories(v.PortfolioCategories)
            }).AsNoTracking().ToListAsync();

        private static List<PortfolioCategoryQueryModel> MapPortfolioCategories(List<PortfolioCategory> portfolioCategories)
        {
            return portfolioCategories.Select(v => new PortfolioCategoryQueryModel
            {
                Id = v.Id,
                Name = v.Name,
                Slug = v.Slug
            }).ToList();
        }

        public async ValueTask<List<ShowCategoryQueryModel>> GetCategoryAsync(string slug) =>
            await _accountContext.PortfolioCategories.Select(v => new ShowCategoryQueryModel
            {
                Id = v.Id,
                Name = v.Name,
                Slug = v.Slug
            }).ToListAsync();
    }
}

