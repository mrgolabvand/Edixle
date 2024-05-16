using _0_Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contracts.EmployerPage
{
    public interface IEmployerPageApplication
    {
        ValueTask<OperationResult> AddAsync(AddEmployerPage command);
        ValueTask<OperationResult> EditAsync(EditEmployerPage command);
        ValueTask<EditEmployerPage> GetDetailsAsync(long id);
        ValueTask<EmployerPageViewModel> GetViewModelAsync(long id);
        ValueTask<List<EmployerPageViewModel>> SearchAsync(string title);
        ValueTask<long> GetEmployerPageIdByAccountIdAsync(long id);
        ValueTask<string> GetEmployerPhoneNumberByEmployerPageIdAsync(long id);
    }
}
