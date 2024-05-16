using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.TextSlider;

namespace AccountManagement.Domain.TextSliderAgg
{
    public interface ITextSliderRepository : IBaseRepository<long, TextSlider>
    {
        ValueTask<EditTextSlider> GetDetailsAsync(long id);
        ValueTask<List<TextSliderViewModel>> SearchAsync(TextSliderSearchModel searchModel);
    }
}
