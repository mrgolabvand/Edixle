using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Application.Sms;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.PersonalPageAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IPersonalPageRepository _personalPageRepository;
        private readonly ISmsService _smsService;
        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IAuthHelper authHelper, IPersonalPageRepository personalPageRepository, IRoleRepository roleRepository, ISmsService smsService)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _authHelper = authHelper;
            _personalPageRepository = personalPageRepository;
            _roleRepository = roleRepository;
            _smsService = smsService;
        }

        public async ValueTask<OperationResult> RegisterAsync(RegisterAccount command)
        {
            var operation = new OperationResult();

            if (await _accountRepository.ExistsAsync(v => v.Email == command.Email || v.UserName == command.UserName))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var password = _passwordHasher.Hash(command.Password);

            var account = new Account(command.UserName, command.Email, password, command.RoleId, command.PhoneNumber);

            await _accountRepository.CreateAsync(account);
            await _accountRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditAccount command)
        {
            var operation = new OperationResult();

            if (await _accountRepository.ExistsAsync(v => (v.Email == command.Email || v.UserName == command.UserName) && v.Id != command.Id))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var account = await _accountRepository.GetAsync(command.Id);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            account.Edit(command.UserName, command.Email, command.RoleId);
            await _accountRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> LoginAsync(Login command)
        {
            var operation = new OperationResult();

            var account = await _accountRepository.GetModelByUserNameAsync(command.UserName);

            if (account == null)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            (bool Verified, bool NeedsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);

            if (!result.Verified)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            var role = await _roleRepository.GetAsync(account.RoleId);
            var permissions = role.Permissions.Select(v => v.Code).ToList();
            var authViewModel = new AuthViewModel(account.Id, account.UserName, account.RoleId, permissions);
            _authHelper.Signin(authViewModel);

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> ChangePasswordAsync(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = await _accountRepository.GetAsync(command.Id);

            if (account == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.PasswordsNotMatch);

            var password = _passwordHasher.Hash(command.Password);
            account.ChangePassword(password);
            await _accountRepository.SaveChangesAsync();
            return operation.Succeeded();
        }

        public void Logout()
        {
            _authHelper.SignOut();
        }

        public async ValueTask<EditAccount> GetDetailsAsync(long id) => await _accountRepository.GetDetailsAsync(id);

        public async ValueTask<List<AccountViewModel>> SearchAsync(AccountSearchModel searchModel) =>
            await _accountRepository.SearchAsync(searchModel);

        public async ValueTask<List<AccountViewModel>> GetListAsync() => await _accountRepository.GetListAsync();

        public async ValueTask<bool> IsEditorAsync(long id) => await _accountRepository.IsEditorAsync(id);

        public async ValueTask<bool> IsEmployerAsync(long id) => await _accountRepository.IsEmployerAsync(id);

        public async ValueTask<long> GetPageIdAsync(long accountId) => await _accountRepository.GetPageIdAsync(accountId);

        public async ValueTask<bool> VerifyCode(string phoneNumber, string code) =>
            await _accountRepository.VerifyCode(phoneNumber, code);

        public async ValueTask<OperationResult> SendVerificationCode(string phoneNumber)
        {
            var result = new OperationResult();
            if (await _accountRepository.PreviousCodeNotExpired(phoneNumber))
            {
                var message = "کد قبلی هنوز معتبر است.";
                return result.Failed(message);
            }
            if (await _accountRepository.ExistsAsync(v => v.PhoneNumber == phoneNumber))
            {
                var message = "این شماره تلفن قبلا ثبت شده است.";
                return result.Failed(message);
            }
            var generatedCode = Tools.CodeGenerator(4);
            var command = new CreateVerificationCode
            {
                Code = generatedCode,
                PhoneNumber = phoneNumber
            };
            var verificationCode = new VerificationCode(command.PhoneNumber, command.Code);
            await _accountRepository.CreateVerificationCode(verificationCode);
            // Send SMS To PhoneNumber
            _smsService.SendRegisterCodeWithMeliPayamak_Com(phoneNumber, generatedCode);
            return result.Succeeded();
        }

        public async ValueTask<List<string>> GetEditorsPhoneNumbers() => await _accountRepository.GetEditorsPhoneNumbers();

        public async ValueTask<string> GetPhoneNumber(long id) =>
            await _accountRepository.GetPhoneNumber(id);

        public async ValueTask<long> GetIdBy(string userName) =>
            await _accountRepository.GetIdBy(userName);
    }
}
