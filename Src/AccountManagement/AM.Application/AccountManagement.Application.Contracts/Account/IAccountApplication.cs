using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        ValueTask<OperationResult> RegisterAsync(RegisterAccount command);
        ValueTask<OperationResult> EditAsync(EditAccount command);
        ValueTask<OperationResult> LoginAsync(Login command);
        ValueTask<OperationResult> ChangePasswordAsync(ChangePassword command);
        void Logout();
        ValueTask<EditAccount> GetDetailsAsync(long id);
        ValueTask<List<AccountViewModel>> SearchAsync(AccountSearchModel searchModel);
        ValueTask<List<AccountViewModel>> GetListAsync();
        ValueTask<bool> IsEditorAsync(long id);
        ValueTask<bool> IsEmployerAsync(long id);
        ValueTask<long> GetPageIdAsync(long accountId);
        ValueTask<bool> VerifyCode(string phoneNumber, string code);
        ValueTask<OperationResult> SendVerificationCode(string phoneNumber);
        ValueTask<List<string>> GetEditorsPhoneNumbers();
        ValueTask<string> GetPhoneNumber(long id);
        ValueTask<long> GetIdBy(string userName);
    }
}
