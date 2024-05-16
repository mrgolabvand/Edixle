using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.EmployerPage;

namespace AccountManagement.Domain.EmployerPageAgg
{
    public interface IEmployerPageRepository : IBaseRepository<long, EmployerPage>
    {
        ValueTask<EditEmployerPage> GetDetailsAsync(long id);
        ValueTask<List<EmployerPageViewModel>> SearchAsync(string fullName);
        ValueTask<long> GetEmployerPageIdByAccountIdAsync(long id);
        ValueTask<EmployerPageViewModel> GetViewModelAsync(long id);
        ValueTask<string> GetEmployerPhoneNumberByEmployerPageIdAsync(long id);
    }
}
