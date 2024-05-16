using _0_Framework.Application;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Domain.EmployerPageAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class EmployerPageApplication : IEmployerPageApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IFileHostUploader _fileHostUploader;
        private readonly IEmployerPageRepository _employerPageRepository;

        public EmployerPageApplication(IEmployerPageRepository employerPageRepository, IFileUploader fileUploader, IFileHostUploader fileHostUploader)
        {
            _employerPageRepository = employerPageRepository;
            _fileUploader = fileUploader;
            _fileHostUploader = fileHostUploader;
        }

        public async ValueTask<OperationResult> AddAsync(AddEmployerPage command)
        {
            var operation = new OperationResult();

            if (await _employerPageRepository.ExistsAsync(v => v.AccountId == command.AccountId))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var name = Tools.CodeGenerator(20);
            var picturePath = $"EmployerPages/{name}";
            var picture = string.Empty;
            if (command.Picture != null)
            {
                picture = await _fileUploader.UploadAsync(command.Picture, picturePath);
            }
            else
            {
                picture = "user.png";
            }

            var employerPage = new EmployerPage(command.FullName, picture, command.AccountId, command.Card);

            await _employerPageRepository.CreateAsync(employerPage);
            await _employerPageRepository.SaveChangesAsync();
            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditEmployerPage command)
        {
            var operation = new OperationResult();

            if (await _employerPageRepository.ExistsAsync(v => v.AccountId == command.AccountId && v.Id != command.Id))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var employerPage = await _employerPageRepository.GetAsync(command.Id);
            
            if (employerPage == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var filePath = employerPage.Picture;
            if (command.Picture != null && filePath != "user.png" && !string.IsNullOrWhiteSpace(filePath))
            {
                try
                {
                    _fileUploader.DeleteFile(filePath);
                }
                catch
                {
                    // ignored
                }
            }

            var picture = filePath;

            if (command.Picture != null && !string.IsNullOrWhiteSpace(filePath) && filePath != "user.png")
            {
                picture = await _fileUploader.UploadAsync(command.Picture, filePath);
            }

            if (command.Picture == null && string.IsNullOrWhiteSpace(filePath))
            {
                picture = "user.png";
            }

            if (command.Picture != null && (string.IsNullOrWhiteSpace(filePath) || filePath == "user.png"))
            {
                var name = Tools.CodeGenerator(20);
                filePath = $"EmployerPages/{name}";
                picture = await _fileUploader.UploadAsync(command.Picture, filePath);
            }
            employerPage.Edit(command.FullName,  picture, command.AccountId, command.Card);
            await _employerPageRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditEmployerPage> GetDetailsAsync(long id) => await _employerPageRepository.GetDetailsAsync(id);

        public async ValueTask<EmployerPageViewModel> GetViewModelAsync(long id) =>
            await _employerPageRepository.GetViewModelAsync(id);

        public async ValueTask<long> GetEmployerPageIdByAccountIdAsync(long id) =>
            await _employerPageRepository.GetEmployerPageIdByAccountIdAsync(id);

        public async ValueTask<string> GetEmployerPhoneNumberByEmployerPageIdAsync(long id) =>
            await _employerPageRepository.GetEmployerPhoneNumberByEmployerPageIdAsync(id);

        public async ValueTask<List<EmployerPageViewModel>> SearchAsync(string title) =>
            await _employerPageRepository.SearchAsync(title);
    }
}
