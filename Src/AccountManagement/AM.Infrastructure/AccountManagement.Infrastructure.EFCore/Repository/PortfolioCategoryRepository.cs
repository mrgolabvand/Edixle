using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.PortfolioCategory;
using AccountManagement.Domain.PortfolioCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class PortfolioCategoryRepository : BaseRepository<long, PortfolioCategory>, IPortfolioCategoryRepository
    {
        private readonly AccountContext _accountContext;

        public PortfolioCategoryRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<EditPortfolioCategory> GetDetailsAsync(long id) =>
            await _accountContext.PortfolioCategories.Select(v => new EditPortfolioCategory
            {
                Id = v.Id,
                Name = v.Name,
                Keywords = v.Keywords,
                MetaDescription = v.MetaDescription,
                Slug = v.Slug,
                PictureTitle = v.PictureTitle,
                PictureAlt = v.PictureAlt,
                BaseCategoryId = v.PortfolioBaseCategoryId
            }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<PortfolioCategoryViewModel>> SearchAsync(PortfolioCategorySearchModel searchModel)
        {
            var query = _accountContext.PortfolioCategories.Include(v => v.PortfolioBaseCategory).Select(v => new PortfolioCategoryViewModel
            {
                Id = v.Id,
                Name = v.Name,
                BaseCategory = v.PortfolioBaseCategory.Name,
                BaseCategoryId = v.PortfolioBaseCategoryId,
                PictureAlt = v.PictureAlt,
                Picture = v.Picture,
                PictureTitle = v.PictureTitle,
                IsRemoved = v.IsRemoved,
                Keywords = v.Keywords,
                MetaDescription = v.MetaDescription,
                Slug = v.Slug,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(v => v.Name.Contains(searchModel.Name));

            return await query.OrderByDescending(v => v.Id).ToListAsync();
        }

        public async ValueTask<string> GetCategorySlugAsync(long id)
        {
            var account = await _accountContext.PortfolioCategories.Select(v => new { v.Id, v.Name })
                .FirstOrDefaultAsync(v => v.Id == id);
            return account?.Name;
        }

        public async ValueTask<List<PortfolioCategoryViewModel>> GetListAsync() =>
            await _accountContext.PortfolioCategories.Select(v => new PortfolioCategoryViewModel
            {
                Id = v.Id,
                Name = v.Name
            }).ToListAsync();
    }
}
