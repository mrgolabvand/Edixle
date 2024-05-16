using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.TextSlider;
using AccountManagement.Domain.TextSliderAgg;

namespace AccountManagement.Application
{
    public class TextSliderApplication : ITextSliderApplication
    {
        private readonly ITextSliderRepository _textSliderRepository;

        public TextSliderApplication(ITextSliderRepository textSliderRepository)
        {
            _textSliderRepository = textSliderRepository;
        }

        public async ValueTask<OperationResult> CreateAsync(CreateTextSlider command)
        {
            var operation = new OperationResult();

            if (await _textSliderRepository.ExistsAsync(v => v.Title == command.Title))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var slider = new TextSlider(command.Title, command.Description, command.Link);

            await _textSliderRepository.CreateAsync(slider);
            await _textSliderRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditTextSlider command)
        {
            var operation = new OperationResult();


            if (await _textSliderRepository.ExistsAsync(v => v.Title == command.Title && v.Id != command.Id))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var slider = await _textSliderRepository.GetAsync(command.Id);

            if (slider == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slider.Edit(command.Title, command.Description, command.Link);

            await _textSliderRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditTextSlider> GetDetailsAsync(long id) =>
            await _textSliderRepository.GetDetailsAsync(id);

        public async ValueTask<List<TextSliderViewModel>> SearchAsync(TextSliderSearchModel searchModel) =>
            await _textSliderRepository.SearchAsync(searchModel);

        public async ValueTask<OperationResult> RemoveAsync(long id)
        {
            var operation = new OperationResult();

            var slider = await _textSliderRepository.GetAsync(id);

            if (slider == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slider.Remove();

            await _textSliderRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> RestoreAsync(long id)
        {
            var operation = new OperationResult();

            var slider = await _textSliderRepository.GetAsync(id);

            if (slider == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slider.Restore();

            await _textSliderRepository.SaveChangesAsync();

            return operation.Succeeded();
        }
    }
}
