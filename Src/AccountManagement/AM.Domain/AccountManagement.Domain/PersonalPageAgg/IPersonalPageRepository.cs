using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PersonalPage;

namespace AccountManagement.Domain.PersonalPageAgg
{
    public interface IPersonalPageRepository : IBaseRepository<long, PersonalPage>
    {
        ValueTask<EditPersonalPage> GetDetailsAsync(long id);
        ValueTask<List<PersonalPageViewModel>> SearchAsync(PersonalPageSearchModel searchModel);
        ValueTask<long> GetPersonalPageIdByAsync(long accountId);
        ValueTask<long> GetPersonalPageIdByAsync(string slug);
        ValueTask<PersonalPageViewModel> GetPageInfoByAsync(long pageId);
        ValueTask<PersonalPageViewModel> GetPageInfoWithPlanByAsync(long pageId);
        ValueTask<string> GetEditorPhoneNumberByEditorPageIdAsync(long pageId);
    }
}
