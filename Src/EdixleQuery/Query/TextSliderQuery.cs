using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.TextSlider;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class TextSliderQuery : ITextSliderQuery
    {
        private readonly AccountContext _accountContext;

        public TextSliderQuery(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<List<TextSliderQueryModel>> GetListAsync() =>
            await _accountContext.TextSliders.Where(v => !v.IsRemoved).Select(v => new TextSliderQueryModel
            {
                Id = v.Id,
                Description = v.Description,
                Title = v.Title,
                Link = v.Link
            }).AsNoTracking().ToListAsync();
    }
}
