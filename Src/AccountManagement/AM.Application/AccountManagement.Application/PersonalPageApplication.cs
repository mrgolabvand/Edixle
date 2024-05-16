using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Domain.PersonalPageAgg;

namespace AccountManagement.Application
{
    public class PersonalPageApplication : IPersonalPageApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IFileUploader _fileUploader;
        private readonly IFileHostUploader _fileHostUploader;
        private readonly IPersonalPageRepository _personalPageRepository;

        public PersonalPageApplication(IPersonalPageRepository personalPageRepository, IFileUploader fileUploader, IAuthHelper authHelper, IFileHostUploader fileHostUploader)
        {
            _personalPageRepository = personalPageRepository;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _fileHostUploader = fileHostUploader;
        }

        public async ValueTask<OperationResult> CreateAsync(CreatePersonalPage command)
        {
            var operation = new OperationResult();

            if (await _personalPageRepository.ExistsAsync(v => v.FullName == command.FullName))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var slug = command.Slug.Slugify();
            var name = Tools.CodeGenerator(20);
            var picture = string.Empty;
            if (command.Picture != null)
            {
                picture = await _fileUploader.UploadAsync(command.Picture, $"Users/{name}");
            }
            else
            {
                picture = "user.png";
            }
            //var picture = _fileHostUploader.Upload(command.Picture, $"Users/{slug}");
            var accountId = _authHelper.CurrentAccountId();

            var page = new PersonalPage(command.FullName, slug, command.Keywords, command.MetaDescription,
                picture, command.PictureAlt, command.PictureTitle, command.About, command.Age,
                accountId, command.MinPay,
                command.MaxPay, command.PayDate, command.Card);

            await _personalPageRepository.CreateAsync(page);
            await _personalPageRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditPersonalPage command)
        {
            var operation = new OperationResult();

            if (await _personalPageRepository.ExistsAsync(v => v.FullName == command.FullName && v.Id != command.Id))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var page = await _personalPageRepository.GetAsync(command.Id);

            if (page == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            var slug = command.Slug.Slugify();
            var filePath = page.Picture;
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
                filePath = $"Users/{name}";
                picture = await _fileUploader.UploadAsync(command.Picture, filePath);
            }    
 
            page.Edit(command.FullName, slug, command.Keywords, command.MetaDescription,
                picture, command.PictureAlt, command.PictureTitle, command.About, command.Age, command.MinPay,
                command.MaxPay, command.PayDate, command.Card);

            await _personalPageRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditPersonalPage> GetDetailsAsync(long id) => await _personalPageRepository.GetDetailsAsync(id);

        public async ValueTask<List<PersonalPageViewModel>> SearchAsync(PersonalPageSearchModel searchModel) =>
            await _personalPageRepository.SearchAsync(searchModel);

        public async ValueTask<PersonalPageViewModel> GetPageAsync(long id)
        {
            var list = await _personalPageRepository.GetAsync();
            return list.Select(v => new PersonalPageViewModel
            {
                FullName = v.FullName,
                About = v.About,
                Age = v.Age,
                CreationDate = v.Slug,
                Id = v.Id,
                Picture = v.Picture,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                Slug = v.Slug,
                MaxPay = v.MaxPay,
                PayDate = v.PayDate,
                MinPay = v.MinPay,
            }).FirstOrDefault(v => v.Id == id);
        }


        public async ValueTask<OperationResult> ConfirmAsync(long id)
        {
            var operation = new OperationResult();

            var page = await _personalPageRepository.GetAsync(id);

            if (page == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            page.Confirm();

            await _personalPageRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> CancelAsync(long id)
        {
            var operation = new OperationResult();

            var page = await _personalPageRepository.GetAsync(id);

            if (page == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            page.Cancel();

            await _personalPageRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> ActiveAsync(long id)
        {
            var operation = new OperationResult();

            var page = await _personalPageRepository.GetAsync(id);

            if (page == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            page.Activate();

            await _personalPageRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> DisableAsync(long id)
        {
            var operation = new OperationResult();

            var page = await _personalPageRepository.GetAsync(id);

            if (page == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            page.Disable();

            await _personalPageRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<long> GetPersonalPageIdByAsync(long accountId)
        {
            if (accountId == 0)
                return 0;
            return await _personalPageRepository.GetPersonalPageIdByAsync(accountId);
        }

        public async ValueTask<long> GetPersonalPageIdByAsync(string slug) =>
            await _personalPageRepository.GetPersonalPageIdByAsync(slug);

        public async ValueTask<PersonalPageViewModel> GetPageInfoByAsync(long pageId) =>
            await _personalPageRepository.GetPageInfoByAsync(pageId);

        public async ValueTask<PersonalPageViewModel> GetPageInfoWithPlanByAsync(long pageId) =>
             await _personalPageRepository.GetPageInfoWithPlanByAsync(pageId);

        public async ValueTask<OperationResult> UseVipOfferAsync(long pageId)
        {
            var result = new OperationResult();
            var page = await _personalPageRepository.GetAsync(pageId);

            if (page == null)
                return result.Failed(ApplicationMessages.RecordNotFound);

            page.UseVipOffer();

            await _personalPageRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async ValueTask<string> GetEditorPhoneNumberByEditorPageIdAsync(long pageId) =>
            await _personalPageRepository.GetEditorPhoneNumberByEditorPageIdAsync(pageId);
    }
}
