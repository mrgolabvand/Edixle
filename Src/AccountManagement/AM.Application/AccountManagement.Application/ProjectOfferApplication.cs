using _0_Framework.Application;
using AccountManagement.Application.Contracts.ProjectOffer;
using AccountManagement.Domain.ProjectOfferAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class ProjectOfferApplication : IProjectOfferApplication
    {
        private readonly IProjectOfferRepository _projectOfferRepository;

        public ProjectOfferApplication(IProjectOfferRepository projectOfferRepository)
        {
            _projectOfferRepository = projectOfferRepository;
        }

        public async ValueTask<OperationResult> AddAsync(AddProjectOffer command)
        {
            var operation = new OperationResult();

            if (await _projectOfferRepository.ExistsAsync(v => v.ProjectId == command.ProjectId && v.EditorPageId == command.EditorPageId))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var projectOffer = new ProjectOffer(command.Title, command.Price, command.Description, command.ProjectId, command.EditorPageId);
            await _projectOfferRepository.CreateAsync(projectOffer);
            await _projectOfferRepository.SaveChangesAsync();
            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditProjectOffer command)
        {
            var operation = new OperationResult();

            if (await _projectOfferRepository.ExistsAsync(v => v.ProjectId == command.ProjectId && v.EditorPageId == command.EditorPageId))
                return operation.Failed(ApplicationMessages.IsDuplicated);
             
            var projectOffer = await _projectOfferRepository.GetAsync(command.Id);
            projectOffer.Edit(command.Title, command.Price, command.Description, command.ProjectId, command.EditorPageId);
            await _projectOfferRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> AcceptAsync(long id)
        {
            var operation = new OperationResult();

            var projectOffer = await _projectOfferRepository.GetAsync(id);
            projectOffer.Accept();
            await _projectOfferRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditProjectOffer> GetDetailsAsync(long id) =>
            await _projectOfferRepository.GetDetailsAsync(id);

        public async ValueTask<List<ProjectOfferViewModel>> SearchAsync(string title) =>
            await _projectOfferRepository.SearchAsync(title);

        public async ValueTask<ProjectOfferViewModel> GetOfferAsync(long id) => await _projectOfferRepository.GetOfferAsync(id);
    }
}
