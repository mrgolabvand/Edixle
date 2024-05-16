using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.JobOffer;
using AccountManagement.Application.Contracts.ProjectOffer;
using AccountManagement.Domain.EmployerPageAgg;
using AccountManagement.Domain.ProjectOfferAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class EmployerPageRepository : BaseRepository<long, EmployerPage>, IEmployerPageRepository
    {
        private readonly AccountContext _context;

        public EmployerPageRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<EditEmployerPage> GetDetailsAsync(long id) =>
            await _context.EmployerPages.Select(v => new EditEmployerPage
            {
                Id = v.Id,
                AccountId = v.AccountId,
                FullName = v.FullName,
                Card = v.Card,
            }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<long> GetEmployerPageIdByAccountIdAsync(long id)
        {
            var result = await _context.EmployerPages.AsNoTracking().FirstOrDefaultAsync(v => v.AccountId == id);
            return result == null ? 0 : result.Id;
        }

        public async ValueTask<EmployerPageViewModel> GetViewModelAsync(long id) =>
            await _context.EmployerPages.Select(v => new EmployerPageViewModel
            {
                Picture = v.Picture,
                FullName = v.FullName,
                AccountId = v.AccountId,
                Id = v.Id,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                Card = v.Card,
            }).AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<string> GetEmployerPhoneNumberByEmployerPageIdAsync(long id)
        {
            var employerAccountId = _context.EmployerPages.FirstOrDefault(v => v.Id == id)?.AccountId;
            if (employerAccountId == null) return string.Empty;
            var phoneNumber = _context.Accounts.FirstOrDefault(v => v.Id == employerAccountId)?.PhoneNumber;
            return phoneNumber == null ? string.Empty : phoneNumber.ToString();
        }

        public async ValueTask<List<EmployerPageViewModel>> SearchAsync(string fullName)
        {
            var query = _context.EmployerPages.Include(v => v.Account).Select(v => new EmployerPageViewModel
            {
                Id = v.Id,
                Picture = v.Picture,
                FullName = v.FullName,
                AccountId = v.AccountId,
                Account = v.Account.UserName,
                Card = v.Card,
            });

            if (!string.IsNullOrWhiteSpace(fullName))
                query = query.Where(v => v.FullName.Contains(fullName));

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
