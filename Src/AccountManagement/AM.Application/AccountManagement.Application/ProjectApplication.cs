using _0_Framework.Application;
using AccountManagement.Application.Contracts.Project;
using AccountManagement.Application.Contracts.ProjectOffer;
using AccountManagement.Domain.ProjectAgg;
using AccountManagement.Domain.ProjectOfferAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class ProjectApplication : IProjectApplication
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectApplication(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async ValueTask<OperationResult> AddAsync(AddProject command)
        {
            var operation = new OperationResult();

            var expireDate = command.ExpireDate.ToGeorgianDateTime();
            var project = new Project(command.Title, command.Budget, expireDate, command.Tags, command.EmployerPageId, command.Description);
            await _projectRepository.CreateAsync(project);
            await _projectRepository.SaveChangesAsync();
            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditProject command)
        {
            var operation = new OperationResult();

            var project = await _projectRepository.GetAsync(command.Id);
            project.Edit(command.Title, command.Budget, command.Tags, command.EmployerPageId, command.Description);
            await _projectRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditProject> GetDetailsAsync(long id) => await _projectRepository.GetDetailsAsync(id);

        public async ValueTask<List<ProjectViewModel>> GetEmployerProjectsAsync(long employerId) =>
            await _projectRepository.GetEmployerProjectsAsync(employerId);

        public async ValueTask CloseAsync(long id)
        {
            var project = await _projectRepository.GetAsync(id);
            project.Close();
            await _projectRepository.SaveChangesAsync();
        }

        public async ValueTask OpenAsync(long id)
        {
            var project = await _projectRepository.GetAsync(id);
            project.Open();
            await _projectRepository.SaveChangesAsync();
        }

        public async ValueTask ConfirmAsync(long id)
        {
            var project = await _projectRepository.GetAsync(id);
            project.Confirm();
            await _projectRepository.SaveChangesAsync();
        }

        public async ValueTask RejectAsync(long id)
        {
            var project = await _projectRepository.GetAsync(id);
            project.Cancel();
            await _projectRepository.SaveChangesAsync();
        }

        public async ValueTask<bool> IsEmployerOwnerOfProject(long pageId, long projectId) => 
            await _projectRepository.IsEmployerOwnerOfProject(pageId, projectId);

        public async ValueTask<List<ProjectViewModel>> SearchAsync(string title) =>
            await _projectRepository.SearchAsync(title);
    }
}
