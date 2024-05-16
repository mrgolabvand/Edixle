using _0_Framework.Application;
using AccountManagement.Domain.PortfolioCategoryAgg;
using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.PortfolioBaseCategory;
using EdixleQuery.Contracts.PortfolioCategory;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class PortfolioBaseCategoryQuery : IPortfolioBaseCategoryQuery
    {
        private readonly AccountContext _accountContext;

        public PortfolioBaseCategoryQuery(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<List<PortfolioBaseCategoryQueryModel>> GetListAsync() =>
            await _accountContext.PortfolioBaseCategories
                .Include(v => v.PortfolioCategories)
                .Select(v => new PortfolioBaseCategoryQueryModel
                {
                    Id = v.Id,
                    Name = v.Name,
                    CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                    PictureTitle = v.PictureTitle,
                    Keywords = v.Keywords,
                    MetaDescription = v.MetaDescription,
                    PictureAlt = v.PictureAlt,
                    Picture = v.Picture,
                    Slug = v.Slug,
                    PortfolioCategories = MapCategories(v.PortfolioCategories)
                }).AsNoTracking().ToListAsync();

        private static List<PortfolioCategoryQueryModel> MapCategories(List<PortfolioCategory> portfolioCategories)
        {
            return portfolioCategories.Where(v => !v.IsRemoved).Select(v => new PortfolioCategoryQueryModel
            {
                Id = v.Id,
                Slug = v.Slug,
                Name = v.Name,
                Picture = v.Picture,
                BaseCategoryId = v.PortfolioBaseCategoryId,
                IsRemoved = v.IsRemoved,
                PictureAlt = v.PictureAlt,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                PictureTitle = v.PictureTitle,
                Keywords = v.Keywords,
                MetaDescription = v.MetaDescription
            }).ToList();
        }
    }
}
