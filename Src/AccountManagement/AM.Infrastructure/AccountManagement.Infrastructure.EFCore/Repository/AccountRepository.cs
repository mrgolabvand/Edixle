using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : BaseRepository<long, Account> /*BaseRepositoryV2<Account, long, EditAccount, AccountViewModel>*/, IAccountRepository
    {
        private readonly AccountContext _accountContext;
        private readonly IMapper Mapper;

        public AccountRepository(AccountContext accountContext, IMapper mapper) : base(accountContext)
        {
            _accountContext = accountContext;
            Mapper = mapper;
        }

        public async ValueTask<Account> GetByUserNameAsync(string userName)
        {
            var account = await _accountContext.Accounts.FirstOrDefaultAsync(v => v.UserName == userName);
            return account;
        }

        public async ValueTask<AccountViewModel> GetModelByUserNameAsync(string userName) =>
            await _accountContext.Accounts.Select(v => new AccountViewModel
            {
                RoleId = v.RoleId,
                UserName = v.UserName,
                Email = v.Email,
                Password = v.Password,
                PhoneNumber = v.PhoneNumber,
                Id = v.Id,
            })
                .FirstOrDefaultAsync(v => v.UserName == userName);

        public async ValueTask<bool> VerifyCode(string phoneNumber, string code) =>
            await _accountContext
                .VerificationCodes
                .AnyAsync(v =>
                    v.PhoneNumber == phoneNumber
                    && v.Code == code
                    && v.ExpireDate >= DateTime.Now);

        public async ValueTask<bool> PreviousCodeNotExpired(string phoneNumber) =>
            await _accountContext
                .VerificationCodes
                .AnyAsync(v =>
                    v.PhoneNumber == phoneNumber
                    && v.ExpireDate >= DateTime.Now);

        public async ValueTask CreateVerificationCode(VerificationCode verificationCode)
        {
            await _accountContext.VerificationCodes.AddAsync(verificationCode);
            await _accountContext.SaveChangesAsync();
        }

        public async ValueTask<List<string>> GetEditorsPhoneNumbers()
        {
            var numbers = new List<string>();
            var accounts = await _accountContext.Accounts
                .Select(v => new { v.Id, v.PhoneNumber })
                .ToListAsync();
          
            foreach (var account in accounts)
            {
                if (await IsEditorAsync(account.Id))
                {
                    numbers.Add(account.PhoneNumber);
                }
            }

            return numbers;
        }

        public async ValueTask<string> GetPhoneNumber(long id)
        {
           var account = await _accountContext.Accounts.Select(v => new { v.Id, v.PhoneNumber })
                .FirstOrDefaultAsync(v => v.Id == id);

           return account == null ? string.Empty : account.PhoneNumber;
        }

        public async ValueTask<long> GetIdBy(string userName)
        {
            var account = await _accountContext.Accounts.Select(v => new{ v.Id, v.UserName}).FirstOrDefaultAsync(v => v.UserName == userName);
            return account?.Id ?? 0;
        }

        public async ValueTask<EditAccount> GetDetailsAsync(long id) =>
            await _accountContext.Accounts.Select(v => new EditAccount
            {
                Id = v.Id,
                Email = v.Email,
                UserName = v.UserName,
                PhoneNumber = v.PhoneNumber,
                RoleId = v.RoleId,
            }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<AccountViewModel>> SearchAsync(AccountSearchModel searchModel)
        {
            var query = _accountContext.Accounts.Select(v => new AccountViewModel
            {
                Id = v.Id,
                Email = v.Email,
                UserName = v.UserName,
                PhoneNumber = v.PhoneNumber,
                CreationDate = v.CreationDate.ToFarsi()
            });
            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.UserName))
                    query = query.Where(v => v.UserName.Contains(searchModel.UserName));

                if (!string.IsNullOrWhiteSpace(searchModel.Email))
                    query = query.Where(v => v.Email.Contains(searchModel.Email));
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async ValueTask<List<AccountViewModel>> GetListAsync() => await _accountContext.Accounts
                .Select(v => new AccountViewModel { Id = v.Id, UserName = v.UserName }).AsNoTracking().ToListAsync();

        public async ValueTask<bool> IsEditorAsync(long id)
        {
            var account = await _accountContext.Accounts.Select(v => new { v.Id })
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);

            if (account == null)
                return false;

            var pages = await _accountContext.PersonalPages.Select(v => new { v.AccountId })
                .AsNoTracking()
                .ToListAsync();

            return pages.Any(v => v.AccountId == account.Id);
        }

        public async ValueTask<bool> IsEmployerAsync(long id)
        {
            var account = await _accountContext.Accounts.Select(v => new { v.Id })
                .FirstOrDefaultAsync(v => v.Id == id);

            if (account == null)
                return false;

            var pages = await _accountContext.EmployerPages.Select(v => new { v.AccountId })
                .AsNoTracking()
                .ToListAsync();

            return pages.Any(v => v.AccountId == account.Id);
        }

        public async ValueTask<long> GetPageIdAsync(long accountId)
        {
            var account = await _accountContext.Accounts.Select(v => new { v.Id })
                .FirstOrDefaultAsync(v => v.Id == accountId);

            if (account == null)
                return 0;

            var pages = await _accountContext.PersonalPages.Select(v => new { v.Id, v.AccountId })
                .AsNoTracking()
                .ToListAsync();

            return pages.FirstOrDefault(v => v.AccountId == account.Id).Id;
        }


    }
}
