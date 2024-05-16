using _0_Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contracts.Project
{
    public interface IProjectApplication
    {
        ValueTask<OperationResult> AddAsync(AddProject command);
        ValueTask<OperationResult> EditAsync(EditProject command);
        ValueTask<EditProject> GetDetailsAsync(long id);
        ValueTask<List<ProjectViewModel>> SearchAsync(string title);
        ValueTask<List<ProjectViewModel>> GetEmployerProjectsAsync(long employerId);
        ValueTask CloseAsync(long id);
        ValueTask OpenAsync(long id);
        ValueTask ConfirmAsync(long id);
        ValueTask RejectAsync(long id);
        ValueTask<bool> IsEmployerOwnerOfProject(long pageId, long projectId);
    }
}
