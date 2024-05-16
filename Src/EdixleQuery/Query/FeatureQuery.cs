using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.Feature;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class FeatureQuery : IFeatureQuery
    {
        private readonly AccountContext _accountContext;

        public FeatureQuery(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<List<FeatureQueryModel>> GetFeaturesAsync() =>
            await _accountContext.Features.Where(v => !v.IsRemoved).Select(v => new FeatureQueryModel
            {
                Id = v.Id,
                Description = v.Description,
                Picture = v.Picture,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                Title = v.Title
            }).ToListAsync();
    }
}
