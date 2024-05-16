using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Application.Contracts.PersonalPage
{
    public interface IPersonalPageApplication
    {
        ValueTask<OperationResult> CreateAsync(CreatePersonalPage command);
        ValueTask<OperationResult> EditAsync(EditPersonalPage command);
        ValueTask<EditPersonalPage> GetDetailsAsync(long id);
        ValueTask<List<PersonalPageViewModel>> SearchAsync(PersonalPageSearchModel searchModel);
        ValueTask<PersonalPageViewModel> GetPageAsync(long id);
        ValueTask<OperationResult> ConfirmAsync(long id);
        ValueTask<OperationResult> CancelAsync(long id);
        ValueTask<OperationResult> ActiveAsync(long id);
        ValueTask<OperationResult> DisableAsync(long id);
        ValueTask<long> GetPersonalPageIdByAsync(long accountId);
        ValueTask<long> GetPersonalPageIdByAsync(string slug);
        ValueTask<PersonalPageViewModel> GetPageInfoByAsync(long pageId);
        ValueTask<PersonalPageViewModel> GetPageInfoWithPlanByAsync(long pageId);
        ValueTask<OperationResult> UseVipOfferAsync(long pageId);
        ValueTask<string> GetEditorPhoneNumberByEditorPageIdAsync(long pageId);
    }
}
