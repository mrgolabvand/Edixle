using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.TextSlider;
using AccountManagement.Domain.TextSliderAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class TextSliderRepository : BaseRepository<long , TextSlider>, ITextSliderRepository
    {
        private readonly AccountContext _accountContext;

        public TextSliderRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<EditTextSlider> GetDetailsAsync(long id) =>
            await _accountContext.TextSliders.Select(v => new EditTextSlider
            {
                Id = v.Id,
                Title = v.Title,
                Link = v.Link,
                Description = v.Description
            }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<TextSliderViewModel>> SearchAsync(TextSliderSearchModel searchModel)
        {
            var query = _accountContext.TextSliders.Select(v => new TextSliderViewModel
            {
                Id = v.Id,
                Title = v.Title,
                Description = v.Description,
                Link = v.Link,
                IsRemoved = v.IsRemoved,
                CreationDate = v.CreationDate.ToFarsi()
            });

            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.Title))
                    query = query.Where(v => v.Title.Contains(searchModel.Title));

                if (!string.IsNullOrWhiteSpace(searchModel.Description))
                    query = query.Where(v => v.Description.Contains(searchModel.Description));
            }

            return await query.ToListAsync();
        }
    }
}
