using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Project;
using AccountManagement.Application.Contracts.ProjectOffer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Domain.ProjectAgg
{
    public interface IProjectRepository : IBaseRepository<long, Project>
    {
        ValueTask<EditProject> GetDetailsAsync(long id);
        ValueTask<List<ProjectViewModel>> SearchAsync(string title);
        ValueTask<List<ProjectViewModel>> GetEmployerProjectsAsync(long employerId);
        ValueTask<bool> IsEmployerOwnerOfProject(long pageId, long projectId);
    }
}
