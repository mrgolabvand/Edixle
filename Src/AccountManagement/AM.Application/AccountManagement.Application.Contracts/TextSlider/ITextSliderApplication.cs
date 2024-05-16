using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.TextSlider
{
    public interface ITextSliderApplication
    {
        ValueTask<OperationResult> CreateAsync(CreateTextSlider command);
        ValueTask<OperationResult> EditAsync(EditTextSlider command);
        ValueTask<EditTextSlider> GetDetailsAsync(long id);
        ValueTask<List<TextSliderViewModel>> SearchAsync(TextSliderSearchModel searchModel);
        ValueTask<OperationResult> RemoveAsync(long id);
        ValueTask<OperationResult> RestoreAsync(long id);
    }
}