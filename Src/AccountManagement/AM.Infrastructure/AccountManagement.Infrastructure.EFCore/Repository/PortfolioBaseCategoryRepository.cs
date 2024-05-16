using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.PortfolioBaseCategory;
using AccountManagement.Application.Contracts.PortfolioCategory;
using AccountManagement.Domain.PortfolioBaseCategoryAgg;
using AccountManagement.Domain.PortfolioCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class PortfolioBaseCategoryRepository : BaseRepository<long, PortfolioBaseCategory>, IPortfolioBaseCategoryRepository
    {
        private readonly AccountContext _accountContext;

        public PortfolioBaseCategoryRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<EditPortfolioBaseCategory> GetDetailsAsync(long id) =>
            await _accountContext.PortfolioBaseCategories.Select(v => new EditPortfolioBaseCategory
                {
                    Id = v.Id,
                    Name = v.Name,
                    Keywords = v.Keywords,
                    MetaDescription = v.MetaDescription,
                    Slug = v.Slug,
                    PictureTitle = v.PictureTitle,
                    PictureAlt = v.PictureAlt,
                }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<PortfolioBaseCategoryViewModel>> SearchAsync(PortfolioBaseCategorySearchModel searchModel)
        {
            var query = _accountContext.PortfolioBaseCategories.Select(v => new PortfolioBaseCategoryViewModel
            {
                Id = v.Id,
                Name = v.Name,
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

            return await query.ToListAsync();
        }

        public async ValueTask<string> GetCategorySlugAsync(long id)
        {
            var account = await _accountContext.PortfolioCategories.Select(v => new { v.Id, v.Name })
                .FirstOrDefaultAsync(v => v.Id == id);
            return account?.Name;
        }

        public async ValueTask<List<PortfolioBaseCategoryViewModel>> GetListAsync() =>
            await _accountContext.PortfolioBaseCategories.Select(v => new PortfolioBaseCategoryViewModel
            {
                Id = v.Id,
                Name = v.Name
            }).ToListAsync();
    }
}
