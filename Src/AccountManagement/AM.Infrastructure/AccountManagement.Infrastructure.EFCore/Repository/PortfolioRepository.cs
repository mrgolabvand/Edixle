using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Portfolio;
using AccountManagement.Domain.PortfolioAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class PortfolioRepository : BaseRepository<long, Portfolio>, IPortfolioRepository
    {
        private readonly AccountContext _accountContext;

        public PortfolioRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<PortfolioViewModel> GetFileFromPortfolioAsync(long id) =>
             await _accountContext.Portfolios.Select(v => new PortfolioViewModel()
             {
                 Id = v.Id,
                 PortfolioName = v.Name,
                 File = v.Video
             }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<EditPortfolio> GetDetailsAsync(long id, long pageId) =>
            await _accountContext.Portfolios.Select(v => new EditPortfolio
            {
                Id = v.Id,
                Name = v.Name,
                PageId = v.PageId,
                Description = v.Description,
                Slug = v.Slug,
                Keywords = v.Keywords,
                MetaDescription = v.MetaDescription,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                ShortDescription = v.ShortDescription,
                Tags = v.Tags
            }).FirstOrDefaultAsync(v => v.Id == id && v.PageId == pageId);

        public async ValueTask<EditPortfolio> GetDetailsAsync(long id) =>
            await _accountContext.Portfolios.Select(v => new EditPortfolio
            {
                Id = v.Id,
                Name = v.Name,
                PageId = v.PageId,
                Description = v.Description,
                Slug = v.Slug,
                Keywords = v.Keywords,
                MetaDescription = v.MetaDescription,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                ShortDescription = v.ShortDescription,
                Tags = v.Tags
            }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<PortfolioViewModel> GetPortfolioAsync(long id) =>
            await _accountContext.Portfolios
                .Include(v => v.PortfolioDownloads)
                .Select(v => new PortfolioViewModel
                {
                    Id = v.Id,
                    Description = v.Description,
                    PortfolioName = v.Name,
                    IsConfirm = v.IsConfirm,
                    IsRemoved = v.IsRemoved,
                    Keywords = v.Keywords,
                    Slug = v.Slug,
                    File = v.Video,
                    Picture = v.Picture,
                    PictureAlt = v.PictureAlt,
                    MetaDescription = v.MetaDescription,
                    Tags = v.Tags,
                    PictureTitle = v.PictureTitle,
                    ShortDescription = v.ShortDescription,
                    DownloadCount = v.PortfolioDownloads.Count(v => v.PortfolioId == id)
                }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<PortfolioViewModel>> SearchAsync(PortfolioSearchModel searchModel)
        {
            var query = _accountContext.Portfolios
                .Include(v => v.PortfolioDownloads)
                .Select(v => new PortfolioViewModel
                {
                    Id = v.Id,
                    ShortDescription = v.ShortDescription,
                    Tags = v.Tags,
                    CreationDate = v.CreationDate.ToFarsi(),
                    Picture = v.Picture,
                    PortfolioName = v.Name,
                    PageId = v.PageId,
                    IsConfirm = v.IsConfirm,
                    DownloadCount = v.PortfolioDownloads.Count()
                });

            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.PortfolioName))
                    query = query.Where(v => v.PortfolioName.Contains(searchModel.PortfolioName));

                if (!string.IsNullOrWhiteSpace(searchModel.Tags))
                    query = query.Where(v => v.Tags.Contains(searchModel.Tags));
            }

            var result = await query.ToListAsync();

            foreach (var portfolioViewModel in result)
            {
                var page = await _accountContext.PersonalPages
                    .FirstOrDefaultAsync(v => v.Id == portfolioViewModel.PageId);
                portfolioViewModel.FullName = page.FullName;

                //var account = await _accountContext.Accounts
                //    .FirstOrDefaultAsync(v => v.Id == portfolioViewModel.AccountId);
                //portfolioViewModel.UserName = account.UserName;
            }

            return result;
        }

        public async ValueTask<long> GetPortfolioIdByAsync(string portfolioSlug)
        {
            var account = await _accountContext.Portfolios.FirstOrDefaultAsync(v => v.Slug == portfolioSlug);
            return account?.Id ?? 0;
        }

        public async ValueTask<long> GetPortfoliosCountAsync(long pageId) =>
            await _accountContext.Portfolios.CountAsync(v => v.PageId == pageId);
    }
}
