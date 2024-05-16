using _0_Framework.Application;
using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.PortfolioCategory;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class PortfolioCategoryQuery : IPortfolioCategoryQuery
    {
        private readonly AccountContext _accountContext;

        public PortfolioCategoryQuery(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<List<PortfolioCategoryQueryModel>> GetCategoriesAsync() =>
            await _accountContext.PortfolioCategories.Select(v => new PortfolioCategoryQueryModel
            {
                Id = v.Id,
                Keywords = v.Keywords,
                MetaDescription = v.MetaDescription,
                Name = v.Name,
                Slug = v.Slug,
                IsRemoved = v.IsRemoved,
                PictureTitle = v.PictureTitle,
                Picture = v.Picture,
                PictureAlt = v.PictureAlt,
                BaseCategory = v.PortfolioBaseCategory.Name,
                BaseCategoryId = v.PortfolioBaseCategoryId,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber()
            }).AsNoTracking().ToListAsync();
    }
}
