using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Feature;
using AccountManagement.Domain.FeatureAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class FeatureRepository : BaseRepository<long, Feature>, IFeatureRepository
    {
        private readonly AccountContext _accountContext;

        public FeatureRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<List<FeatureViewModel>> GetListAsync() =>
            await _accountContext.Features.Select(v => new FeatureViewModel
            {
                Id = v.Id,
                IsRemoved = v.IsRemoved,
                Picture = v.Picture,
                Title = v.Title
            }).ToListAsync();

        public async ValueTask<EditFeature> GetDetailsAsync(long id) => 
            await _accountContext.Features.Select(v => new EditFeature
            {
                Id = v.Id,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                Description = v.Description,
                Title = v.Title
            }).AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
    }
}
