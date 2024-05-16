using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IBaseRepository<long, Account> /*IBaseRepositorV2<Account, long, EditAccount, AccountViewModel>*/
    {
        ValueTask<Account> GetByUserNameAsync(string userName);
        ValueTask<EditAccount> GetDetailsAsync(long id);
        ValueTask<List<AccountViewModel>> SearchAsync(AccountSearchModel searchModel);
        ValueTask<List<AccountViewModel>> GetListAsync();
        ValueTask<bool> IsEditorAsync(long id);
        ValueTask<bool> IsEmployerAsync(long id);
        ValueTask<long> GetPageIdAsync(long accountId);
        ValueTask<AccountViewModel> GetModelByUserNameAsync(string userName);
        ValueTask<bool> VerifyCode(string phoneNumber, string code);
        ValueTask<bool> PreviousCodeNotExpired(string phoneNumber);
        ValueTask CreateVerificationCode(VerificationCode verificationCode);
        ValueTask<List<string>> GetEditorsPhoneNumbers();
        ValueTask<string> GetPhoneNumber(long id);
        ValueTask<long> GetIdBy(string userName);
    }
}
